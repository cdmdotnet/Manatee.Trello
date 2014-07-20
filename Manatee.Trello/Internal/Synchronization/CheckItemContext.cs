using System.Collections.Generic;
using Manatee.Trello.Internal.DataAccess;
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
					{"Id", new Property<IJsonCheckItem, string>(d => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonCheckItem, string>(d => d.Name, (d, o) => d.Name = o)},
					{"Position", new Property<IJsonCheckItem, Position>(d => d.Pos.HasValue ? new Position(d.Pos.Value) : null, (d, o) => d.Pos = o.Value)},
					{"State", new Property<IJsonCheckItem, CheckItemState>(d => d.State, (d, o) => d.State = o)},
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
			// Checklist should be downloaded already since CheckItem ctor is internal,
			// but allow for the case where it has not been anyway.
			var checkList = TrelloConfiguration.Cache.Find<CheckList>(cl => cl.Id == _ownerId) ?? new CheckList(_ownerId);
			// This may make a call to get the card, but it can't be avoided.  We need its ID.
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Write_Update, new Dictionary<string, object>
				{
					{"_cardId", checkList.Card.Id},
					{"_checkListId", _ownerId},
					{"_id", Data.Id},
				});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}