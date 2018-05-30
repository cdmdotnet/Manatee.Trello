using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collectin of boards.
	/// </summary>
	public class ReadOnlyStarredBoardCollection : ReadOnlyCollection<IStarredBoard>
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
	}

	public class StarredBoardCollection : ReadOnlyStarredBoardCollection, IStarredBoardCollection
	{
		internal StarredBoardCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		public async Task<IStarredBoard> Add(IBoard board, Position position = null, CancellationToken ct = default(CancellationToken))
		{
			var error = NotNullRule<IBoard>.Instance.Validate(null, board);
			if (error != null)
				throw new ValidationException<IBoard>(board, new[] {error});
			position = position ?? Position.Bottom;

			var json = TrelloConfiguration.JsonFactory.Create<IJsonStarredBoard>();
			json.Board = TrelloConfiguration.JsonFactory.Create<IJsonBoard>();
			json.Board.Id = board.Id;
			json.Pos = Position.GetJson(position);

			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Write_AddStarredBoard, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			return new StarredBoard(OwnerId, newData, Auth);
		}
	}
}