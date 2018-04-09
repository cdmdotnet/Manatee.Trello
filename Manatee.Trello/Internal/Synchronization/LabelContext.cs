using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class LabelContext : SynchronizationContext<IJsonLabel>
	{
		private bool _deleted;

		static LabelContext()
		{
			_properties = new Dictionary<string, Property<IJsonLabel>>
				{
					{
						"Board", new Property<IJsonLabel, Board>((d, a) => d.Board?.GetFromCache<Board>(a),
																 (d, o) => d.Board = o?.Json)
					},
					{"Color", new Property<IJsonLabel, LabelColor?>((d, a) => d.Color, (d, o) => d.Color = o)},
					{"Id", new Property<IJsonLabel, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonLabel, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Uses", new Property<IJsonLabel, int?>((d, a) => d.Uses, (d, o) => d.Uses = o)},
				};
		}
		public LabelContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Label_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}

		protected override async Task<IJsonLabel> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Label_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonLabel>(Auth, endpoint, ct);

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
		protected override async Task SubmitData(IJsonLabel json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Label_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}

		protected override bool IsDataComplete => !Data.Name.IsNullOrWhiteSpace();
	}
}