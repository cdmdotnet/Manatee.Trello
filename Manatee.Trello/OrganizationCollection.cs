using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyOrganizationCollection : ReadOnlyCollection<Organization>
	{
		internal ReadOnlyOrganizationCollection(string ownerId)
			: base(ownerId) { }

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Organizations, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonOrganization>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => TrelloConfiguration.Cache.Find<Organization>(c => c.Id == jc.Id) ?? new Organization(jc, true)));
		}
	}

	public class OrganizationCollection : ReadOnlyOrganizationCollection
	{
		internal OrganizationCollection(string ownerId)
			: base(ownerId) {}

		public Organization Add(string name)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonOrganization>();
			json.Name = name;

			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Write_CreateOrganization);
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new Organization(newData, true);
		}
	}
}