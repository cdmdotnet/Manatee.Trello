---
title: ICheckList
category: API
order: 74
---

Represents a checklist.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ICheckList

## Properties

### [IBoard](../IBoard#iboard) Board { get; }

Gets the board on which the checklist belongs.

### [ICard](../ICard#icard) Card { get; }

Gets the card on which the checklist belongs.

### [ICheckItemCollection](../ICheckItemCollection#icheckitemcollection) CheckItems { get; }

Gets the collection of items in the checklist.

### DateTime CreationDate { get; }

Gets the creation date of the checklist.

### [ICheckItem](../ICheckItem#icheckitem) this[string key] { get; }

Retrieves a check list item which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list item, or null if none found.

#### Remarks

Matches on CheckItem.Id and CheckItem.Name. Comparison is case-sensitive.

### [ICheckItem](../ICheckItem#icheckitem) this[int index] { get; }

Retrieves the check list item at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The check list item.

### string Name { get; set; }

Gets the checklist&#39;s name.

### [Position](../Position#position) Position { get; set; }

Gets the checklist&#39;s position.

## Events

### Action&lt;[ICheckList](../ICheckList#ichecklist), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the check list is updated.

## Methods

### Task Delete(CancellationToken ct)

Deletes the checklist.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the checklist from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct)

Refreshes the checklist data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

