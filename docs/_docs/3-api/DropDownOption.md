---
title: DropDownOption
category: API
order: 49
---

Represents an option for a custom selection field.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- DropDownOption

## Properties

### [LabelColor](../LabelColor#labelcolor)? Color { get; }

Gets the option color.

### [ICustomFieldDefinition](../ICustomFieldDefinition#icustomfielddefinition) Field { get; }

Gets the custom field definition that defines this option.

### string Id { get; }

Gets an ID on which matching can be performed.

### [Position](../Position#position) Position { get; }

Gets the option position.

### string Text { get; }

Gets the option text.

## Methods

### static [IDropDownOption](../IDropDownOption#idropdownoption) Create(string text, LabelColor color)

Creates a new drop down option.

**Parameter:** text

The text.

**Parameter:** color

(Optional) The label color.

**Returns:** A new drop down option.

#### Remarks

This object will not update. It is intended for adding new options to custom drop down fields.

### Task Delete(CancellationToken ct)

Deletes the drop down option.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the drop down option from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct)

Refreshes the drop down option data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

