using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CustomFieldContext : SynchronizationContext<IJsonCustomField>
	{
		private readonly string _ownerId;

		static CustomFieldContext()
		{
			Properties = new Dictionary<string, Property<IJsonCustomField>>
				{
					{
						nameof(CustomField.Definition),
						new Property<IJsonCustomField, CustomFieldDefinition>(
							(d, a) => d.Definition.GetFromCache<CustomFieldDefinition, IJsonCustomFieldDefinition>(a),
							(d, o) =>
								{
									if (o != null) d.Definition = o.Json;
								})
					},
					{
						nameof(IJsonCustomField.Checked),
						new Property<IJsonCustomField, bool?>((d, a) => d.Checked,
						                                      (d, o) => d.Checked = o)
					},
					{
						nameof(IJsonCustomField.Date),
						new Property<IJsonCustomField, DateTime?>((d, a) => d.Date,
						                                          (d, o) => d.Date = o)
					},
					{
						nameof(CustomField.Id),
						new Property<IJsonCustomField, string>((d, a) => d.Id,
						                                       (d, o) => d.Id = o)
					},
					{
						nameof(IJsonCustomField.Number),
						new Property<IJsonCustomField, double?>((d, a) => d.Number,
						                                        (d, o) => d.Number = o)
					},
					{
						nameof(IJsonCustomField.Text),
						new Property<IJsonCustomField, string>((d, a) => d.Text,
						                                       (d, o) => d.Text = o)
					},
					{
						nameof(IJsonCustomField.Selected),
						new Property<IJsonCustomField, DropDownOption>((d, a) => d.Selected.GetFromCache<DropDownOption>(a),
						                                               (d, o) => d.Selected = o?.Json)
					},
					{
						nameof(IJsonCustomField.Type),
						new Property<IJsonCustomField, CustomFieldType>((d, a) => d.Type,
						                                                (d, o) => d.Type = o)
					},
				};
		}

		public CustomFieldContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		protected override async Task SubmitData(IJsonCustomField json, CancellationToken ct)
		{
			var parameter = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			parameter.Object = json;
			json.Type = Data.Type;
			var endpoint = EndpointFactory.Build(EntityRequestType.CustomField_Write_Update, new Dictionary<string, object>
				{
					{"_cardId", _ownerId},
					{"_id", Data.Definition.Id},
				});
			var newData = await JsonRepository.Execute<IJsonParameter, IJsonCustomField>(Auth, endpoint, parameter, ct);
			Merge(newData);
		}
	}
}