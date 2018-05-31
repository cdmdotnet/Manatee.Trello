---
title: NotificationData
category: API
order: 204
---

Exposes any data associated with a notification.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- NotificationData

## Properties

### [IAttachment](../IAttachment#iattachment) Attachment { get; }

Gets an assocated attachment.

#### Associated Notification Types

- AddedAttachmentToCard

### [IBoard](../IBoard#iboard) Board { get; }

Gets an assocated board.

#### Associated Notification Types

- AddedToBoard
- AddAdminToBoard
- CloseBoard
- RemovedFromBoard
- MakeAdminOfBoard

### [IBoard](../IBoard#iboard) BoardSource { get; }

Gets an assocated board.

### [IBoard](../IBoard#iboard) BoardTarget { get; }

Gets an assocated board.

### [ICard](../ICard#icard) Card { get; }

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

### [ICard](../ICard#icard) CardSource { get; }

Gets an assocated card.

### [ICheckItem](../ICheckItem#icheckitem) CheckItem { get; }

Gets an assocated checklist item.

#### Associated Notification Types

- UpdateCheckItemStateOnCard

### [ICheckList](../ICheckList#ichecklist) CheckList { get; }

Gets an assocated checklist.

### [IList](../IList#ilist) List { get; }

Gets an assocated list.

### [IList](../IList#ilist) ListAfter { get; }

Gets the current list.

#### Remarks

For some action types, this information may be in the Manatee.Trello.NotificationData.List or Manatee.Trello.NotificationData.OldList properties.

### [IList](../IList#ilist) ListBefore { get; }

Gets the previous list.

#### Remarks

For some action types, this information may be in the Manatee.Trello.NotificationData.List or Manatee.Trello.NotificationData.OldList properties.

### [IMember](../IMember#imember) Member { get; }

Gets an assocated member.

#### Associated Notification Types

- AddedMemberToCard
- RemovedMemberFromCard
- MentionedOnCard

### string OldDescription { get; }

Gets the previous description.

#### Associated Notification Types

- ChangeCard

### [IList](../IList#ilist) OldList { get; }

Gets the previous list.

#### Remarks

For some action types, this information may be in the Manatee.Trello.NotificationData.ListAfter or Manatee.Trello.NotificationData.ListBefore properties.

### [Position](../Position#position) OldPosition { get; }

Gets the previous position.

#### Associated Notification Types

- ChangeCard

### string OldText { get; }

Gets the previous text value.

#### Associated Notification Types

- CommentCard

### [IOrganization](../IOrganization#iorganization) Organization { get; }

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

