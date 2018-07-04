using System.Collections.Generic;
using System.Threading;
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
			Properties = new Dictionary<string, Property<IJsonOrganizationMembership>>
				{
					{
						nameof(OrganizationMembership.Id),
						new Property<IJsonOrganizationMembership, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(OrganizationMembership.IsUnconfirmed),
						new Property<IJsonOrganizationMembership, bool?>((d, a) => d.Unconfirmed, (d, o) => d.Unconfirmed = o)
					},
					{
						nameof(OrganizationMembership.Member),
						new Property<IJsonOrganizationMembership, Member>((d, a) => d.Member.GetFromCache<Member, IJsonMember>(a),
						                                                  (d, o) => d.Member = o?.Json)
					},
					{
						nameof(OrganizationMembership.MemberType),
						new Property<IJsonOrganizationMembership, OrganizationMembershipType?>(
							(d, a) => d.MemberType, (d, o) => d.MemberType = o)
					},
				};
		}
		public OrganizationMembershipContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.OrganizationMembership_Read_Refresh,
			                             new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
		}
		protected override async Task<IJsonOrganizationMembership> GetData(CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationMembership_Read_Refresh,
			                                     new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonOrganizationMembership>(Auth, endpoint, ct);

			return newData;
		}
		protected override async Task SubmitData(IJsonOrganizationMembership json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationMembership_Write_Update,
			                                     new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}
	}
}