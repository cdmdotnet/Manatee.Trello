---
title: JSON Serialization
category: Version 2.x - API
order: 18
---

# ISerializer

Defines methods required by the IRestClient to serialize an object to JSON.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- ISerializer

## Methods

### string Serialize(Object obj)

Serializes an object to JSON.

**Parameter:** obj

The object to serialize.

**Returns:** An equivalent JSON string.

# IDeserializer

Defines methods required by the IRestClient to deserialize a response from JSON to an object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IDeserializer

## Methods

### T Deserialize&lt;T&gt;(string content)

Attempts to deserialize a RESTful response to the indicated type.

**Type Parameter:** T (no constraints)

The type of object expected.

**Parameter:** content

A string which contains the JSON to deserialize.

**Returns:** The requested object, if JSON is valid; null otherwise.

# ManateeSerializer

Wrapper class for the Manatee.Json.Serializer for use with RestSharp.

**Assembly:** Manatee.Trello.ManateeJson.dll

**Namespace:** Manatee.Trello.ManateeJson

**Inheritance hierarchy:**

- Object
- ManateeSerializer

## Constructors

### ManateeSerializer()

Creates and initializes a new instance of the ManateeJsonSerializer class.

## Methods

### T Deserialize&lt;T&gt;(string content)

Attempts to deserialize a RESTful response to the indicated type.

**Type Parameter:** T (no constraints)

The type of object expected.

**Parameter:** content

A string which contains the JSON to deserialize.

**Returns:** The requested object, if JSON is valid; null otherwise.

### string Serialize(Object obj)

Serializes an object to JSON.

**Parameter:** obj

The object to serialize.

**Returns:** An equivalent JSON string.

# JsonDeserializeAttribute

Declares that the JSON property should be deserialized.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- Object
- Attribute
- JsonDeserializeAttribute

# JsonSerializeAttribute

Declares that the JSON property should be serialized and whether it is optional or required.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- Object
- Attribute
- JsonSerializeAttribute

## Properties

### bool IsRequired { get; set; }

Gets or sets whether this property is required by the Trello API.

# JsonSpecialSerializationAttribute

Declares that the JSON property has a special serialization.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- Object
- Attribute
- JsonSpecialSerializationAttribute

# IJsonFactory

Creates instances of JSON interfaces.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonFactory

## Methods

### T Create&lt;T&gt;()

Creates an instance of the requested JSON interface.

**Type Parameter:** T (no constraints)

The type to create.

**Returns:** An instance of the requested type.

# ManateeFactory

Creates instances of JSON interfaces.

**Assembly:** Manatee.Trello.ManateeJson.dll

**Namespace:** Manatee.Trello.ManateeJson

**Inheritance hierarchy:**

- Object
- ManateeFactory

## Methods

### T Create&lt;T&gt;()

Creates an instance of the requested JSON interface.

**Type Parameter:** T (no constraints)

The type to create.

**Returns:** An instance of the requested type.

# IJsonCacheable

Defines properties required for TrelloService to cache an item.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCacheable

## Properties

### string Id { get; set; }

Gets or sets a unique identifier (not necessarily a GUID).

# IJsonAction

Defines the JSON structure for the Action object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonAction

## Properties

### [IJsonActionData](/API-Json#ijsonactiondata) Data { get; set; }

Gets or sets the data associated with the action. Contents depend upon the action&#39;s type.

### DateTime? Date { get; set; }

Gets or sets the date on which the action was performed.

### [IJsonMember](/API-Json#ijsonmember) MemberCreator { get; set; }

Gets or sets the ID of the member who performed the action.

### string Text { get; set; }

Gets or sets the text for a comment while updating it.

### [ActionType](/API-Actions#actiontype)? Type { get; set; }

Gets or sets the action&#39;s type.

# IJsonActionData

Defines the JSON structure for the ActionData object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonActionData

## Properties

