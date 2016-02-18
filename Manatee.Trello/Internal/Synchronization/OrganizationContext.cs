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
 
	File Name:		OrganizationContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		OrganizationContext
	Purpose:		Provides a data context for an organization.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class OrganizationContext : SynchronizationContext<IJsonOrganization>
	{
		private bool _deleted;

		public OrganizationPreferencesContext OrganizationPreferencesContext { get; }
		protected override bool IsDataComplete => !Data.DisplayName.IsNullOrWhiteSpace();
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static OrganizationContext()
		{
			_properties = new Dictionary<string, Property<IJsonOrganization>>
				{
					{"Description", new Property<IJsonOrganization, string>((d, a) => d.Desc, (d, o) => d.Desc = o)},
					{"DisplayName", new Property<IJsonOrganization, string>((d, a) => d.DisplayName, (d, o) => d.DisplayName = o)},
					{"Id", new Property<IJsonOrganization, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsBusinessClass", new Property<IJsonOrganization, bool?>((d, a) => d.PaidAccount, (d, o) => d.PaidAccount = o)},
					{"Name", new Property<IJsonOrganization, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Preferences", new Property<IJsonOrganization, IJsonOrganizationPreferences>((d, a) => d.Prefs, (d, o) => d.Prefs = o)},
					{"Url", new Property<IJsonOrganization, string>((d, a) => d.Url, (d, o) => d.Url = o)},
					{"Website", new Property<IJsonOrganization, string>((d, a) => d.Website, (d, o) => d.Website = o)},
				};
		}
		public OrganizationContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			OrganizationPreferencesContext = new OrganizationPreferencesContext(Auth);
			OrganizationPreferencesContext.SynchronizeRequested += () => Synchronize();
			OrganizationPreferencesContext.SubmitRequested += () => HandleSubmitRequested("Preferences");
			Data.Prefs = OrganizationPreferencesContext.Data;
		}

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override IJsonOrganization GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonOrganization>(Auth, endpoint);
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
		protected override void SubmitData(IJsonOrganization json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override void ApplyDependentChanges(IJsonOrganization json)
		{
			if (json.Prefs != null)
			{
				json.Prefs = OrganizationPreferencesContext.GetChanges();
				OrganizationPreferencesContext.ClearChanges();
			}
		}
		protected override IEnumerable<string> MergeDependencies(IJsonOrganization json)
		{
			return OrganizationPreferencesContext.Merge(json.Prefs);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}