---
title: IJsonBoardPreferences
category: API
order: 94
---

Defines the JSON structure for the BoardPreferences object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardPreferences

## Properties

### [IJsonBoardBackground](../IJsonBoardBackground#ijsonboardbackground) Background { get; set; }

Gets or sets the background image of the board.

### bool? CalendarFeed { get; set; }

Gets or sets whether the calendar feed is enabled.

### [CardAgingStyle](../CardAgingStyle#cardagingstyle)? CardAging { get; set; }

Gets or sets the style of card aging is used, if the power up is enabled.

### bool? CardCovers { get; set; }

Gets or sets whether card covers are shown on the board.

### [BoardCommentPermission](../BoardCommentPermission#boardcommentpermission)? Comments { get; set; }

Gets or sets who may comment on cards.

### [BoardInvitationPermission](../BoardInvitationPermission#boardinvitationpermission)? Invitations { get; set; }

Gets or sets who may extend invitations to join the board.

### [BoardPermissionLevel](../BoardPermissionLevel#boardpermissionlevel)? PermissionLevel { get; set; }

Gets or sets who may view the board.

### bool? SelfJoin { get; set; }

Gets or sets whether a Trello member may join the board without an invitation.

### [BoardVotingPermission](../BoardVotingPermission#boardvotingpermission)? Voting { get; set; }

Gets or sets who may vote on cards.

