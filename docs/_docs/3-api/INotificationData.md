---
title: INotificationData
category: API
order: 139
---

Exposes any data associated with a notification.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- INotificationData

## Properties

### [IAttachment](../IAttachment#iattachment) Attachment { get; }

Gets an assocated attachment.

### [IBoard](../IBoard#iboard) Board { get; }

Gets an assocated board.

### [IBoard](../IBoard#iboard) BoardSource { get; }

Gets an assocated board.

### [IBoard](../IBoard#iboard) BoardTarget { get; }

Gets an assocated board.

### [ICard](../ICard#icard) Card { get; }

Gets an assocated card.

### [ICard](../ICard#icard) CardSource { get; }

Gets an assocated card.

### [ICheckItem](../ICheckItem#icheckitem) CheckItem { get; }

Gets an assocated checklist item.

### [ICheckList](../ICheckList#ichecklist) CheckList { get; }

Gets an assocated checklist.

### [IList](../IList#ilist) List { get; }

Gets an assocated list.

### [IList](../IList#ilist) ListAfter { get; }

Gets the current list.

#### Remarks

For some action types, this information may be in the Manatee.Trello.INotificationData.List or Manatee.Trello.INotificationData.OldList properties.

### [IList](../IList#ilist) ListBefore { get; }

Gets the previous list.

#### Remarks

For some action types, this information may be in the Manatee.Trello.INotificationData.List or Manatee.Trello.INotificationData.OldList properties.

### [IMember](../IMember#imember) Member { get; }

Gets an assocated member.

### string OldDescription { get; }

Gets the previous description.

### [IList](../IList#ilist) OldList { get; }

Gets the previous list.

#### Remarks

For some action types, this information may be in the Manatee.Trello.INotificationData.ListAfter or Manatee.Trello.INotificationData.ListBefore properties.

### [Position](../Position#position) OldPosition { get; }

Gets the previous position.

### string OldText { get; }

Gets the previous text value.

### [IOrganization](../IOrganization#iorganization) Organization { get; }

Gets an assocated organization.

### string Text { get; set; }

Gets assocated text.

### bool? WasArchived { get; }

Gets whether the object was previously archived.

