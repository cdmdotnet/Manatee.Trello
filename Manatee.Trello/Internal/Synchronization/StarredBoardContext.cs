using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class StarredBoardContext : SynchronizationContext<IJsonStarredBoard>
	{
		private readonly string _ownerId;
		private bool _deleted;

		static StarredBoardContext()
		{
			Properties = new Dictionary<string, Property<IJsonStarredBoard>>
				{
					{
						nameof(StarredBoard.Board),
						new Property<IJsonStarredBoard, Board>((d, a) => d.Board?.GetFromCache<Board, IJsonBoard>(a),
						                                       (d, o) => d.Board = o?.Json)
					},
					{
						nameof(StarredBoard.Position),
						new Property<IJsonStarredBoard, Position>((d, a) => Position.GetPosition(d.Pos),
						                                          (d, o) => d.Pos = Position.GetJson(o))
					},
				};
		}
		public StarredBoardContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.StarredBoard_Write_Delete,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_idMember", _ownerId},
					                                     {"_id", Data.Id}
				                                     });
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
			RaiseDeleted();
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.StarredBoard_Read_Refresh,
			                             new Dictionary<string, object> {{"_idMember", _ownerId}, {"_id", Data.Id}});
		}

		protected override async Task<IJsonStarredBoard> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.StarredBoard_Read_Refresh,
				                                     new Dictionary<string, object>
					                                     {
						                                     {"_idMember", _ownerId},
						                                     {"_id", Data.Id}
					                                     });
				var newData = await JsonRepository.Execute<IJsonStarredBoard>(Auth, endpoint, ct);

				MarkInitialized();
				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				_deleted = true;
				return Data;
			}
		}

		protected override async Task SubmitData(IJsonStarredBoard json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.StarredBoard_Write_Update,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_idMember", _ownerId},
					                                     {"_id", Data.Id}
				                                     });
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			Merge(newData);
		}
	}
}
