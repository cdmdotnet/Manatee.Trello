---
title: ReadOnlyActionCollection
category: API
order: 220
---

A read-only collection of actions.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IAction&gt;
- ReadOnlyActionCollection

## Methods

### void Filter(ActionType actionType)

Adds a filter to the collection.

**Parameter:** actionType

The action type.

### void Filter(IEnumerable&lt;ActionType&gt; actionTypes)

Adds a number of filters to the collection.

**Parameter:** actionTypes

A collection of action types.

### void Filter(DateTime? start, DateTime? end)

Adds a date-based filter to the collection.

**Parameter:** start

The start date.

**Parameter:** end

The end date.

