---
title: Members
category: Version 2.x - API
order: 9
---

> **NOTICE** In migrating to this new documentation, many (if not all) of the links are broken.  Please use the sidebar on the left for navigation.

# Member

Represents a member.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Member

## Constructors

### Member(string id, TrelloAuthorization auth)

Creates a new instance of the [Member](/API-Members#member) object.

**Parameter:** id

The member&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](/API-Configuration#static-trelloauthorization-default--get-) will be used.

#### Remarks

The supplied ID can be either the full ID or the username.

## Properties

### static Manatee.Trello.Member+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### static [Me](/API-Members#me) Me { get; }

Returns the [Member](/API-Members#member) associated with the default [UserToken](/API-Configuration#string-usertoken--get-set-).

### [ReadOnlyActionCollection](/API-Actions#readonlyactioncollection) Actions { get; }

Gets the collection of actions performed by the member.

### [AvatarSource](/API-Members#avatarsource)? AvatarSource { get; }

Gets the source type for the member&#39;s avatar.

### string AvatarUrl { get; }

Gets the URL to the member&#39;s avatar.

### string Bio { get; }

Gets the member&#39;s bio.

### [ReadOnlyBoardCollection](/API-Boards#readonlyboardcollection) Boards { get; }

Gets the collection of boards owned by the member.

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

### [ReadOnlyOrganizationCollection](/API-Organizations#readonlyorganizationcollection) Organizations { get; }

Gets the collection of organizations to which the member belongs.

### [MemberStatus](/API-Members#memberstatus)? Status { get; }

Gets the member&#39;s online status.

### IEnumerable&lt;string&gt; Trophies { get; }

Gets the collection of trophies earned by the member.

### string Url { get; }

Gets the member&#39;s URL.

### string UserName { get; }

Gets the member&#39;s username.

## Events

### Action&lt;Member, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the member is updated.

## Methods

### void ApplyAction(Action action)

Applies the changes an action represents.

**Parameter:** action

The action.

### void Refresh()

Marks the member to be refreshed the next time data is accessed.

### string ToString()

Returns the [FullName](/API-Members#string-fullname--get-).

**Returns:** A string that represents the current object.

# Me

Represents the current member.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Member
- Me

## Properties

### [AvatarSource](/API-Members#avatarsource)? AvatarSource { get; set; }

Gets or sets the source type for the member&#39;s avatar.

### string Bio { get; set; }

Gets or sets the member&#39;s bio.

### [BoardCollection](/API-Boards#boardcollection) Boards { get; }

Gets the collection of boards owned by the member.

### string Email { get; set; }

Gets or sets the member&#39;s email.

### string FullName { get; set; }

Gets or sets the member&#39;s full name.

### string Initials { get; set; }

Gets or sets the member&#39;s initials.

### [ReadOnlyNotificationCollection](/API-Notifications#readonlynotificationcollection) Notifications { get; }

Gets the collection of notificaitons for the member.

### [OrganizationCollection](/API-Organizations#organizationcollection) Organizations { get; }

Gets the collection of organizations to which the member belongs.

### [MemberPreferences](/API-Members#memberpreferences) Preferences { get; }

Gets the set of preferences for the member.

### string UserName { get; set; }

Gets or sets the member&#39;s username.

# AvatarSource

Enumerates the avatar sources used by Trello.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- AvatarSource

## Fields

### Unknown

Indicates the avatar source is not recognized.

### None

Indicates there is no avatar.

### Upload

Indicates the avatar has been uploaded by the user.

### Gravatar

Indicates the avatar is supplied by Gravatar.

# MemberStatus

Enumerates known values for a member&#39;s activity status.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- MemberStatus

## Fields

### Unknown

Not recognized. May have been created since the current version of this API.

### Disconnected

Indicates the member is not connected to the website.

### Idle

Indicates the member is connected to the website but inactive.

### Active

Indicates the member is actively using the website.

# ReadOnlyMemberCollection

A read-only collection of members.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Member&gt;
- ReadOnlyMemberCollection

## Properties

### [Member](/API-Members#member) this[string key] { get; }

Retrieves a member which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching member, or null if none found.

#### Remarks

Matches on [Id](/API-Members#string-id--get-), [FullName](/API-Members#string-fullname--get-), and [UserName](/API-Members#string-username--get-). Comparison is case-sensitive.

# MemberCollection

A collection of members.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Member&gt;
- ReadOnlyMemberCollection
- MemberCollection

## Methods

### void Add(Member member)

Adds a member to the collection.

**Parameter:** member

The member to add.

### void Remove(Member member)

Removes a member from the collection.

**Parameter:** member

The member to remove.

# MemberPreferences

Represents preferences for a member.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- MemberPreferences

## Properties

### bool? EnableColorBlindMode { get; set; }

Gets or sets whether color-blind mode is enabled.

### int? MinutesBetweenSummaries { get; set; }

Gets or sets the time between email summaries.

