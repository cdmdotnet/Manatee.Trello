---
title: CardLabelCollection
category: API
order: 29
---

A collection of labels for cards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;ILabel&gt;
- CardLabelCollection

## Methods

### Task Add(ILabel label, CancellationToken ct = default(CancellationToken))

Adds a label to the collection.

**Parameter:** label

The label to add.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task Remove(ILabel label, CancellationToken ct = default(CancellationToken))

Removes a label from the collection.

**Parameter:** label

The label to add.

**Parameter:** ct

(Optional) A cancellation token for async processing.

