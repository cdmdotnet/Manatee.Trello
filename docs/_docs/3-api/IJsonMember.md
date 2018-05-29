---
title: IJsonMember
category: API
order: 107
---

Defines the JSON structure for the Member object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonMember

## Properties

### List&lt;IJsonAction&gt; Actions { get; set; }

Gets or sets a collection of actions.

### string AvatarHash { get; set; }

Gets or sets the member&#39;s avatar hash.

### [AvatarSource](../AvatarSource#avatarsource)? AvatarSource { get; set; }

Gets or sets the source URL for the member&#39;s avatar.

### string Bio { get; set; }

Gets or sets the bio of the member.

### List&lt;IJsonBoard&gt; Boards { get; set; }

Gets or sets a collection of boards.

### List&lt;IJsonCard&gt; Cards { get; set; }

Gets or sets a collection of cards.

### bool? Confirmed { get; set; }

Gets or sets whether the member is confirmed.

### string Email { get; set; }

Gets or sets the member&#39;s registered email address.

### string FullName { get; set; }

Gets the member&#39;s full name.

### string GravatarHash { get; set; }

Gets or sets the member&#39;s Gravatar hash.

### string Initials { get; set; }

Gets or sets the member&#39;s initials.

### List&lt;string&gt; LoginTypes { get; set; }

Gets or sets the login types for the member.

### string MemberType { get; set; }

Gets or sets the type of member.

### List&lt;IJsonNotification&gt; Notifications { get; set; }

Gets or sets a collection of notifications.

### List&lt;string&gt; OneTimeMessagesDismissed { get; set; }

Gets or sets the types of message which are dismissed for the member.

### List&lt;IJsonOrganization&gt; Organizations { get; set; }

Gets or sets a collection of organizations.

### [IJsonMemberPreferences](../IJsonMemberPreferences#ijsonmemberpreferences) Prefs { get; set; }

Gets or sets a set of preferences for the member.

### int? Similarity { get; set; }

Gets or sets the similarity of the member to a search query.

### [MemberStatus](../MemberStatus#memberstatus)? Status { get; set; }

Gets or sets the member&#39;s activity status.

### List&lt;string&gt; Trophies { get; set; }

Gets or sets the trophies obtained by the member.

### string UploadedAvatarHash { get; set; }

Gets or sets the user&#39;s uploaded avatar hash.

### string Url { get; set; }

Gets or sets the URL to the member&#39;s profile.

### string Username { get; set; }

Gets or sets the member&#39;s username.

