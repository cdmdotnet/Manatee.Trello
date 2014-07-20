using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyListCollection : ReadOnlyCollection<List>
	{
		internal ReadOnlyListCollection(string ownerId)
			: base(ownerId) { }

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Lists, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonList>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => TrelloConfiguration.Cache.Find<List>(c => c.Id == jc.Id) ?? new List(jc, true)));
		}
	}

	public class ListCollection : ReadOnlyListCollection
	{
		internal ListCollection(string ownerId)
			: base(ownerId) { }

		public List Add(string name)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonList>();
			json.Name = name;
			json.Board = TrelloConfiguration.JsonFactory.Create<IJsonBoard>();
			json.Board.Id = OwnerId;

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_AddList);
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new List(newData, true);
		}
	}
}