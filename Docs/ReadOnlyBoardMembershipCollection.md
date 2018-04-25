# ReadOnlyBoardMembershipCollection

A read-only collection of board memberships.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IBoardMembership&gt;
- ReadOnlyBoardMembershipCollection

## Properties

### [IBoardMembership](IBoardMembership#iboardmembership) this[string key] { get; }

Retrieves a membership which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching membership, or null if none found.

#### Remarks

Matches on membership ID, member ID, BoardMembership. member full name, and member username. Comparison is case-sensitive.

## Methods

### void Filter(MembershipFilter membership)

Adds a filter to the collection.

**Parameter:** membership

The filter value.

### Task Refresh(CancellationToken ct)

Manually updates the collection&#39;s data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task Refresh(CancellationToken ct)

Manually updates the collection&#39;s data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

