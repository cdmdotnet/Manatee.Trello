/***************************************************************************************

	Copyright 2014 Greg Dennis

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

		public ActionDataContext ActionDataContext { get; private set; }
		public virtual bool HasValidId { get { return IdRule.Instance.Validate(Data.Id, null) == null; } }

		static ActionContext()
		{
			_properties = new Dictionary<string, Property<IJsonAction>>
				{
					{
						"Creator", new Property<IJsonAction, Member>(d => d.MemberCreator.GetFromCache<Member>(),
						                                             (d, o) => d.MemberCreator = o.Json)
					},
					{"Date", new Property<IJsonAction, DateTime?>(d => d.Date, (d, o) => d.Date = o)},
					{"Id", new Property<IJsonAction, string>(d => d.Id, (d, o) => d.Id = o)},
					{"Type", new Property<IJsonAction, ActionType?>(d => d.Type, (d, o) => d.Type = o)},
				};
		}
		public ActionContext(string id)
		{
			Data.Id = id;
			ActionDataContext = new ActionDataContext();
			ActionDataContext.SynchronizeRequested += () => Synchronize();
			Data.Data = ActionDataContext.Data;
		}

		public void Delete()
		{
			if (_deleted) return;

			var endpoint = EndpointFactory.Build(EntityRequestType.Action_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);

			_deleted = true;
		}

		protected override IJsonAction GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Action_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonAction>(TrelloAuthorization.Default, endpoint);

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError()) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}