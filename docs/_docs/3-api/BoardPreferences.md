---
title: BoardPreferences
category: API
order: 23
---

# BoardPreferences

Represents the preferences for a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- BoardPreferences

## Properties

### bool? AllowSelfJoin { get; set; }

Gets or sets whether any Trello member may join the board themselves or if an invitation is required.

### [IBoardBackground](IBoardBackground#iboardbackground) Background { get; set; }

Gets or sets the background of the board.

### [CardAgingStyle](CardAgingStyle#cardagingstyle)? CardAgingStyle { get; set; }

Gets or sets the card aging style for the Card Aging power up.

### [BoardCommentPermission](BoardCommentPermission#boardcommentpermission)? Commenting { get; set; }

Gets or sets whether commenting is enabled and which members are allowed to add comments.

### [BoardInvitationPermission](BoardInvitationPermission#boardinvitationpermission)? Invitations { get; set; }

Gets or sets which members may invite others to the board.

### bool? IsCalendarFeedEnabled { get; set; }

Gets or sets whether the calendar feed is enabled.

### [BoardPermissionLevel](BoardPermissionLevel#boardpermissionlevel)? PermissionLevel { get; set; }

Gets or sets the general visibility of the board.

### bool? ShowCardCovers { get; set; }

Gets or sets whether card covers are shown.

### [BoardVotingPermission](BoardVotingPermission#boardvotingpermission)? Voting { get; set; }

Gets or sets whether voting is enabled and which members are allowed to vote.