### [IJsonAttachment](/API-Json#ijsonattachment) Attachment { get; set; }

Gets or sets an attachment associated with the action if any.

### [IJsonBoard](/API-Json#ijsonboard) Board { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonBoard](/API-Json#ijsonboard) BoardSource { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonBoard](/API-Json#ijsonboard) BoardTarget { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonCard](/API-Json#ijsoncard) Card { get; set; }

Gets or sets a card associated with the action if any.

### [IJsonCard](/API-Json#ijsoncard) CardSource { get; set; }

Gets or sets a card associated with the action if any.

### [IJsonCheckItem](/API-Json#ijsoncheckitem) CheckItem { get; set; }

Gets or sets a check item associated with the action if any.

### [IJsonCheckList](/API-Json#ijsonchecklist) CheckList { get; set; }

Gets or sets a check list associated with the action if any.

### DateTime? DateLastEdited { get; set; }

Gets or sets the last date/time that a comment was edited.

### [IJsonLabel](/API-Json#ijsonlabel) Label { get; set; }

Gets or sets a label associated with the action if any.

### [IJsonList](/API-Json#ijsonlist) List { get; set; }

Gets or sets a list associated with the action if any.

### [IJsonList](/API-Json#ijsonlist) ListAfter { get; set; }

Gets or sets a destination list associated with the action if any.

### [IJsonList](/API-Json#ijsonlist) ListBefore { get; set; }

Gets or sets a source list associated with the action if any.

### [IJsonMember](/API-Json#ijsonmember) Member { get; set; }

Gets or sets a member associated with the action if any.

### [IJsonActionOldData](/API-Json#ijsonactionolddata) Old { get; set; }

Gets or sets any previous data associated with the action.

### [IJsonOrganization](/API-Json#ijsonorganization) Org { get; set; }

Gets or sets an organization associated with the action if any.

### [IJsonPowerUp](/API-Json#ijsonpowerup) Plugin { get; set; }

Gets or sets plugin data associated with the action if any.

### string Text { get; set; }

Gets or sets text associated with the action if any.

### string Value { get; set; }

Gets or sets a custom value associate with the action if any.

# IJsonActionOldData

Defines the JSON structure for the ActionOldData object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonActionOldData

## Properties

### bool? Closed { get; set; }

Gets or sets whether an item was closed.

### string Desc { get; set; }

Gets or sets an old description.

### [IJsonList](/API-Json#ijsonlist) List { get; set; }

Gets or sets an old list.

### double? Pos { get; set; }

Gets or sets an old position.

### string Text { get; set; }

Gets or sets old text.

# IJsonAttachment

Defines the JSON structure for the Attachment object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonAttachment

## Properties

### int? Bytes { get; set; }

Gets or sets the size of the attachment.

### DateTime? Date { get; set; }

Gets or sets the date on which the attachment was created.

### string EdgeColor { get; set; }

Gets or sets the border color of the attachment preview on the card.

### bool? IsUpload { get; set; }

?

### [IJsonMember](/API-Json#ijsonmember) Member { get; set; }

Gets or sets the ID of the member who created the attachment.

### string MimeType { get; set; }

Gets or sets the type of attachment.

### string Name { get; set; }

Gets or sets the name of the attachment.

### [IJsonPosition](/API-Json#ijsonposition) Pos { get; set; }

Gets or sets the attachment&#39;s position.

### List&lt;IJsonImagePreview&gt; Previews { get; set; }

Gets or sets a collection of previews for the attachment.

### string Url { get; set; }

Gets or sets the attachment storage location.

# IJsonBadges

Defines the JSON structure for the Badges object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBadges

## Properties

### int? Attachments { get; set; }

Gets or sets the number of attachments.

### int? CheckItems { get; set; }

Gets or sets the number of check items.

### int? CheckItemsChecked { get; set; }

Gets or sets the number of check items which have been checked.

### int? Comments { get; set; }

