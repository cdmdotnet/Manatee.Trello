---
title: Power-Ups
category: Version 2.x
order: 3
---

Some time ago, Trello announced a new feature that enables users to leverage custom functionality to make their board more powerful.  They called this feature [PowerUps](https://trello.com/power-ups).

To support this new feature and allow anyone to create their own power-up, they expose only the raw data.  It's up to the power-up creator to organize and use the data.  As a result, each power-up can be uniquely organized, making a generic Manatee.Trello power-up implementation impossible.  Due to this, power-up implementations will be published separately from the primary Nuget package.

Current implementations include:

- [Custom Fields](https://www.nuget.org/packages/Manatee.Trello.CustomFields/)
- more to come...

This section describes the base functionality in Manatee.Trello for implementing power-ups, using the Custom Fields power-up (provided by Manatee.Trello.CustomFields) as an example.

# Data organization

Trello exposes data in two forms: the power-up definition and the associated data.

## Power-up definitions

Each board determines the set of active power-ups.  Boards under free accounts can have only a single power-up active at a time, while paid accounts can have multiple.  Definitions are available on the board via the `Board.PowerUps` property.

Unless a specific implementation has been registered (see *Creating your own power-up*) this will return only `UnknownPowerUp` instances.  Once a power-up is registered, it will start returning that type of power-up if it's available.

A power-up definition specifies the power-up global ID (unique across all boards), the name, and a URL to a JSON object containing more meta-data regarding the power-up.

## Power-up data

Power-ups can enhance the functionality of boards and cards.  Each of these objects in Manatee.Trello exposes a `PowerUpData` property that can be used to get a collection of data objects (`PowerUpData`) that reveals the enhanced data for that item.  (Trello says that power-ups can enhance organizations, too, but they don't have that exposed.  The `Organization.PowerUpData` property will therefore likely throw an exception.  I have it there for completeness.)

Each data object defines the specific ID for that instance, the ID of the power-up which defined it, and the value (as a JSON string) of the data.

This is as implementation-specific as Manatee.Trello can be.  Making use of this data requires details very specific to a particular power-up.

# Creating your own power-up

The Custom Fields power-up has already been implemented.  It will serve as our example.  Note that each implementation will function slightly differently, but this should give you a general idea of how to proceed.

To do this, you'll need to get a hold of the JSON response values directly from the Trello API.

## Implementing the definition

Every power-up implementation is required to define its unique ID and a constructor that takes an `IJsonPowerUp` object and a `TrelloAuthorization` object.  All power-ups should also register themselves with the `TrelloConfiguration` static class (it makes things easier for the client).  Note that these do not have to be public members.

The `PowerUpBase` abstract class has been provided by Manatee.Trello to get you started and provide some basic power-up functionality.

	public class CustomFieldsPowerUp : PowerUpBase
	{
		internal const string PluginId = "56d5e249a98895a9797bebb9";

		private static bool _isRegistered;

		private CustomFieldsPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth) {}

		internal static void Register()
		{
			if (!_isRegistered)
			{
				_isRegistered = true;
				TrelloConfiguration.RegisterPowerUp(PluginId, (j, a) => new CustomFieldsPowerUp(j, a));
			}
		}
	}

That's about it for the definition.  Now, the `Board.PowerUps` property will return an instance of this class (instead of `UnknownPowerUp`) if the power-up is active.  But just having the power-up definition doesn't really help much, so let's take a look at getting some data.

## Implementing the data

We start by looking at the data that is returned when we call all of the power-up-related properties.

> **NOTE** This data assumes that Custom Fields is the only power-up active on the board.  Otherwise, there will be an entry for each plugin.
> **NOTE** To create this data, I had to add the Custom Fields plugin to a board, define a field of each type, and then apply data to a card for each field.

- `Board.PowerUps`

        [
            {
                "id": "56d5e249a98895a9797bebb9",
                "name": "Custom Fields",
                "public": true,
                "url": "https://card-fields.trello.services/manifest.json"
            }
        ]
- `Board.PowerUpData`

        [
            {
                "id": "57f4bfeccccb2628768688be",
                "idPlugin": "56d5e249a98895a9797bebb9",
                "scope": "board",
                "idModel": "51478f6469fd3d9341001dae",
                "value": "{\"fields\":[{\"n\":\"MyNumberField\",\"t\":1,\"b\":1,\"id\":\"VHHdzCU0-ROR8bF\"},{\"n\":\"MyDateField\",\"t\":0,\"id\":\"VHHdzCU0-ITIYAU\"},{\"n\":\"MyDateField\",\"t\":3,\"id\":\"VHHdzCU0-dE8xFw\"},{\"n\":\"MyCheckboxField\",\"t\":2,\"id\":\"VHHdzCU0-o8TMjq\"},{\"n\":\"MyDropdownField\",\"t\":4,\"o\":[\"One\",\"Two\",\"Three\",\"Text\"],\"id\":\"VHHdzCU0-Y1ylHE\"}],\"btn\":\"Metadata\"}",
                "access": "shared"
            }
        ]
- `Card.PowerUpData`

        [
            {
                "id": "57f4bffd0a0a813388367a0b",
                "idPlugin": "56d5e249a98895a9797bebb9",
                "scope": "card",
                "idModel": "51478f6ce7d2d11751005681",
                "value": "{\"fields\":{\"VHHdzCU0-ROR8bF\":\"4\",\"VHHdzCU0-ITIYAU\":\"nothing to see here\",\"VHHdzCU0-dE8xFw\":\"2016-11-15T12:00:00+13:00\",\"VHHdzCU0-o8TMjq\":true,\"VHHdzCU0-Y1ylHE\":3}}",
                "access": "shared"
            }
        ]

If you stare at this data long enough, and through squinty eyes, you'll be able to see the pattern:

- The `Board.PowerUps` property give us the data to build the `CustomFieldsPowerUp` instance from the last section.
- The `Board.PowerUpData` property defines the fields and the text that appears on the button inside a card.  Each field has and ID (`id`), a name (`n`), and a data type (`t`).  Dropdown fields also have a property that defines the available options (`o`).
- The `Card.PowerUpData` property defines the field data for that card.  Note that numbers, text, and dates all come in as text, which booleans show as `bool` and dropdown data appears as an integer indicating the index of the selected option.

So all we have to do is link up the field definitions with the field data.  To start, we model the data.

    // this models the Board.PowerUpData data.
	public class CustomFieldsSettings
	{
		public string ButtonText { get; private set; }
		public IEnumerable<CustomFieldDefinition> Fields { get; private set; }
        
        ... // I have some JSON serialization details as well.
	}

    // This models each field definition.
	public class CustomFieldDefinition : IJsonSerializable
	{
		internal string Id { get; private set; }
		public string Name { get; private set; }
		public FieldType Type { get; private set; }
		public bool ShowBadge { get; private set; }
		public IEnumerable<string> DropdownOptions { get; private set; }

        ... // more JSON serialization stuff.
	}

    // This models the data for a field.
	public class CustomFieldData
	{
		internal string Id { get; set; }
		public string Name { get; internal set; }
		public FieldType Type { get; internal set; }
		public string Value { get; internal set; }
	}

To get the data (in a convenient fashion) we create a couple extension methods:  one that works on boards and one on cards.

	public static class CustomFieldExtensions
	{
		private static readonly JsonSerializer Serializer = new JsonSerializer();

		public static CustomFieldsSettings CustomFieldsSettings(this Board board)
		{
			CustomFieldsPowerUp.Register();
			var data = board.PowerUpData.FirstOrDefault(d => d.PluginId == CustomFieldsPowerUp.PluginId);
			if (data == null) return null;

			var json = JsonValue.Parse(data.Value);
			var settings = Serializer.Deserialize<CustomFieldsSettings>(json);
			return settings;
		}
		public static IEnumerable<CustomFieldData> CustomFields(this Card card)
		{
			CustomFieldsPowerUp.Register();
			var data = card.PowerUpData.FirstOrDefault(d => d.PluginId == CustomFieldsPowerUp.PluginId);
			if (data == null) return null;
			// This will return null if the power-up isn't registered.
			var powerUp = card.Board.TryGetPowerUp();
			if (powerUp == null) return null;
			var json = JsonValue.Parse(data.Value);
			var fieldData = json.Object.TryGetObject("fields").Select(d => new CustomFieldData
				                                                          {
					                                                          Id = d.Key,
					                                                          Value = d.Value.Type == JsonValueType.String
						                                                                  ? d.Value.String
						                                                                  : d.Value.ToString()
				                                                          });
			var fieldSettings = card.Board.CustomFieldsSettings();
			fieldData = fieldSettings.Fields.Join(fieldData,
			                                      f => f.Id,
			                                      d => d.Id,
			                                      (f, d) =>
				                                      {
					                                      d.Name = f.Name;
					                                      d.Type = f.Type;
					                                      return d;
				                                      });

			return fieldData;
		}
	}

> **NOTE** The `TryGetPowerUp()` method is an extension on the board that caches the power-up per board.  It just helps reduces the number of calls.

Now the client can simply call `card.CustomFields()` to get all of the custom field data.

Ideally other power-up implementations will follow the same basic concept: creating a `PowerUpBase` derivative and extension methods to access the data.  The hard part is figuring out the data.