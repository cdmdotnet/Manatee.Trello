using System.Collections.Generic;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
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
					{"Id", new Property<IJsonOrganizationMembership, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsUnconfirmed", new Property<IJsonOrganizationMembership, bool?>((d, a) => d.Unconfirmed, (d, o) => d.Unconfirmed = o)},
					{
						"Member", new Property<IJsonOrganizationMembership, Member>((d, a) => d.Member.GetFromCache<Member>(a),
						                                                    (d, o) => d.Member = o?.Json)
					},
					{"MemberType", new Property<IJsonOrganizationMembership, OrganizationMembershipType?>((d, a) => d.MemberType, (d, o) => d.MemberType = o)},
				};
		}
		public OrganizationMembershipContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}
		protected override async Task<IJsonOrganizationMembership> GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationMembership_Read_Refresh, new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonOrganizationMembership>(Auth, endpoint);

			return newData;
		}
		protected override async Task SubmitData(IJsonOrganizationMembership json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationMembership_Write_Update, new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
	}
}