Gets or sets the number of comments.

### bool? Description { get; set; }

Gets or sets whether the card has a description.

### DateTime? Due { get; set; }

Gets or sets the due date, if one exists.

### bool? DueComplete { get; set; }

Gets or sets whether the card is complete.

### string Fogbugz { get; set; }

Gets or sets the FogBugz ID.

### bool? Subscribed { get; set; }

Gets or sets whether the member is subscribed to the card.

### bool? ViewingMemberVoted { get; set; }

Gets or sets whether the member has voted for this card.

### int? Votes { get; set; }

Gets or sets the number of votes.

# IJsonBoard

Defines the JSON structure for the Board object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoard

## Properties

### [IJsonBoard](/API-Json#ijsonboard) BoardSource { get; set; }

Gets or sets a board to be used as a template.

### bool? Closed { get; set; }

Gets or sets whether this board is closed.

### DateTime? DateLastActivity { get; set; }

Gets or sets the date the board last had activity.

### DateTime? DateLastView { get; set; }

Gets or sets the date the board was last viewed.

### string Desc { get; set; }

Gets or sets the board&#39;s description.

### string Name { get; set; }

Gets or sets the board&#39;s name.

### [IJsonOrganization](/API-Json#ijsonorganization) Organization { get; set; }

Gets or sets the ID of the organization, if any, to which this board belongs.

### bool? Pinned { get; set; }

Gets or sets whether the board is pinned.

### [IJsonBoardPreferences](/API-Json#ijsonboardpreferences) Prefs { get; set; }

Gets or sets a set of preferences for the board.

### string ShortLink { get; set; }

Gets or sets the short link (ID).

### string ShortUrl { get; set; }

Gets or sets the short URL.

### bool? Starred { get; set; }

Gets or sets whether the board is starred on the member&#39;s dashboard.

### bool? Subscribed { get; set; }

Gets or sets whether the user is subscribed to this board.

### string Url { get; set; }

Gets or sets the URL for this board.

# IJsonBoardBackground

Defines the JSON structure for the BoardBackground object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardBackground

## Properties

### string BottomColor { get; set; }

The bottom color of a gradient background.

### BoardBackgroundBrightness? Brightness { get; set; }

Gets the overall brightness of the background.

### string Color { get; set; }

Gets or sets the color.

### string Image { get; set; }

Gets or sets the url for the image.

### List&lt;IJsonImagePreview&gt; ImageScaled { get; set; }

Gets or sets a collection of scaled images.

### bool? Tile { get; set; }

Gets or sets whether the image should be tiled when displayed.

### string TopColor { get; set; }

The top color of a gradient background.

# IJsonBoardMembership

Defines the JSON structure for the BoardMembership object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardMembership

## Properties

### bool? Deactivated { get; set; }

Gets or sets whether the membership is deactivated.

### [IJsonMember](/API-Json#ijsonmember) Member { get; set; }

Gets or sets the ID of the member.

### [BoardMembershipType](/API-Boards#boardmembershiptype)? MemberType { get; set; }

Gets or sets the membership type.

# IJsonBoardPersonalPreferences

Defines the JSON structure for the BoardPersonalPreferences object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardPersonalPreferences

## Properties

### [IJsonList](/API-Json#ijsonlist) EmailList { get; set; }

Gets or sets the list for new cards when they are added via email.

### [IJsonPosition](/API-Json#ijsonposition) EmailPosition { get; set; }

Gets or sets the position of new cards when they are added via email.

### bool? ShowListGuide { get; set; }

Gets or sets whether the list guide (left side of the screen) is expanded.

### bool? ShowSidebar { get; set; }

Gets or sets whether the side bar (right side of the screen) is shown

### bool? ShowSidebarActivity { get; set; }

Gets or sets whether the activity section of the side bar is shown.

### bool? ShowSidebarBoardActions { get; set; }

