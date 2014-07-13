using System.Collections.Generic;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardMembershipContext : SynchronizationContext<IJsonBoardMembership>
	{
		private readonly string _ownerId;

		static BoardMembershipContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoardMembership>>
				{
					{"IsDeactivated", new Property<IJsonBoardMembership>(d => d.Deactivated, (d, o) => d.Deactivated = (bool?) o)},
					{
						"Member", new Property<IJsonBoardMembership>(d => TrelloConfiguration.Cache.Find<Member>(b => b.Id == d.Member.Id) ?? new Member(d.Member),
						                                             (d, o) => d.Member = o != null ? ((Member) o).Json : null)
					},
					{"MemberType", new Property<IJsonBoardMembership>(d => d.MemberType.ConvertEnum<BoardMembershipType>(), (d, o) => d.MemberType = ((BoardMembershipType) o).ConvertEnum())},
				};
		}
		public BoardMembershipContext(string id, string ownerId)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}
		protected override IJsonBoardMembership GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.BoardMembership_Read_Refresh, new Dictionary<string, object> {{"_boardId", _ownerId}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonBoardMembership>(TrelloAuthorization.Default, endpoint);
			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.BoardMembership_Write_Update, new Dictionary<string, object> {{"_boardId", _ownerId}, {"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}