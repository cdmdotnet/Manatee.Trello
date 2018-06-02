---
title: ILabel
category: API
order: 131
---

A label.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ILabel

## Properties

### [IBoard](../IBoard#iboard) Board { get; }

Gets the Manatee.Trello.ILabel.Board on which the label is defined.

### [LabelColor](../LabelColor#labelcolor)? Color { get; set; }

Gets and sets the color. Use null for no color.

### DateTime CreationDate { get; }

Gets the creation date of the label.

### string Name { get; set; }

Gets and sets the label&#39;s name.

### int? Uses { get; }

Gets the number of cards which use this label.

## Methods

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the label. All usages of the label will also be removed.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the label from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the label data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

