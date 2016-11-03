using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class ListContext : SynchronizationContext<IJsonList>
	{
		protected override bool IsDataComplete => !Data.Name.IsNullOrWhiteSpace();
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static ListContext()
		{
			_properties = new Dictionary<string, Property<IJsonList>>
				{
					{
						"Board", new Property<IJsonList, Board>((d, a) => d.Board?.GetFromCache<Board>(a),
						                                        (d, o) => { if (o != null) d.Board = o.Json; })
					},
					{"Id", new Property<IJsonList, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsArchived", new Property<IJsonList, bool?>((d, a) => d.Closed, (d, o) => d.Closed = o)},
					{"IsSubscribed", new Property<IJsonList, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)},
					{"Name", new Property<IJsonList, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{
						"Position", new Property<IJsonList, Position>((d, a) => Position.GetPosition(d.Pos), (d, o) => d.Pos = Position.GetJson(o))
					},
				};
		}
		public ListContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}

		protected override IJsonList GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.List_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonList>(Auth, endpoint);

			return newData;
		}
		protected override void SubmitData(IJsonList json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.List_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
	}
}