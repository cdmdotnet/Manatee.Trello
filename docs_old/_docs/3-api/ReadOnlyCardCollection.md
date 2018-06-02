---
title: ReadOnlyCardCollection
category: API
order: 226
---

A read-only collection of cards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;ICard&gt;
- ReadOnlyCardCollection

## Properties

### [ICard](../ICard#icard) this[string key] { get; }

Retrieves a card which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching card, or null if none found.

#### Remarks

Matches on card ID and name. Comparison is case-sensitive.

## Methods

### void Filter(CardFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.

### void Filter(DateTime? start, DateTime? end)

Adds a filter to the collection based on start and end date.

**Parameter:** start

The start date.

**Parameter:** end

The end date.

