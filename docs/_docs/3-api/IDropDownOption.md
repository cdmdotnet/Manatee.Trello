---
title: IDropDownOption
category: API
order: 85
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

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the drop down option.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the drop down option from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the drop down option data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

