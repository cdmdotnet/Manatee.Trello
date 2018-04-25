# BoardMembership

Represents the permission level a member has on a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- BoardMembership

## Properties

### DateTime CreationDate { get; }

Gets the creation date of the membership.

### string Id { get; }

Gets the membership definition&#39;s ID.

### bool? IsDeactivated { get; }

Gets whether the member has accepted the invitation to join Trello.

### [IMember](IMember#imember) Member { get; }

Gets the member.

### [BoardMembershipType](BoardMembershipType#boardmembershiptype)? MemberType { get; set; }

Gets the membership&#39;s permission level.

## Events

### Action&lt;IBoardMembership, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the membership is updated.

## Methods

### Task Refresh(CancellationToken ct)

Refreshes the board membership data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

