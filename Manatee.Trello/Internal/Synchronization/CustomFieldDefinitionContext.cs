using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CustomFieldDefinitionContext : SynchronizationContext<IJsonCustomFieldDefinition>
	{
		public ReadOnlyDropDownOptionCollection DropDownOptions { get; }

		static CustomFieldDefinitionContext()
		{
			Properties = new Dictionary<string, Property<IJsonCustomFieldDefinition>>
				{
					{
						nameof(CustomFieldDefinition.Board),
						new Property<IJsonCustomFieldDefinition, Board>((d, a) => d.Board?.GetFromCache<Board, IJsonBoard>(a),
						                                                (d, o) =>
							                                                {
								                                                if (o != null) d.Board = o.Json;
							                                                })
					},
					{
						nameof(CustomFieldDefinition.FieldGroup),
						new Property<IJsonCustomFieldDefinition, string>((d, a) => d.FieldGroup,
						                                                 (d, o) =>
							                                                 {
								                                                 if (o != null) d.FieldGroup = o;
							                                                 })
					},
					{
						nameof(CustomFieldDefinition.Id),
						new Property<IJsonCustomFieldDefinition, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(CustomFieldDefinition.Name),
						new Property<IJsonCustomFieldDefinition, string>((d, a) => d.Name,
						                                                 (d, o) =>
							                                                 {
								                                                 if (o != null) d.Name = o;
							                                                 })
					},
					{
						nameof(CustomFieldDefinition.Position),
						new Property<IJsonCustomFieldDefinition, Position>((d, a) => Position.GetPosition(d.Pos),
						                                                   (d, o) => d.Pos = Position.GetJson(o))
					},
					{
						nameof(CustomFieldDefinition.Type),
						new Property<IJsonCustomFieldDefinition,CustomFieldType?>((d,a) => d.Type,
							(d, o) =>
								{
									if (o != null) d.Type = o;
								})
					},
				};
		}

		public CustomFieldDefinitionContext(TrelloAuthorization auth)
			: base(auth)
		{
			DropDownOptions = new ReadOnlyDropDownOptionCollection(() => Data.Id, auth);
		}

		protected override IEnumerable<string> MergeDependencies(IJsonCustomFieldDefinition json)
		{
			var properties = new List<string>();

			if (json.Options != null)
			{
				DropDownOptions.Update(json.Options.Select(a => a.GetFromCache<DropDownOption, IJsonCustomDropDownOption>(Auth)));
				properties.Add(nameof(Board.Cards));
			}

			return properties;
		}
	}
}
