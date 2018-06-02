---
title: Member
category: API
order: 193
---

Represents a member.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Member

## Constructors

### Member(string id, TrelloAuthorization auth = null)

Creates a new instance of the Manatee.Trello.Member object.

**Parameter:** id

The member&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, Manatee.Trello.TrelloAuthorization.Default will be used.

#### Remarks

The supplied ID can be either the full ID or the username.

## Properties

### static [AvatarSize](../AvatarSize#avatarsize) AvatarSize { get; set; }

Specifies the desired size for avatars. The default is Manatee.Trello.AvatarSize.Large

### static Member.Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [IReadOnlyActionCollection](../IReadOnlyActionCollection#ireadonlyactioncollection) Actions { get; }

Gets the collection of actions performed by the member.

### [AvatarSource](../AvatarSource#avatarsource)? AvatarSource { get; }

Gets the source type for the member&#39;s avatar.

### string AvatarUrl { get; }

Gets the URL to the member&#39;s avatar.

### string Bio { get; }

Gets the member&#39;s bio.

### [IReadOnlyBoardCollection](../IReadOnlyBoardCollection#ireadonlyboardcollection) Boards { get; }

Gets the collection of boards owned by the member.

### [IReadOnlyCardCollection](../IReadOnlyCardCollection#ireadonlycardcollection) Cards { get; }

Gets the collection of cards assigned to the member.

### DateTime CreationDate { get; }

Gets the creation date of the member.

### string FullName { get; }

Gets the member&#39;s full name.

### string Id { get; }

Gets the member&#39;s ID.

### string Initials { get; }

Gets or sets the member&#39;s initials.

### bool? IsConfirmed { get; }

Gets whether the member has actually join or has merely been invited (ghost).

### string Mention { get; }

Gets a string which can be used in comments or descriptions to mention another user. The user will receive notification that they&#39;ve been mentioned.

### [IReadOnlyOrganizationCollection](../IReadOnlyOrganizationCollection#ireadonlyorganizationcollection) Organizations { get; }

Gets the collection of organizations to which the member belongs.

### IReadOnlyCollection&lt;[IStarredBoard](../IStarredBoard#istarredboard)&gt; StarredBoards { get; }

Gets the collection of the member&#39;s board stars.

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

### void ApplyAction(IAction action)

Applies the changes an action represents.

**Parameter:** action

The action.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the member data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

#### Filterpriority

2

