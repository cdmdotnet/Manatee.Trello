# Getting a known card and updating some of its data

```csharp
var card = factory.Card("[some known ID]");
await card.Refresh();

// basic fields
card.Name = "a new name";
card.Description = "hello";
card.Postion = Position.Top;

// custom fields
var checkBoxField = card.CustomFields.FirstOrDefault(f => f.Type == CustomFieldTypes.CheckBox);
if (checkBoxField.Value)
{
    var numericCustomField = card.CustomFields.FirstOrDefault(f => f.Type == CustomFieldType.Number);
    numericCustomField.Value = 9;
}

// Drop down fields only allow a predetermined set of values.  These values can be found
// in the field's definition, accessible through the board.  While the definition is
// accessible through the field, Trello doesn't send the options during this call.  To
// ensure that the drop down field options are available, it's a good idea to refresh the
// definition before accessing the field itself.  Once this is done, the definition will
// be cached and the options will be available through the field's Definition property.
await card.Board.CustomFieldDefinitions.Refresh();
var dropDownField = card.CustomFields.FirstOrDefault(f => f.Type == CustomFieldType.DropDown);
dropDownField.Value = dropDownField.Definition.Options[2];

// Trello doesn't return a custom field for a card if it doesn't have a value for that field.
// So to set a custom field value on a card that has no value for that field, you have to
// get the field's definition first and call the appropriate setter method.  All setter methods
// are available on all custom field definitions, so be sure you set the right type of data
// for the right type of field.
var textFieldDefinition = card.Board.CustomFieldsDefinitions.FirstOrDefault(f => f.Type == CustomFieldType.Text);
var textField = await textFieldDefinition.SetValueForCard(card, "some text");
```

The above code will consolidate the *basic fields* changes and make a single call to set those properties.  Since the custom fields are different entities, two additional calls will be made to set the respective values.
