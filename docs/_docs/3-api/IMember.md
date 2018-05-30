---
title: IMember
category: API
order: 132
---

Represents a member.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IMember

## Properties

### IReadOnlyCollection&lt;[IAction](../IAction#iaction)&gt; Actions { get; }

Gets the collection of actions performed by the member.

### [AvatarSource](../AvatarSource#avatarsource)? AvatarSource { get; }

Gets the source type for the member&#39;s avatar.

### string AvatarUrl { get; }

Gets the URL to the member&#39;s avatar.

### string Bio { get; }

Gets the member&#39;s bio.

### IReadOnlyCollection&lt;[IBoard](../IBoard#iboard)&gt; Boards { get; }

Gets the collection of boards owned by the member.

### IReadOnlyCollection&lt;[ICard](../ICard#icard)&gt; Cards { get; }

Gets the collection of cards assigned to the member.

### DateTime CreationDate { get; }

Gets the creation date of the member.

### string FullName { get; }

Gets the member&#39;s full name.

### string Initials { get; }

Gets or sets the member&#39;s initials.

### bool? IsConfirmed { get; }

Gets whether the member has actually join or has merely been invited (ghost).

### string Mention { get; }

Gets a string which can be used in comments or descriptions to mention this user. The user will receive notification that they&#39;ve been mentioned.

### IReadOnlyCollection&lt;[IOrganization](../IOrganization#iorganization)&gt; Organizations { get; }

Gets the collection of organizations to which the member belongs.

### [MemberStatus](../MemberStatus#memberstatus)? Status { get; }

Gets the member&#39;s online status.

### IEnumerable&lt;string&gt; Trophies { get; }

Gets the collection of trophies earned by the member.

### string Url { get; }

Gets the member&#39;s URL.

### string UserName { get; }

Gets the member&#39;s username.

## Events

### Action&lt;[IMember](../IMember#imember), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the member is updated.

## Methods

### Task Refresh(CancellationToken ct)

Refreshes the member data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

