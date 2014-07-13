using System.Collections.Generic;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class OrganizationContext : SynchronizationContext<IJsonOrganization>
	{
		public OrganizationPreferencesContext OrganizationPreferencesContext { get; private set; }

		static OrganizationContext()
		{
			_properties = new Dictionary<string, Property<IJsonOrganization>>
				{
					{"Description", new Property<IJsonOrganization>(d => d.Desc, (d, o) => d.Desc = (string) o)},
					{"DisplayName", new Property<IJsonOrganization>(d => d.DisplayName, (d, o) => d.DisplayName = (string) o)},
					{"IsBusinessClass", new Property<IJsonOrganization>(d => d.PaidAccount, (d, o) => d.PaidAccount = (bool?) o)},
					{"Name", new Property<IJsonOrganization>(d => d.Name, (d, o) => d.Name = (string) o)},
					{"Website", new Property<IJsonOrganization>(d => d.Name, (d, o) => d.Name = (string) o)},
				};
		}
		public OrganizationContext(string id)
		{
			Data.Id = id;
			OrganizationPreferencesContext = new OrganizationPreferencesContext();
			OrganizationPreferencesContext.SynchronizeRequested += () => Synchronize();
			OrganizationPreferencesContext.SubmitRequested += ResetTimer;
			Data.Prefs = OrganizationPreferencesContext.Data;
		}

		public void Delete()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);
		}

		protected override IJsonOrganization GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonOrganization>(TrelloAuthorization.Default, endpoint);
			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}