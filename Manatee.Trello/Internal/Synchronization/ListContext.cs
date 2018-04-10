using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
			Properties = new Dictionary<string, Property<IJsonList>>
				{
					{
						nameof(List.Board),
						new Property<IJsonList, Board>((d, a) => d.Board?.GetFromCache<Board>(a),
						                               (d, o) =>
							                               {
								                               if (o != null) d.Board = o.Json;
							                               })
					},
					{
						nameof(List.Id),
						new Property<IJsonList, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(List.IsArchived),
						new Property<IJsonList, bool?>((d, a) => d.Closed, (d, o) => d.Closed = o)
					},
					{
						nameof(List.IsSubscribed),
						new Property<IJsonList, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)
					},
					{
						nameof(List.Name),
						new Property<IJsonList, string>((d, a) => d.Name, (d, o) => d.Name = o)
					},
					{
						nameof(List.Position),
						new Property<IJsonList, Position>((d, a) => Position.GetPosition(d.Pos), (d, o) => d.Pos = Position.GetJson(o))
					},
				};
		}
		public ListContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}

		protected override async Task<IJsonList> GetData(CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.List_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonList>(Auth, endpoint, ct);

			return newData;
		}
		protected override async Task SubmitData(IJsonList json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.List_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}
	}
}