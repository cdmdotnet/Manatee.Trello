using System.Collections.Generic;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CheckItemContext : SynchronizationContext<IJsonCheckItem>
	{
		private readonly string _ownerId;
		static CheckItemContext()
		{
			_properties = new Dictionary<string, Property<IJsonCheckItem>>
				{
					{"Name", new Property<IJsonCheckItem>(d => d.Name, (d, o) => d.Name = (string) o)},
					{"Position", new Property<IJsonCheckItem>(d => d.Pos.HasValue ? new Position(d.Pos.Value) : null, (d, o) => d.Pos = ((Position) o).Value)},
					{"State", new Property<IJsonCheckItem>(d => d.State, (d, o) => d.State = (string) o)},
				};
		}
		public CheckItemContext(string id, string ownerId)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		public void Delete()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Write_Delete, new Dictionary<string, object> {{"_checklistId", _ownerId}, {"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);
		}

		protected override IJsonCheckItem GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonCheckItem>(TrelloAuthorization.Default, endpoint);

			return newData;
		}
		protected override void SubmitData()
		{
			// TODO: determine how to get the card and checkList IDs into the query string
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}