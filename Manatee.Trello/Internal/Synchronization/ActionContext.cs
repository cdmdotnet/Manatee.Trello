/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ActionContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		ActionContext
	Purpose:		Provides a data context for actions.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class ActionContext : SynchronizationContext<IJsonAction>
	{
		private bool _deleted;

		public ActionDataContext ActionDataContext { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static ActionContext()
		{
			_properties = new Dictionary<string, Property<IJsonAction>>
				{
					{
						"Creator", new Property<IJsonAction, Member>((d, a) => d.MemberCreator.GetFromCache<Member>(a),
						                                             (d, o) => { if (o != null) d.MemberCreator = o.Json; })
					},
					{"Date", new Property<IJsonAction, DateTime?>((d, a) => d.Date, (d, o) => d.Date = o)},
					{"Id", new Property<IJsonAction, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Type", new Property<IJsonAction, ActionType?>((d, a) => d.Type, (d, o) => d.Type = o)},
				};
		}
		public ActionContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			ActionDataContext = new ActionDataContext(Auth);
			ActionDataContext.SynchronizeRequested += () => Synchronize();
			Data.Data = ActionDataContext.Data;
		}

		public void Delete()
		{
			if (_deleted) return;

			var endpoint = EndpointFactory.Build(EntityRequestType.Action_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override IJsonAction GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Action_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonAction>(Auth, endpoint);
				MarkInitialized();

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override void ApplyDependentChanges(IJsonAction json)
		{
			if (json.Data != null)
			{
				json.Data = ActionDataContext.GetChanges();
				ActionDataContext.ClearChanges();
			}
		}
		protected override IEnumerable<string> MergeDependencies(IJsonAction json)
		{
			return ActionDataContext.Merge(json.Data);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}