---
title: ICheckItem
category: API
order: 72
---

Represents a checklist item.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ICheckItem

## Properties

### [ICheckList](../ICheckList#ichecklist) CheckList { get; set; }

Gets or sets the checklist to which the item belongs.

#### Remarks

Trello only supports moving a check item between lists on the same card.

### DateTime CreationDate { get; }

Gets the creation date of the checklist item.

### string Name { get; set; }

Gets or sets the checklist item&#39;s name.

### [Position](../Position#position) Position { get; set; }

Gets or sets the checklist item&#39;s position.

### [CheckItemState](../CheckItemState#checkitemstate)? State { get; set; }

Gets or sets the checklist item&#39;s state.

## Events

### Action&lt;[ICheckItem](../ICheckItem#icheckitem), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the checklist item is updated.

## Methods

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the checklist item.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the checklist item from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct = default(CancellationToken))

Refreshes the checklist item data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

