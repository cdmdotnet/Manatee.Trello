using System.Collections.Generic;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CheckListContext : SynchronizationContext<IJsonCheckList>
	{
		static CheckListContext()
		{
			_properties = new Dictionary<string, Property<IJsonCheckList>>
				{
					{
						"Board", new Property<IJsonCheckList, Board>(d => TrelloConfiguration.Cache.Find<Board>(b => b.Id == d.Board.Id) ?? new Board(d.Board.Id),
						                                             (d, o) => d.Board = o != null ? (o).Json : null)
					},
					{
						"Card", new Property<IJsonCheckList, Card>(d => TrelloConfiguration.Cache.Find<Card>(b => b.Id == d.Card.Id) ?? new Card(d.Card.Id),
						                                           (d, o) => d.Card = o != null ? (o).Json : null)
					},
					{"Id", new Property<IJsonCheckList, string>(d => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonCheckList, string>(d => d.Name, (d, o) => d.Name = o)},
					{"Position", new Property<IJsonCheckList, Position>(d => d.Pos.HasValue ? new Position(d.Pos.Value) : null, (d, o) => d.Pos = (o).Value)},
				};
		}
		public CheckListContext(string id)
		{
			Data.Id = id;
		}

		public void Delete()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);
		}

		protected override IJsonCheckList GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonCheckList>(TrelloAuthorization.Default, endpoint);

			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}