Gets or sets whether the board actions (Add List/Add Member/Filter Cards) section of the side bar is shown.

### bool? ShowSidebarMembers { get; set; }

Gets or sets whether the members section of the list of the side bar is shown.

# IJsonBoardPreferences

Defines the JSON structure for the BoardPreferences object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardPreferences

## Properties

### [IJsonBoardBackground](/API-Json#ijsonboardbackground) Background { get; set; }

Gets or sets the background image of the board.

### bool? CalendarFeed { get; set; }

Gets or sets whether the calendar feed is enabled.

### [CardAgingStyle](/API-Boards#cardagingstyle)? CardAging { get; set; }

Gets or sets the style of card aging is used, if the power up is enabled.

### bool? CardCovers { get; set; }

Gets or sets whether card covers are shown on the board.

### [BoardCommentPermission](/API-Boards#boardcommentpermission)? Comments { get; set; }

Gets or sets who may comment on cards.

### [BoardInvitationPermission](/API-Boards#boardinvitationpermission)? Invitations { get; set; }

Gets or sets who may extend invitations to join the board.

### [BoardPermissionLevel](/API-Boards#boardpermissionlevel)? PermissionLevel { get; set; }

Gets or sets who may view the board.

### bool? SelfJoin { get; set; }

Gets or sets whether a Trello member may join the board without an invitation.

### [BoardVotingPermission](/API-Boards#boardvotingpermission)? Voting { get; set; }

Gets or sets who may vote on cards.

# IJsonBoardVisibilityRestrict

Defines the JSON structure for the BoardVisibilityRestrict object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardVisibilityRestrict

## Properties

### [OrganizationBoardVisibility](/API-Organizations#organizationboardvisibility)? Org { get; set; }

Gets or sets the visibility of Org-visible boards owned by the organization.

### [OrganizationBoardVisibility](/API-Organizations#organizationboardvisibility)? Private { get; set; }

Gets or sets the visibility of private boards owned by the organization.

### [OrganizationBoardVisibility](/API-Organizations#organizationboardvisibility)? Public { get; set; }

Gets or sets the visibility of publicly-visible boards owned by the organization.

# IJsonCard

Defines the JSON structure for the Card object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCard

## Properties

### [IJsonBadges](/API-Json#ijsonbadges) Badges { get; set; }

Gets or set the badges displayed on the card cover.

### [IJsonBoard](/API-Json#ijsonboard) Board { get; set; }

Gets or sets the ID of the board which contains the card.

### [IJsonCard](/API-Json#ijsoncard) CardSource { get; set; }

Gets or sets a card to be used as a template during creation.

### bool? Closed { get; set; }

Gets or sets whether a card has been archived.

### DateTime? DateLastActivity { get; set; }

Gets or sets the date of last activity for a card.

### string Desc { get; set; }

Gets or sets the card&#39;s description.

### DateTime? Due { get; set; }

Gets or sets the card&#39;s due date.

### bool? DueComplete { get; set; }

Gets or sets whether the card is complete.

### bool ForceDueDate { get; set; }

Gets or sets whether the due date should be serialized, even if it is null.

### string IdAttachmentCover { get; set; }

Gets or sets the ID of the attachment cover image.

### string IdLabels { get; set; }

Gets or sets a comma-delimited list of label IDs. Used during card creation.

### string IdMembers { get; set; }

Gets or sets a comma-delimited list of member IDs. Used during card creation.

### int? IdShort { get; set; }

Gets or sets the card&#39;s short ID.

### List&lt;IJsonLabel&gt; Labels { get; set; }

Gets or sets the labels assigned to this card.

### [IJsonList](/API-Json#ijsonlist) List { get; set; }

Gets or sets the ID of the list which contains the card.

### bool? ManualCoverAttachment { get; set; }

Gets or sets whether the cover attachment was manually selected

### string Name { get; set; }

Gets or sets the card&#39;s name

