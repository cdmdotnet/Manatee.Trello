# ReadOnlyCheckListCollection

A read-only collection of checklists.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;ICheckList&gt;
- ReadOnlyCheckListCollection

## Properties

### [ICheckList](ICheckList#ichecklist) this[string key] { get; }

Retrieves a check list which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list, or null if none found.

#### Remarks

Matches on checklist ID and name. Comparison is case-sensitive.

## Methods

### Task Refresh(CancellationToken ct)

Manually updates the collection&#39;s data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task Refresh(CancellationToken ct)

Manually updates the collection&#39;s data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

