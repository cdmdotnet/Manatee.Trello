---
title: IDropDownOption
category: API
order: 82
---

Represents a custom field drop down option.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IDropDownOption

## Properties

### [LabelColor](../LabelColor#labelcolor)? Color { get; }

Gets the option color.

### [ICustomFieldDefinition](../ICustomFieldDefinition#icustomfielddefinition) Field { get; }

Gets the custom field definition that defines this option.

### [Position](../Position#position) Position { get; }

Gets the option position.

### string Text { get; }

Gets the option text.

## Methods

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