### [IJsonPosition](/API-Json#ijsonposition) Pos { get; set; }

Gets or sets the card&#39;s position.

### string ShortUrl { get; set; }

Gets or sets the short URL for this card.

### bool? Subscribed { get; set; }

Gets or sets whether the current member is subscribed to this card.

### string Url { get; set; }

Gets or sets the URL for this card.

### Object UrlSource { get; set; }

Gets or sets a URL to be imported during creation.

# IJsonCheckItem

Defines the JSON structure for the CheckItem object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCheckItem

## Properties

### string Name { get; set; }

Gets or sets the name of the checklist item.

### [IJsonPosition](/API-Json#ijsonposition) Pos { get; set; }

Gets or sets the position of the checklist item.

### [CheckItemState](/API-CheckLists#checkitemstate)? State { get; set; }

Gets or sets the check state of the checklist item.

# IJsonCheckList

Defines the JSON structure for the CheckList object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCheckList

## Properties

### [IJsonBoard](/API-Json#ijsonboard) Board { get; set; }

Gets or sets the ID of the board which contains this checklist.

### [IJsonCard](/API-Json#ijsoncard) Card { get; set; }

Gets or sets the ID of the card which contains this checklist.

### List&lt;IJsonCheckItem&gt; CheckItems { get; set; }

Gets or sets the collection of items in this checklist.

### [IJsonCheckList](/API-Json#ijsonchecklist) CheckListSource { get; set; }

Gets or sets a checklist to copy during creation.

### string Name { get; set; }

Gets or sets the name of this checklist.

### [IJsonPosition](/API-Json#ijsonposition) Pos { get; set; }

Gets or sets the position of this checklist.

# IJsonImagePreview

Defines the JSON structure for the AttachmentPreview object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonImagePreview

## Properties

### int? Height { get; set; }

Gets or sets the height in pixels of the attachment preview.

### bool? Scaled { get; set; }

Gets or sets whether the attachment was scaled to produce the preview.

### string Url { get; set; }

Gets or sets the attachment storage location.

### int? Width { get; set; }

Gets or sets the width in pixels of the attachment preview.

# IJsonLabel

Defines the JSON structure for the Label object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonLabel

## Properties

### [IJsonBoard](/API-Json#ijsonboard) Board { get; set; }

Gets and sets the board on which the label is defined.

### LabelColor? Color { get; set; }

Gets and sets the color of the label.

### bool ForceNullColor { set; }

Determines if the color property should be submitted even if it is null.

#### Remarks

This property is not part of the JSON structure.

### string Name { get; set; }

Gets and sets the name of the label.

### int? Uses { get; set; }

Gets and sets how many cards use this label.

# IJsonList

Defines the JSON structure for the List object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonList

## Properties

### [IJsonBoard](/API-Json#ijsonboard) Board { get; set; }

Gets or sets the ID of the board which contains the list.

### bool? Closed { get; set; }

Gets or sets whether the list is archived.

### string Name { get; set; }

Gets or sets the name of the list.

### [IJsonPosition](/API-Json#ijsonposition) Pos { get; set; }

Gets or sets the position of the list.

### bool? Subscribed { get; set; }

Gets or sets whether the current member is subscribed to the list.

# IJsonMember

Defines the JSON structure for the Member object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonMember

## Properties

### string AvatarHash { get; set; }

Gets or sets the member&#39;s avatar hash.

### [AvatarSource](/API-Members#avatarsource)? AvatarSource { get; set; }

Gets or sets the source URL for the member&#39;s avatar.

### string Bio { get; set; }

Gets or sets the bio of the member.

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

### List&lt;string&gt; OneTimeMessagesDismissed { get; set; }

Gets or sets the types of message which are dismissed for the member.

### [IJsonMemberPreferences](/API-Json#ijsonmemberpreferences) Prefs { get; set; }

Gets or sets a set of preferences for the member.

