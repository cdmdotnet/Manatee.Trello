---
title: IJsonCustomField
category: API
order: 104
---

Defines the JSON structure for a custom field instance.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCustomField

## Properties

### bool? Checked { get; set; }

Gets or sets the checked.

### DateTime? Date { get; set; }

Gets or sets the date.

### [IJsonCustomFieldDefinition](../IJsonCustomFieldDefinition#ijsoncustomfielddefinition) Definition { get; set; }

Gets or sets the custom field definition.

### double? Number { get; set; }

Gets or sets the number.

### [IJsonCustomDropDownOption](../IJsonCustomDropDownOption#ijsoncustomdropdownoption) Selected { get; set; }

Gets or sets the selected drop down option.

### string Text { get; set; }

Gets or sets the text.

### [CustomFieldType](../CustomFieldType#customfieldtype) Type { get; set; }

Gets or sets the data type.

