using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardMembershipContext : SynchronizationContext<IJsonBoardMembership>
	{
		private readonly string _ownerId;

		static BoardMembershipContext()
		{
			Properties = new Dictionary<string, Property<IJsonBoardMembership>>
				{
					{
						nameof(BoardMembership.Id),
						new Property<IJsonBoardMembership, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(BoardMembership.IsDeactivated),
						new Property<IJsonBoardMembership, bool?>((d, a) => d.Deactivated, (d, o) => d.Deactivated = o)
					},
					{
						nameof(BoardMembership.Member),
						new Property<IJsonBoardMembership, Member>((d, a) => d.Member.GetFromCache<Member, IJsonMember>(a),
						                                           (d, o) => d.Member = o?.Json)
					},
					{
						nameof(BoardMembership.MemberType),
						new Property<IJsonBoardMembership, BoardMembershipType?>((d, a) => d.MemberType, (d, o) => d.MemberType = o)
					},
				};
		}
		public BoardMembershipContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}
		protected override async Task<IJsonBoardMembership> GetData(CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.BoardMembership_Read_Refresh,
			                                     new Dictionary<string, object> {{"_boardId", _ownerId}, {"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonBoardMembership>(Auth, endpoint, ct);

			return newData;
		}
		protected override async Task SubmitData(IJsonBoardMembership json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.BoardMembership_Write_Update,
			                                     new Dictionary<string, object> {{"_boardId", _ownerId}, {"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}
	}
}