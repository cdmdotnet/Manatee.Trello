using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class DropDownOptionContext : DeletableSynchronizationContext<IJsonCustomDropDownOption>
	{
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
					{
						nameof(IJsonCustomDropDownOption.ValidForMerge),
						new Property<IJsonCustomDropDownOption, bool>((d, a) => d.ValidForMerge, (d, o) => d.ValidForMerge = o, true)
					},
				};
		}

		public DropDownOptionContext(IJsonCustomDropDownOption data, TrelloAuthorization auth, bool created)
			: base(auth)
		{
			Data.Id = data.Id;
			Merge(data);

			Deleted = created;
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.CustomFieldDropDownOption_Read_Refresh,
			                             new Dictionary<string, object>
				                             {
					                             {"_idField", Data.Field.Id},
					                             {"_id", Data.Id}
				                             });
		}

		protected override Endpoint GetDeleteEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.CustomFieldDropDownOption_Write_Delete,
			                             new Dictionary<string, object>
				                             {
					                             {"_idField", Data.Field.Id},
					                             {"_id", Data.Id}
				                             });
		}
	}
}