### int? Similarity { get; set; }

Gets or sets the similarity of the member to a search query.

### [MemberStatus](/API-Members#memberstatus)? Status { get; set; }

Gets or sets the member&#39;s activity status.

### List&lt;string&gt; Trophies { get; set; }

Gets or sets the trophies obtained by the member.

### string UploadedAvatarHash { get; set; }

Gets or sets the user&#39;s uploaded avatar hash.

### string Url { get; set; }

Gets or sets the URL to the member&#39;s profile.

### string Username { get; set; }

Gets or sets the member&#39;s username.

# IJsonMemberPreferences

Defines the JSON structure for the MemberPreferences object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonMemberPreferences

## Properties

### bool? ColorBlind { get; set; }

Enables/disables color-blind mode.

### int? MinutesBetweenSummaries { get; set; }

Gets or sets the number of minutes between summary emails.

# IJsonMemberSearch

Defines the JSON structure for a member search.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonMemberSearch

## Properties

### [IJsonBoard](/API-Json#ijsonboard) Board { get; set; }

Gets or sets a board within which the search should run.

### int? Limit { get; set; }

Gets or sets the number of results to return.

### List&lt;IJsonMember&gt; Members { get; set; }

Gets or sets a list of members.

### bool? OnlyOrgMembers { get; set; }

Gets or sets whether only organization members should be returned.

### [IJsonOrganization](/API-Json#ijsonorganization) Organization { get; set; }

Gets or sets an organization within which the search should run.

### string Query { get; set; }

Gets or sets the search query.

# IJsonNotification

Defines the JSON structure for the Notification object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonNotification

## Properties

### [IJsonNotificationData](/API-Json#ijsonnotificationdata) Data { get; set; }

Gets or sets the data associated with the notification. Contents depend upon the notification&#39;s type.

### DateTime? Date { get; set; }

Gets or sets the date on which the notification was created.

### [IJsonMember](/API-Json#ijsonmember) MemberCreator { get; set; }

Gets or sets the ID of the member whose action spawned the notification.

### [NotificationType](/API-Notifications#notificationtype)? Type { get; set; }

Gets or sets the notification&#39;s type.

### bool? Unread { get; set; }

Gets or sets whether the notification has been read.

# IJsonNotificationData

Defines the JSON structure for the NotificationData object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonNotificationData

## Properties

### [IJsonAttachment](/API-Json#ijsonattachment) Attachment { get; set; }

Gets or sets an attachment associated with the action if any.

### [IJsonBoard](/API-Json#ijsonboard) Board { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonBoard](/API-Json#ijsonboard) BoardSource { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonBoard](/API-Json#ijsonboard) BoardTarget { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonCard](/API-Json#ijsoncard) Card { get; set; }

Gets or sets a card associated with the action if any.

### [IJsonCard](/API-Json#ijsoncard) CardSource { get; set; }

Gets or sets a card associated with the action if any.

### [IJsonCheckItem](/API-Json#ijsoncheckitem) CheckItem { get; set; }

Gets or sets a check item associated with the action if any.

### [IJsonCheckList](/API-Json#ijsonchecklist) CheckList { get; set; }

Gets or sets a check list associated with the action if any.

### [IJsonList](/API-Json#ijsonlist) List { get; set; }

Gets or sets a list associated with the action if any.

### [IJsonList](/API-Json#ijsonlist) ListAfter { get; set; }

Gets or sets a destination list associated with the action if any.

### [IJsonList](/API-Json#ijsonlist) ListBefore { get; set; }

Gets or sets a source list associated with the action if any.

### [IJsonMember](/API-Json#ijsonmember) Member { get; set; }

Gets or sets a member associated with the action if any.

### [IJsonNotificationOldData](/API-Json#ijsonnotificationolddata) Old { get; set; }

Gets or sets any previous data associated with the action.

### [IJsonOrganization](/API-Json#ijsonorganization) Org { get; set; }

