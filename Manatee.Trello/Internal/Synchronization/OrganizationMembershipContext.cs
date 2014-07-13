using System.Collections.Generic;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class OrganizationMembershipContext : SynchronizationContext<IJsonOrganizationMembership>
	{
		private readonly string _ownerId;

		static OrganizationMembershipContext()
		{
			_properties = new Dictionary<string, Property<IJsonOrganizationMembership>>
				{
					{"IsUnconfirmed", new Property<IJsonOrganizationMembership>(d => d.Unconfirmed, (d, o) => d.Unconfirmed = (bool?) o)},
					{
						"Member", new Property<IJsonOrganizationMembership>(d => TrelloConfiguration.Cache.Find<Member>(b => b.Id == d.Member.Id) ?? new Member(d.Member),
						                                                    (d, o) => d.Member = o != null ? ((Member) o).Json : null)
					},
					{
						"MemberType", new Property<IJsonOrganizationMembership>(d => d.MemberType.ConvertEnum<OrganizationMembershipType>(),
						                                                        (d, o) => d.MemberType = ((OrganizationMembershipType) o).ConvertEnum())
					},
				};
		}
		public OrganizationMembershipContext(string id, string ownerId)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}
		protected override IJsonOrganizationMembership GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationMembership_Read_Refresh, new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonOrganizationMembership>(TrelloAuthorization.Default, endpoint);
			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationMembership_Write_Update, new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}