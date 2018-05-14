# Notification

Represents a notification.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Notification

## Constructors

### Notification(string id, TrelloAuthorization auth)

Creates a new [Notification](API-Notifications#notification) object.

**Parameter:** id

The notification&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided,
[TrelloAuthorization.Default](API-Configuration#static-trelloauthorization-default--get-) will be used.

## Properties

### static Manatee.Trello.Notification+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### DateTime CreationDate { get; }

Gets the creation date of the notification.

### [Member](API-Members#member) Creator { get; }

Gets the member who performed the action which created the notification.

### [NotificationData](API-Notifications#notificationdata) Data { get; }

Gets any data associated.

### DateTime? Date { get; }

Gets the date and time at which the notification was issued.

### string Id { get; }

Gets the notification&#39;s ID.

### bool? IsUnread { get; set; }

Gets or sets whether the notification has been read.

### [NotificationType](API-Notifications#notificationtype)? Type { get; }

Gets the type of notification.

## Events

### Action&lt;Notification, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the notification is updated.

## Methods

### string ToString()

Returns a string that represents the notification. The content will vary based on the value of [Type](API-Notifications#notificationtype-type--get-).

**Returns:** A string that represents the current object.

# NotificationData

Exposes any data associated with a notification.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- NotificationData

## Properties

### [Attachment](API-Attachments#attachment) Attachment { get; }

Gets an assocated attachment.

#### Associated Notification Types

- AddedAttachmentToCard

### [Board](API-Boards#board) Board { get; }

Gets an assocated board.

#### Associated Notification Types

- AddedToBoard
- AddAdminToBoard
- CloseBoard
- RemovedFromBoard
- MakeAdminOfBoard

### [Board](API-Boards#board) BoardSource { get; }

Gets an assocated board.

### [Board](API-Boards#board) BoardTarget { get; }

Gets an assocated board.

### [Card](API-Cards#card) Card { get; }

Gets an assocated card.

#### Associated Notification Types

- AddedAttachmentToCard
- AddedToCard
- AddedMemberToCard
- ChangeCard
- CommentCard
- CreatedCard
- RemovedFromCard
- RemovedMemberFromCard
- MentionedOnCard
- UpdateCheckItemStateOnCard
- CardDueSoon

### [Card](API-Cards#card) CardSource { get; }

Gets an assocated card.

### [CheckItem](API-Cards#checkitem) CheckItem { get; }

Gets an assocated checklist item.

#### Associated Notification Types

- UpdateCheckItemStateOnCard

### [CheckList](API-Cards#checklist) CheckList { get; }

Gets an assocated checklist.

### [List](API-Lists#list) List { get; }

Gets an assocated list.

### [List](API-Lists#list) ListAfter { get; }

Gets the current list.

#### Remarks

For some action types, this information may be in the [List](API-Notifications#list-list--get-) or [OldList](API-Notifications#list-oldlist--get-) properties.

### [List](API-Lists#list) ListBefore { get; }

Gets the previous list.

#### Remarks

For some action types, this information may be in the [List](API-Notifications#list-list--get-) or [OldList](API-Notifications#list-oldlist--get-) properties.

### [Member](API-Members#member) Member { get; }

Gets an assocated member.

#### Associated Notification Types

- AddedMemberToCard
- RemovedMemberFromCard
- MentionedOnCard

### string OldDescription { get; }

Gets the previous description.

#### Associated Notification Types

- ChangeCard

### [List](API-Lists#list) OldList { get; }

Gets the previous list.

#### Remarks

For some action types, this information may be in the [ListAfter](API-Notifications#list-listafter--get-) or [ListBefore](API-Notifications#list-listbefore--get-) properties.

### [Position](API-Common-Types#position) OldPosition { get; }

Gets the previous position.

#### Associated Notification Types

- ChangeCard

### string OldText { get; }

Gets the previous text value.

#### Associated Notification Types

- CommentCard

### [Organization](API-Organizations#organization) Organization { get; }

Gets an assocated organization.

#### Associated Notification Types

- AddedToOrganization
- AddAdminToOrganization
- RemovedFromOrganization
- MakeAdminOfOrganization

### string Text { get; set; }

Gets assocated text.

#### Associated Notification Types

- CommentCard

### bool? WasArchived { get; }

Gets whether the object was previously archived.

#### Associated Notification Types

- ChangeCard
- CloseBoard

# NotificationType

Enumerates known types of notifications.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- NotificationType

## Fields

### Unknown

Not recognized. May have been created since the current version of this API.

### AddedAttachmentToCard

Indicates an attachment was added to a card.

### AddedToBoard

Indicates the current member was added to a board.

### AddedToCard

Indicates the current member was added to a card.

### AddedToOrganization

Indicates the current member was added to an organization.

### AddedMemberToCard

Indicates another member was added to an card.

### AddAdminToBoard

Indicates the current member was added to a board as an admin.

### AddAdminToOrganization

Indicates the current member was added to an organization as an admin.

### ChangeCard

Indicates a card was changed.

### CloseBoard

Indicates a board was closed.

### CommentCard

Indicates another member commented on a card.

### CreatedCard

Indicates another member created a card.

### RemovedFromBoard

Indicates the current member was removed from a board.

### RemovedFromCard

Indicates the current member was removed from a card.

### RemovedMemberFromCard

Indicates another member was removed from a card.

### RemovedFromOrganization

Indicates the current member was removed from an organization.

### MentionedOnCard

Indicates the current member was mentioned on a card.

### UpdateCheckItemStateOnCard

Indicates a checklist item was updated.

### MakeAdminOfBoard

Indicates the current member was made an admin of a board.

### MakeAdminOfOrganization

Indicates the current member was made an admin of an organization.

### CardDueSoon

Indicates a card due date is approaching.

### All

Indicates all notification types.

# ReadOnlyNotificationCollection

A read-only collection of notifications.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Notification&gt;
- ReadOnlyNotificationCollection

