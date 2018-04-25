# ReadOnlyOrganizationCollection

A read-only collection of organizations.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IOrganization&gt;
- ReadOnlyOrganizationCollection

## Properties

### [IOrganization](IOrganization#iorganization) this[string key] { get; }

Retrieves a organization which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching organization, or null if none found.

#### Remarks

Matches on organization ID, name, and display name. Comparison is case-sensitive.

## Methods

### void Filter(OrganizationFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.

### Task Refresh(CancellationToken ct)

Manually updates the collection&#39;s data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task Refresh(CancellationToken ct)

Manually updates the collection&#39;s data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

