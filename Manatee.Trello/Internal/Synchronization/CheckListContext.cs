using System.Collections.Generic;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CheckListContext : SynchronizationContext<IJsonCheckList>
	{
		private bool _deleted;

		static CheckListContext()
		{
			_properties = new Dictionary<string, Property<IJsonCheckList>>
				{
					{
						"Board", new Property<IJsonCheckList, Board>((d, a) => d.Board.GetFromCache<Board>(a),
						                                             (d, o) => d.Board = o?.Json)
					},
					{
						"Card", new Property<IJsonCheckList, Card>((d, a) => d.Card.GetFromCache<Card>(a),
						                                           (d, o) => d.Card = o?.Json)
					},
					{"CheckItems", new Property<IJsonCheckList, List<IJsonCheckItem>>((d, a) => d.CheckItems, (d, o) => d.CheckItems = o)},
					{"Id", new Property<IJsonCheckList, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonCheckList, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Position", new Property<IJsonCheckList, Position>((d, a) => Position.GetPosition(d.Pos), (d, o) => d.Pos = Position.GetJson(o))},
				};
		}
		public CheckListContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}

		public async Task Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override async Task<IJsonCheckList> GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonCheckList>(Auth, endpoint);

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
		protected override async Task SubmitData(IJsonCheckList json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}