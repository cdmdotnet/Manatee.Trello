---
title: ReadOnlyCollection&lt;T&gt;
category: API
order: 229
---

Provides base functionality for a read-only collection.

**Type Parameter:** T (no constraints)

The type of object contained by the collection.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;T&gt;

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

### IEnumerator&lt;T&gt; GetEnumerator()

Returns an enumerator that iterates through the collection.

**Returns:** An enumerator that can be used to iterate through the collection.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Manually updates the collection&#39;s data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

