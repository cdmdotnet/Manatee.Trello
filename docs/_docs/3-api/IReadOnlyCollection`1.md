---
title: IReadOnlyCollection&lt;T&gt;
category: API
order: 155
---

Provides base functionality for a read-only collection.

**Type Parameter:** T (no constraints)

The type of object contained by the collection.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyCollection&lt;T&gt;

## Properties

### T this[int index] { get; }

Retrieves the item at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The item.

### int? Limit { get; set; }

Indicates the maximum number of items to return.

## Methods

### Task Refresh(CancellationToken ct = default(CancellationToken))

Manually updates the collection&#39;s data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

