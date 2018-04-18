using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class DropDownOptionContext : SynchronizationContext<IJsonCustomDropDownOption>
	{
		private bool _deleted;

		static DropDownOptionContext()
		{
			Properties = new Dictionary<string, Property<IJsonCustomDropDownOption>>
				{
					{
						nameof(DropDownOption.Field),
						new Property<IJsonCustomDropDownOption, CustomFieldDefinition>(
							(d, a) => d.Field.GetFromCache<CustomFieldDefinition, IJsonCustomFieldDefinition>(a),
							(d, o) =>
								{
									if (o != null) d.Field = o.Json;
								})
					},
					{
						nameof(DropDownOption.Id),
						new Property<IJsonCustomDropDownOption, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(DropDownOption.Text),
						new Property<IJsonCustomDropDownOption, string>((d, a) => d.Text,
						                                                (d, o) =>
							                                                {
								                                                if (o != null) d.Text = o;
							                                                })
					},
					{
						nameof(DropDownOption.Color),
						new Property<IJsonCustomDropDownOption, LabelColor?>((d, a) => d.Color,
						                                                     (d, o) =>
							                                                     {
								                                                     if (o != null) d.Color = o;
							                                                     })
					},
					{
						nameof(DropDownOption.Position),
						new Property<IJsonCustomDropDownOption, Position>((d, a) => Position.GetPosition(d.Pos),
						                                                  (d, o) => d.Pos = Position.GetJson(o))
					},
				};
		}

		public DropDownOptionContext(TrelloAuthorization auth)
			: base(auth)
		{
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.CustomFieldDropDownOption_Write_Delete,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_idField", Data.Field.Id},
					                                     {"_id", Data.Id}
				                                     });
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}

		protected override async Task<IJsonCustomDropDownOption> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.CustomFieldDropDownOption_Read_Refresh,
				                                     new Dictionary<string, object>
					                                     {
						                                     {"_idField", Data.Field.Id},
						                                     {"_id", Data.Id}
					                                     });
				var newData = await JsonRepository.Execute<IJsonCustomDropDownOption>(Auth, endpoint, ct);

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				_deleted = true;
				return Data;
			}
		}
	}
}