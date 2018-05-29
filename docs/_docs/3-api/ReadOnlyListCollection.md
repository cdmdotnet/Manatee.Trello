# ReadOnlyListCollection

A read-only collection of lists.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IList&gt;
- ReadOnlyListCollection

## Properties

### [IList](IList#ilist) this[string key] { get; }

Retrieves a list which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching list, or null if none found.

#### Remarks

Matches on list ID and name. Comparison is case-sensitive.

## Methods

### void Filter(ListFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.

