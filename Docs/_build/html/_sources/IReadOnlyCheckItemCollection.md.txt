# IReadOnlyCheckItemCollection

A read-only collection of checklist items.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyCheckItemCollection

## Properties

### [ICheckItem](ICheckItem#icheckitem) this[string key] { get; }

Retrieves a check list item which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list item, or null if none found.

#### Remarks

Matches on CheckItem.Id and CheckItem.Name. Comparison is case-sensitive.