Gets or sets an organization associated with the action if any.

### string Text { get; set; }

Gets or sets text associated with the action if any.

# IJsonNotificationOldData

Defines the JSON structure for the ActionOldData object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonNotificationOldData

## Properties

### bool? Closed { get; set; }

Gets or sets whether an item was closed.

### string Desc { get; set; }

Gets or sets an old description.

### [IJsonList](/API-Json#ijsonlist) List { get; set; }

Gets or sets an old list.

### double? Pos { get; set; }

Gets or sets an old position.

### string Text { get; set; }

Gets or sets old text.

# IJsonOrganization

Defines the JSON structure for the Organization object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonOrganization

## Properties

### string Desc { get; set; }

Gets or sets the description for the organization.

### string DisplayName { get; set; }

Gets or sets the name to be displayed for the organization.

### string LogoHash { get; set; }

Gets or sets the organization&#39;s logo hash.

### string Name { get; set; }

Gets or sets the name of the organization.

### bool? PaidAccount { get; set; }

Gets or sets whether the organization is a paid account.

### List&lt;int&gt; PowerUps { get; set; }

Enumerates the powerups obtained by the organization.

### [IJsonOrganizationPreferences](/API-Json#ijsonorganizationpreferences) Prefs { get; set; }

Gets or sets a set of preferences for the organization.

### List&lt;string&gt; PremiumFeatures { get; set; }

Gets or sets a collection of premium features available to the organization.

### string Url { get; set; }

Gets or sets the URL to the organization&#39;s profile.

### string Website { get; set; }

Gets or sets the organization&#39;s website.

# IJsonOrganizationMembership

Defines the JSON structure for the OrganizationMembership object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonOrganizationMembership

## Properties

### [IJsonMember](/API-Json#ijsonmember) Member { get; set; }

Gets or sets the ID of the member.

### [OrganizationMembershipType](/API-Organizations#organizationmembershiptype)? MemberType { get; set; }

Gets or sets the membership type.

### bool? Unconfirmed { get; set; }

Gets or sets whether the membership is unconfirmed.

# IJsonOrganizationPreferences

Defines the JSON structure for the OrganizationPreferences object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonOrganizationPreferences

## Properties

### string AssociatedDomain { get; set; }

Gets or sets the Google Apps domain.

### [IJsonBoardVisibilityRestrict](/API-Json#ijsonboardvisibilityrestrict) BoardVisibilityRestrict { get; set; }

Gets or sets the visibility of boards owned by the organization.

### bool? ExternalMembersDisabled { get; set; }

Gets or sets whether external members are disabled.

### List&lt;Object&gt; OrgInviteRestrict { get; set; }

Gets or sets organization invitation restrictions.

### [OrganizationPermissionLevel](/API-Organizations#organizationpermissionlevel)? PermissionLevel { get; set; }

Gets or sets the permission level.

# IJsonParameter

Defines the JSON structure for a single-value parameter.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonParameter

## Properties

### bool? Boolean { get; set; }

Gets or sets a boolean parameter value;

### string String { get; set; }

Gets or sets a string parameter value;

# IJsonPosition

Defines the JSON structure for the Position object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonPosition

## Properties

### double? Explicit { get; set; }

Gets or sets an explicit numeric value for the position.

### string Named { get; set; }

Gets or sets a named value for the position.

#### Remarks

Valid values are &quot;top&quot; and &quot;bottom&quot;.

# IJsonPowerUp

Defines the JSON structure for the PowerUp object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonPowerUp

## Properties

### string Name { get; set; }

Gets or sets the name.

### bool? Public { get; set; }

Gets or sets whether the power-up is public.

### string Url { get; set; }

Gets or sets the URL for more information about the power-up.

# IJsonPowerUpData

Defines the JSON structure for the PowerUpData object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonPowerUpData

## Properties

### string PluginId { get; set; }

Gets or sets the power-up ID.

