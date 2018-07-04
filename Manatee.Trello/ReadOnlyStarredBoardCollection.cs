using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Eventing;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of boards.
	/// </summary>
	public class ReadOnlyStarredBoardCollection : ReadOnlyCollection<IStarredBoard>,
	                                              IHandle<EntityDeletedEvent<IJsonStarredBoard>>
	{
		internal ReadOnlyStarredBoardCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			IncorporateLimit();

			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_StarredBoards, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonStarredBoard>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jb =>
				{
					var board = jb.GetFromCache<StarredBoard, IJsonStarredBoard>(Auth);
					board.Json = jb;
					return board;
				}));
		}

		void IHandle<EntityDeletedEvent<IJsonStarredBoard>>.Handle(EntityDeletedEvent<IJsonStarredBoard> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}