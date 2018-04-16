using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class DropDownOptionContext : SynchronizationContext<IJsonCustomDropDownOption>
	{
		static DropDownOptionContext()
		{
			Properties = new Dictionary<string, Property<IJsonCustomDropDownOption>>
				{
					{
						nameof(DropDownOption.Field),
						new Property<IJsonCustomDropDownOption, CustomField>((d, a) => d.Field.GetFromCache<CustomField, IJsonCustomField>(a),
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
	}
}