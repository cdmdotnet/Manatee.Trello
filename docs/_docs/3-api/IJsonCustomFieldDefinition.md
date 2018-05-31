---
title: IJsonCustomFieldDefinition
category: API
order: 105
---

Defines the JSON structure for a custom field definition.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCustomFieldDefinition

## Properties

### [IJsonBoard](../IJsonBoard#ijsonboard) Board { get; set; }

Gets or sets the board.

### [IJsonCustomFieldDisplayInfo](../IJsonCustomFieldDisplayInfo#ijsoncustomfielddisplayinfo) Display { get; set; }

Gets or sets the display information.

### string FieldGroup { get; set; }

Gets or sets the field group.

### string Name { get; set; }

Gets or sets the name.

### List&lt;[IJsonCustomDropDownOption](../IJsonCustomDropDownOption#ijsoncustomdropdownoption)&gt; Options { get; set; }

Gets or sets any drop down options.

### [IJsonPosition](../IJsonPosition#ijsonposition) Pos { get; set; }

Gets or sets the position.

### [CustomFieldType](../CustomFieldType#customfieldtype)? Type { get; set; }

Gets or sets the type.

