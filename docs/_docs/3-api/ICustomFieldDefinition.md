---
title: ICustomFieldDefinition
category: API
order: 81
---

Represents a custom field definition.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ICustomFieldDefinition

## Properties

### [IBoard](../IBoard#iboard) Board { get; }

Gets the board on which the field is defined.

### [ICustomFieldDisplayInfo](../ICustomFieldDisplayInfo#icustomfielddisplayinfo) DisplayInfo { get; }

Gets display information for the custom field.

### string FieldGroup { get; }

Gets an identifier that groups fields across boards.

### string Name { get; set; }

Gets or sets the name of the field.

### [IDropDownOptionCollection](../IDropDownOptionCollection#idropdownoptioncollection) Options { get; }

Gets drop down options, if applicable.

### [Position](../Position#position) Position { get; set; }

Gets or sets the position of the field.

### [CustomFieldType](../CustomFieldType#customfieldtype)? Type { get; }

Gets the data type of the field.

## Methods

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the field definition.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the custom field definition data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task&lt;ICustomField&lt;double?&gt;&gt; SetValueForCard(ICard card, double? value, CancellationToken ct = default(CancellationToken))

Sets a value for a custom field on a card.

**Parameter:** card

The card on which to set the value.

**Parameter:** value

The vaue to set.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The custom field instance.

### Task&lt;ICustomField&lt;bool?&gt;&gt; SetValueForCard(ICard card, bool? value, CancellationToken ct = default(CancellationToken))

Sets a value for a custom field on a card.

**Parameter:** card

The card on which to set the value.

**Parameter:** value

The vaue to set.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The custom field instance.

### Task&lt;ICustomField&lt;string&gt;&gt; SetValueForCard(ICard card, string value, CancellationToken ct = default(CancellationToken))

Sets a value for a custom field on a card.

**Parameter:** card

The card on which to set the value.

**Parameter:** value

The vaue to set.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The custom field instance.

### Task&lt;ICustomField&lt;IDropDownOption&gt;&gt; SetValueForCard(ICard card, IDropDownOption value, CancellationToken ct = default(CancellationToken))

Sets a value for a custom field on a card.

**Parameter:** card

The card on which to set the value.

**Parameter:** value

The vaue to set.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The custom field instance.

### Task&lt;ICustomField&lt;DateTime?&gt;&gt; SetValueForCard(ICard card, DateTime? value, CancellationToken ct = default(CancellationToken))

Sets a value for a custom field on a card.

**Parameter:** card

The card on which to set the value.

**Parameter:** value

The vaue to set.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The custom field instance.

