---
title: IReadOnlyCardCollection
category: API
order: 96
---

# IReadOnlyCardCollection

A read-only collection of cards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyCardCollection

## Properties

### [ICard](ICard#icard) this[string key] { get; }

Retrieves a card which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching card, or null if none found.

#### Remarks

Matches on Card.Id and Card.Name. Comparison is case-sensitive.

## Methods

### void Filter(CardFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.

