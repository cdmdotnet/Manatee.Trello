# IOrganizationMembership

Associates a [Member](IOrganizationMembership#imember-member--get-) to an [Organization](Organization#organization) and indicates any permissions the member has in the organization.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IOrganizationMembership

## Properties

### DateTime CreationDate { get; }

Gets the creation date of the membership.

### bool? IsUnconfirmed { get; }

Gets whether the member has accepted the invitation to join Trello.

### [IMember](IMember#imember) Member { get; }

Gets the member.

### [OrganizationMembershipType](OrganizationMembershipType#organizationmembershiptype)? MemberType { get; set; }

Gets the membership&#39;s permission level.

## Events

### Action&lt;IOrganizationMembership, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the membership is updated.

## Methods

### Task Refresh(CancellationToken ct)

Refreshes the organization membership data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

