using System.Collections.Generic;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
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
						"Board", new Property<IJsonCheckList>(d => TrelloConfiguration.Cache.Find<Board>(b => b.Id == d.IdBoard) ?? new Board(d.IdBoard),
						                                      (d, o) => d.IdBoard = o != null ? ((Board) o).Id : null)
					},
					{
						"Card", new Property<IJsonCheckList>(d => TrelloConfiguration.Cache.Find<Card>(b => b.Id == d.IdCard) ?? new Card(d.IdCard),
						                                     (d, o) => d.IdCard = o != null ? ((Card) o).Id : null)
					},
					{"Name", new Property<IJsonCheckList>(d => d.Name, (d, o) => d.Name = (string) o)},
					{"Position", new Property<IJsonCheckList>(d => d.Pos.HasValue ? new Position(d.Pos.Value) : null, (d, o) => d.Pos = ((Position) o).Value)},
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