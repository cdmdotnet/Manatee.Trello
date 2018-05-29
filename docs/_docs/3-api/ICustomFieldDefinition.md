---
title: ICustomFieldDefinition
category: API
order: 66
---

# ICustomFieldDefinition

Represents a custom field definition.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ICustomFieldDefinition

## Properties

### [IBoard](IBoard#iboard) Board { get; }

Gets the board on which the field is defined.

### string FieldGroup { get; }

Gets an identifier that groups fields across boards.

### string Name { get; set; }

Gets or sets the name of the field.

### [IDropDownOptionCollection](IDropDownOptionCollection#idropdownoptioncollection) Options { get; }

Gets drop down options, if applicable.

### [Position](Position#position) Position { get; set; }

Gets or sets the position of the field.

### [CustomFieldType](CustomFieldType#customfieldtype)? Type { get; }

Gets the data type of the field.

## Methods

### Task Delete(CancellationToken ct)

Deletes the field definition.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task Refresh(CancellationToken ct)

Refreshes the custom field definition data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task&lt;ICustomField&lt;string&gt;&gt; SetValueForCard(ICard card, string value, CancellationToken ct)

Sets a value for a custom field on a card.

**Parameter:** card

The card on which to set the value.

**Parameter:** value

The vaue to set.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The custom field instance.

### Task&lt;ICustomField&lt;IDropDownOption&gt;&gt; SetValueForCard(ICard card, IDropDownOption value, CancellationToken ct)

Sets a value for a custom field on a card.

**Parameter:** card

The card on which to set the value.

**Parameter:** value

The vaue to set.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The custom field instance.

