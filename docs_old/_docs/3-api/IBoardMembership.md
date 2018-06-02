---
title: IBoardMembership
category: API
order: 63
---

Associates a Manatee.Trello.IMember to a Manatee.Trello.IBoard and indicates any permissions the member has on the board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IBoardMembership

## Properties

### DateTime CreationDate { get; }

Gets the creation date of the membership.

### bool? IsDeactivated { get; }

Gets whether the member has accepted the invitation to join Trello.

### [IMember](../IMember#imember) Member { get; }

Gets the member.

### [BoardMembershipType](../BoardMembershipType#boardmembershiptype)? MemberType { get; set; }

Gets the membership&#39;s permission level.

## Events

### Action&lt;[IBoardMembership](../IBoardMembership#iboardmembership), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the membership is updated.

## Methods

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the board membership data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

