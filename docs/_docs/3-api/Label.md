---
title: Label
category: API
order: 178
---

A label.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Label

## Properties

### static Manatee.Trello.Label+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [IBoard](../IBoard#iboard) Board { get; }

Gets the board on which the label is defined.

### [LabelColor](../LabelColor#labelcolor)? Color { get; set; }

Gets and sets the color. Use null for no color.

### DateTime CreationDate { get; }

Gets the creation date of the label.

### string Id { get; }

Gets the label&#39;s ID.

### string Name { get; set; }

Gets and sets the label&#39;s name.

### int? Uses { get; }

Gets the number of cards which use this label.

## Methods

### Task Delete(CancellationToken ct)

Deletes the label. All usages of the label will also be removed.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the label from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct)

Refreshes the label data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