### string Value { get; set; }

Gets or sets the value.

# IJsonSearch

Defines the JSON structure for the Search object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonSearch

## Properties

### List&lt;IJsonAction&gt; Actions { get; set; }

Lists the IDs of actions which match the query.

### List&lt;IJsonBoard&gt; Boards { get; set; }

Lists the IDs of boards which match the query.

### List&lt;IJsonCard&gt; Cards { get; set; }

Lists the IDs of cards which match the query.

### List&lt;IJsonCacheable&gt; Context { get; set; }

Gets or sets a collection of boards, cards, and organizations within which the search should run.

### int? Limit { get; set; }

Gets or sets how many results to return;

### List&lt;IJsonMember&gt; Members { get; set; }

Lists the IDs of members which match the query.

### List&lt;IJsonOrganization&gt; Organizations { get; set; }

Lists the IDs of organizations which match the query.

### bool Partial { get; set; }

Gets or sets whether the search should match on partial words.

### string Query { get; set; }

Gets or sets the search query.

### [SearchModelType](/API-Searching#searchmodeltype)? Types { get; set; }

Gets or sets which types of objects should be returned.

# IJsonSticker

Defines the JSON structure for the Sticker object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonSticker

## Properties

### double? Left { get; set; }

Gets or sets the position of the left edge of the sticker.

### string Name { get; set; }

Gets or sets the name of the sticker.

### List&lt;IJsonImagePreview&gt; Previews { get; set; }

Gets or sets a collection of previews for the attachment.

### int? Rotation { get; set; }

Gets or sets the rotation angle of the sticker in degrees.

### double? Top { get; set; }

Gets or sets the position of the top edge of the sticker.

### string Url { get; set; }

Gets or sets the image&#39;s URL.

### int? ZIndex { get; set; }

Gets or sets the sticker&#39;s z-index.

# IJsonToken

Defines the JSON structure for the Token object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonToken

## Properties

### DateTime? DateCreated { get; set; }

Gets or sets the date the token was created.

### DateTime? DateExpires { get; set; }

Gets or sets the date the token will expire, if any.

### string Identifier { get; set; }

Gets or sets the identifier of the application which requested the token.

### [IJsonMember](/API-Json#ijsonmember) Member { get; set; }

Gets or sets the ID of the member who issued the token.

### List&lt;IJsonTokenPermission&gt; Permissions { get; set; }

Gets or sets the collection of permissions granted by the token.

# TokenModelType

Enumerates the model types to which a user token may grant access.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- TokenModelType

## Fields

### Unknown

Assigned when the model type is not recognized.

### Member

Indicates the model is one or more Members.

### Board

Indicates the model is one or more Boards.

### Organization

Indicates the model is one or more Organizations.

# IJsonTokenPermission

Defines the JSON structure for the TokenPermission object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonTokenPermission

## Properties

### string IdModel { get; set; }

Gets or sets the ID of the model to which a token grants permissions.

### [TokenModelType](/API-Json#tokenmodeltype)? ModelType { get; set; }

Gets or sets the type of the model.

### bool? Read { get; set; }

Gets or sets whether a token grants read permissions to the model.

### bool? Write { get; set; }

Gets or sets whether a token grants write permissions to the model.

# IJsonWebhook

Defines the JSON structure for the Webhook object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonWebhook

## Properties

### bool? Active { get; set; }

Gets or sets whether the webhook is active.

### string CallbackUrl { get; set; }

Gets or sets the URL which receives notification messages.

### string Description { get; set; }

Gets or sets the description of the webhook.

### string IdModel { get; set; }

Gets or sets the ID of the entity which the webhook monitors.

# IJsonWebhookNotification

Defines the JSON structure for the WebhookNotification object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonWebhookNotification

## Properties

### [IJsonAction](/API-Json#ijsonaction) Action { get; set; }

Gets or sets the action associated with the notification.

