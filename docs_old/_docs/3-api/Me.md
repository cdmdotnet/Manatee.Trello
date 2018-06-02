---
title: Me
category: API
order: 192
---

Represents the current member.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Member
- Me

## Properties

### [AvatarSource](../AvatarSource#avatarsource)? AvatarSource { get; set; }

Gets or sets the source type for the member&#39;s avatar.

### string Bio { get; set; }

Gets or sets the member&#39;s bio.

### [IBoardCollection](../IBoardCollection#iboardcollection) Boards { get; }

Gets the collection of boards owned by the member.

### string Email { get; set; }

Gets or sets the member&#39;s email.

### string FullName { get; set; }

Gets or sets the member&#39;s full name.

### string Initials { get; set; }

Gets or sets the member&#39;s initials.

### [IReadOnlyNotificationCollection](../IReadOnlyNotificationCollection#ireadonlynotificationcollection) Notifications { get; }

Gets the collection of notificaitons for the member.

### [IOrganizationCollection](../IOrganizationCollection#iorganizationcollection) Organizations { get; }

Gets the collection of organizations to which the member belongs.

### [IMemberPreferences](../IMemberPreferences#imemberpreferences) Preferences { get; }

Gets the set of preferences for the member.

### [IStarredBoardCollection](../IStarredBoardCollection#istarredboardcollection) StarredBoards { get; }

Gets the collection of the member&#39;s board stars.

### string UserName { get; set; }

Gets or sets the member&#39;s username.

