using System;
using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CustomFieldContext : SynchronizationContext<IJsonCustomField>
	{
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
						                                      (d, o) =>
							                                      {
								                                      if (o != null) d.Checked = o;
							                                      })
					},
					{
						nameof(IJsonCustomField.Date),
						new Property<IJsonCustomField, DateTime?>((d, a) => d.Date,
						                                          (d, o) =>
							                                          {
								                                          if (o != null) d.Date = o;
							                                          })
					},
					{
						nameof(CustomField.Id),
						new Property<IJsonCustomField, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(IJsonCustomField.Number),
						new Property<IJsonCustomField, double?>((d, a) => d.Number,
						                                        (d, o) =>
							                                        {
								                                        if (o != null) d.Number = o;
							                                        })
					},
					{
						nameof(IJsonCustomField.Text),
						new Property<IJsonCustomField, string>((d, a) => d.Text,
						                                       (d, o) =>
							                                       {
								                                       if (o != null) d.Text = o;
							                                       })
					},
					{
						nameof(IJsonCustomField.Selected),
						new Property<IJsonCustomField, DropDownOption>((d, a) => d.Selected.GetFromCache<DropDownOption>(a),
						                                                          (d, o) =>
							                                                          {
								                                                          if (o != null) d.Selected = o.Json;
							                                                          })
					},
				};
		}

		public CustomFieldContext(TrelloAuthorization auth)
			: base(auth)
		{
		}
	}
}