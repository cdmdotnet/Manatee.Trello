using System.Collections.Generic;
using Manatee.Trello.Internal.DataAccess;
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
					{"Description", new Property<IJsonOrganization, string>(d => d.Desc, (d, o) => d.Desc = o)},
					{"DisplayName", new Property<IJsonOrganization, string>(d => d.DisplayName, (d, o) => d.DisplayName = o)},
					{"Id", new Property<IJsonOrganization, string>(d => d.Id, (d, o) => d.Id = o)},
					{"IsBusinessClass", new Property<IJsonOrganization, bool?>(d => d.PaidAccount, (d, o) => d.PaidAccount = o)},
					{"Name", new Property<IJsonOrganization, string>(d => d.Name, (d, o) => d.Name = o)},
					{"Website", new Property<IJsonOrganization, string>(d => d.Name, (d, o) => d.Name = o)},
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
		protected override IEnumerable<string> MergeDependencies(IJsonOrganization json)
		{
			return OrganizationPreferencesContext.Merge(json.Prefs);
		}
	}
}