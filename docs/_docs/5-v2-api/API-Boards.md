---
title: Boards
category: Version 2.x - API
order: 3
---

> **NOTICE** In migrating to this new documentation, many (if not all) of the links are broken.  Please use the sidebar on the left for navigation.

# Board

Represents a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Board

## Constructors

### Board(string id, TrelloAuthorization auth)

Creates a new instance of the [Board](/API-Boards#board) object.

**Parameter:** id

The board&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](/API-Configuration#static-trelloauthorization-default--get-) will be used.

## Properties

### static Manatee.Trello.Board+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [ReadOnlyActionCollection](/API-Actions#readonlyactioncollection) Actions { get; }

Gets the collection of actions performed on and within the board.

### [ReadOnlyCardCollection](/API-Cards#readonlycardcollection) Cards { get; }

Gets the collection of cards contained within the board.

#### Remarks

This property only exposes unarchived cards.

### DateTime CreationDate { get; }

Gets the creation date of the board.

### string Description { get; set; }

Gets or sets the board&#39;s description.

### string Id { get; }

Gets the board&#39;s ID.

### bool? IsClosed { get; set; }

Gets or sets whether the board is closed.

### bool? IsPinned { get; set; }

Gets or sets wheterh this board is pinned.

### bool? IsStarred { get; set; }

Gets or sets wheterh this board is pinned.

### bool? IsSubscribed { get; set; }

Gets or sets whether the current member is subscribed to this board.

### [List](/API-Lists#list) this[string key] { get; }

Retrieves a list which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching list, or null if none found.

#### Remarks

Matches on List.Id and List.Name. Comparison is case-sensitive.

### [List](/API-Lists#list) this[int index] { get; }

Retrieves the list at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The list.

### [BoardLabelCollection](/API-Labels#boardlabelcollection) Labels { get; }

Gets the collection of labels for the board.

### DateTime? LastActivity { get; }

Gets the date of the board&#39;s most recent activity.

### DateTime? LastViewed { get; }

Gets the date when the board was most recently viewed.

### [ListCollection](/API-Lists#listcollection) Lists { get; }

Gets the collection of lists on this board.

#### Remarks

This property only exposes unarchived lists.

### [ReadOnlyMemberCollection](/API-Members#readonlymembercollection) Members { get; }

Gets the collection of members on the board.

### [BoardMembershipCollection](/API-Boards#boardmembershipcollection) Memberships { get; }

Gets the collection of members and their privileges on the board.

### string Name { get; set; }

Gets or sets the board&#39;s name.

### [Organization](/API-Organizations#organization) Organization { get; set; }

Gets or sets the organization to which the board belongs.

#### Remarks

Setting null makes the board&#39;s first admin the owner.

### [BoardPersonalPreferences](/API-Boards#boardpersonalpreferences) PersonalPreferences { get; }

Gets the set of preferences for the board.

### [ReadOnlyPowerUpDataCollection](/API-Power-Ups#readonlypowerupdatacollection) PowerUpData { get; }

Gets specific data regarding power-ups.

### [ReadOnlyPowerUpCollection](/API-Power-Ups#readonlypowerupcollection) PowerUps { get; }

Gets metadata about any active power-ups.

### [BoardPreferences](/API-Boards#boardpreferences) Preferences { get; }

Gets the set of preferences for the board.

### string ShortLink { get; }

Gets the board&#39;s short URI.

### string ShortUrl { get; }

Gets the board&#39;s short link (ID).

### string Url { get; }

Gets the board&#39;s URI.

## Events

### Action&lt;Board, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the board is updated.

## Methods

### void ApplyAction(Action action)

Applies the changes an action represents.

**Parameter:** action

The action.

### void Delete()

Permanently deletes the board from Trello.

#### Remarks

This instance will remain in memory and all properties will remain accessible.

### void Refresh()

Marks the board to be refreshed the next time data is accessed.

### string ToString()

Returns the [Name](/API-Boards#string-name--get-set-).

**Returns:** A string that represents the attachment.

# ReadOnlyBoardCollection

A read-only collectin of boards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Board&gt;
- ReadOnlyBoardCollection

## Properties

### [Board](/API-Boards#board) this[string key] { get; }

Retrieves a board which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching board, or null if none found.

#### Remarks

Matches on [Id](/API-Boards#string-id--get-) and [Name](/API-Boards#string-name--get-set-). Comparison is case-sensitive.

# BoardCollection

A collection of boards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Board&gt;
- ReadOnlyBoardCollection
- BoardCollection

## Methods

### [Board](/API-Boards#board) Add(string name, Board source)

Creates a new board.

**Parameter:** name

The name of the board to create.

**Parameter:** source

(Optional) A board to use as a template.

**Returns:** The [Board](/API-Boards#board) generated by Trello.

# BoardMembership

Associates a [Member](/API-Boards#member-member--get-) to a [Board](/API-Boards#board) and indicates any permissions the member has on the board.

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

### [Member](/API-Members#member) Member { get; }

Gets the member.

### [BoardMembershipType](/API-Boards#boardmembershiptype)? MemberType { get; set; }

Gets the membership&#39;s permission level.

## Events

### Action&lt;BoardMembership, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the membership is updated.

## Methods

### void Refresh()

Marks the board membership to be refreshed the next time data is accessed.

# BoardMembershipType

Enumerates known board membership types.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- BoardMembershipType

## Fields

### Unknown

Not recognized. May have been created since the current version of this API.

### Admin

Indicates the member is an admin of the board.

### Normal

Indicates the member is a normal member of the board.

### Observer

Indicates the member is may only view the board.

### Ghost

Indicates the member has been invited, but has not yet joined Trello.

# BoardMembershipCollection

A collection of board memberships.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;BoardMembership&gt;
- ReadOnlyBoardMembershipCollection
- BoardMembershipCollection

## Methods

### [BoardMembership](/API-Boards#boardmembership) Add(Member member, BoardMembershipType membership)

Adds a member to a board with specified privileges.

**Parameter:** member

The member to add.

**Parameter:** membership

The membership type.

### void Remove(Member member)

Removes a member from a board.

**Parameter:** member

The member to remove.

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

### [BoardBackground](/API-Boards#boardbackground) Background { get; set; }

Gets or sets the background of the board.

### [CardAgingStyle](/API-Boards#cardagingstyle)? CardAgingStyle { get; set; }

Gets or sets the card aging style for the Card Aging power up.

### [BoardCommentPermission](/API-Boards#boardcommentpermission)? Commenting { get; set; }

Gets or sets whether commenting is enabled and which members are allowed to add comments.

### [BoardInvitationPermission](/API-Boards#boardinvitationpermission)? Invitations { get; set; }

Gets or sets which members may invite others to the board.

### bool? IsCalendarFeedEnabled { get; set; }

Gets or sets whether the calendar feed is enabled.

### [BoardPermissionLevel](/API-Boards#boardpermissionlevel)? PermissionLevel { get; set; }

Gets or sets the general visibility of the board.

### bool? ShowCardCovers { get; set; }

Gets or sets whether card covers are shown.

### [BoardVotingPermission](/API-Boards#boardvotingpermission)? Voting { get; set; }

Gets or sets whether voting is enabled and which members are allowed to vote.

# CardAgingStyle

Enumerates the various styles of aging for the Card Aging power up.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- CardAgingStyle

## Fields

### Unknown

Not recognized. May have been created since the current version of this API.

### Regular

Indicates that cards will age by fading.

### Pirate

Indicates that cards will age using a treasure map effect.

# BoardCommentPermission

Enumerates known board commenting permission levels.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- BoardCommentPermission

## Fields

### Unknown

Not recognized. May have been created since the current version of this API.

### Members

Indicates that only members of the board may comment on cards.

### Observers

Indicates that observers may make comments on cards.

### Org

Indicates that only members of the organization to which the board belongs may comment on cards.

### Public

Indicates that any Trello member may comment on cards.

### Disabled

Indicates that no members may comment on cards.

# BoardInvitationPermission

Enumerates known board invitation permission levels.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- BoardInvitationPermission

## Fields

### Unknown

Not recognized. May have been created since the current version of this API.

### Members

Indicates that any member of the board may extend an invitation to join the board.

### Admins

Indicates that only admins of the board may extend an invitation to joni the board.

# BoardPermissionLevel

Enumerates known values for board permission levels

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- BoardPermissionLevel

## Fields

### Unknown

Not recognized. May have been created since the current version of this API.

### Private

Indicates that the board can only be viewed by its members.

### Org

Indicates that the board may be viewed by any member of the organization to which the board belongs.

### Public

Indicates that anyone (even non-Trello users) may view the board.

# BoardVotingPermission

Enumerates known voting permission levels for a board

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- BoardVotingPermission

## Fields

### Unknown

Not recognized. May have been created since the current version of this API.

### Members

Indicates that only members of the board may vote on cards.

### Org

Indicates that only members of the organization to which the board belongs may vote on cards.

### Public

Indicates that any Trello member may vote on cards.

### Disabled

Indicates that no members may vote on cards.

# BoardPersonalPreferences

Represents the user-specific preferences for a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- BoardPersonalPreferences

## Properties

### [List](/API-Lists#list) EmailList { get; set; }

Gets or sets the [List](/API-Lists#list) which will be used to post new cards submitted by email.

### [Position](/API-Common-Types#position) EmailPosition { get; set; }

Gets or sets the [Position](/API-Common-Types#position) within a [List](/API-Lists#list) which will be used to post new cards submitted by email.

### bool? ShowListGuide { get; set; }

Gets or sets whether to show the list guide.

#### Remarks

It appears that this may be deprecated by Trello.

### bool? ShowSidebar { get; set; }

Gets or sets whether to show the side bar.

### bool? ShowSidebarActivity { get; set; }

Gets or sets whether to show the activity list in the side bar.

#### Remarks

It appears that this may be deprecated by Trello.

### bool? ShowSidebarBoardActions { get; set; }

Gets or sets whether to show the board action list in the side bar.

#### Remarks

It appears that this may be deprecated by Trello.

### bool? ShowSidebarMembers { get; set; }

Gets or sets whether to show the board members in the side bar.

#### Remarks

It appears that this may be deprecated by Trello.

# BoardBackground

Represents a background image for a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- BoardBackground

## Properties

### static [BoardBackground](/API-Boards#boardbackground) Blue { get; }

The standard blue board background.

### static [BoardBackground](/API-Boards#boardbackground) Green { get; }

The standard green board background.

### static [BoardBackground](/API-Boards#boardbackground) Grey { get; }

The standard grey board background.

### static [BoardBackground](/API-Boards#boardbackground) Lime { get; }

The standard bright green board background.

### static [BoardBackground](/API-Boards#boardbackground) Orange { get; }

The standard orange board background.

### static [BoardBackground](/API-Boards#boardbackground) Pink { get; }

The standard pink board background.

### static [BoardBackground](/API-Boards#boardbackground) Purple { get; }

The standard purple board background.

### static [BoardBackground](/API-Boards#boardbackground) Red { get; }

The standard red board background.

### static [BoardBackground](/API-Boards#boardbackground) Sky { get; }

The standard light blue board background.

### [WebColor](/API-Boards#webcolor) BottomColor { get; }

Gets the bottom color of a gradient background.

### BoardBackgroundBrightness? Brightness { get; }

Gets the brightness of the background.

### [WebColor](/API-Boards#webcolor) Color { get; }

Gets the color of a stock solid-color background.

### string Id { get; }

Gets the background&#39;s ID.

### string Image { get; }

Gets the image of a background.

### bool? IsTiled { get; }

Gets whether the image is tiled when displayed.

### [ReadOnlyBoardBackgroundScalesCollection](/API-Boards#readonlyboardbackgroundscalescollection) ScaledImages { get; }

Gets a collections of scaled background images.

### [WebColor](/API-Boards#webcolor) TopColor { get; }

Gets the top color of a gradient background.

# ReadOnlyBoardBackgroundScalesCollection

A read-only collection of scaled versions of board backgrounds.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;ImagePreview&gt;
- ReadOnlyBoardBackgroundScalesCollection

# WebColor

Defines a color in the RGB space.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- WebColor

## Constructors

### WebColor(ushort red, ushort green, ushort blue)

Creates a new instance of the [WebColor](/API-Boards#webcolor) class.

**Parameter:** red

The red component.

**Parameter:** green

The green component.

**Parameter:** blue

The blue component.

### WebColor(string serialized)

Creates a new isntance of the [WebColor](/API-Boards#webcolor) class.

**Parameter:** serialized

A string representation of RGB values in the format &quot;#RRGGBB&quot;.

## Properties

### ushort Blue { get; }

Gets the blue component.

### ushort Green { get; }

Gets the green component.

### ushort Red { get; }

Gets the red component.

## Methods

### string ToString()

Returns an HTML-compatible color code.

**Returns:** A string that represents the current object.

