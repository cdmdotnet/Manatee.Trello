---
title: IActionData
category: API
order: 53
---

Exposes any data associated with an action.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IActionData

## Properties

### [IAttachment](../IAttachment#iattachment) Attachment { get; }

Gets an assocated attachment.

#### Associated Action Types

- AddAttachmentToCard
- DeleteAttachmentFromCard

### [IBoard](../IBoard#iboard) Board { get; }

Gets an assocated board.

#### Associated Action Types

- AddMemberToBoard
- AddToOrganizationBoard
- CreateBoard
- DeleteBoardInvitation
- MakeAdminOfBoard
- MakeNormalMemberOfBoard
- MakeObserverOfBoard
- RemoveFromOrganizationBoard
- UnconfirmedBoardInvitation
- UpdateBoard

### [IBoard](../IBoard#iboard) BoardSource { get; }

Gets an assocated board.

#### Associated Action Types

- CopyBoard

### [IBoard](../IBoard#iboard) BoardTarget { get; }

Gets an assocated board.

#### Associated Action Types

- CopyBoardx

### [ICard](../ICard#icard) Card { get; }

Gets an assocated card.

#### Associated Action Types

- AddAttachmentToCard
- AddChecklistToCard
- AddMemberToCard
- CommentCard
- ConvertToCardFromCheckItem
- CopyCommentCard
- CreateCard
- DeleteAttachmentFromCard
- DeleteCard
- EmailCard
- MoveCardFromBoard
- MoveCardToBoard
- RemoveChecklistFromCard
- RemoveMemberFromCard
- UpdateCard
- UpdateCardClosed
- UpdateCardDesc
- UpdateCardIdList
- UpdateCardName
- UpdateCheckItemStateOnCard

### [ICard](../ICard#icard) CardSource { get; }

Gets an assocated card.

#### Associated Action Types

- CopyCard

### [ICheckItem](../ICheckItem#icheckitem) CheckItem { get; }

Gets an assocated checklist item.

#### Associated Action Types

- ConvertToCardFromCheckItem
- UpdateCheckItemStateOnCard

### [ICheckList](../ICheckList#ichecklist) CheckList { get; }

Gets an assocated checklist.

#### Associated Action Types

- AddChecklistToCard
- RemoveChecklistFromCard
- UpdateChecklist

### [ICustomFieldDefinition](../ICustomFieldDefinition#icustomfielddefinition) CustomField { get; }

Gets an associated custom field definition.

#### Associated Action Types

- UpdateCustomField
- UpdateCustomFieldItem

### [ILabel](../ILabel#ilabel) Label { get; }

Gets the associated label.

#### Associated Action Types

- AddLabelToCard
- CreateLabel
- DeleteLabel
- RemoveLabelFromCard
- UpdateLabel

### DateTime? LastEdited { get; }

Gets the date/time a comment was last edited.

### [IList](../IList#ilist) List { get; }

Gets an assocated list.

#### Associated Action Types

- CreateList
- MoveListFromBoard
- MoveListToBoard
- UpdateList
- UpdateListClosed
- UpdateListName

### [IList](../IList#ilist) ListAfter { get; }

Gets the current list.

#### Associated Action Types

- UpdateCardIdList

#### Remarks

For some action types, this information may be in the Manatee.Trello.IActionData.List or Manatee.Trello.IActionData.OldList properties.

### [IList](../IList#ilist) ListBefore { get; }

Gets the previous list.

#### Associated Action Types

- UpdateCardIdList

#### Remarks

For some action types, this information may be in the Manatee.Trello.IActionData.List or Manatee.Trello.IActionData.OldList properties.

### [IMember](../IMember#imember) Member { get; }

Gets an assocated member.

#### Associated Action Types

- AddMemberToBoard
- AddMemberToCard
- AddMemberToOrganization
- MakeNormalMemberOfBoard
- MakeNormalMemberOfOrganization
- MemberJoinedTrello
- RemoveMemberFromCard
- UpdateMember

### string OldDescription { get; }

Gets the previous description.

#### Associated Action Types

- UpdateCard
- UpdateCardDesc

### [IList](../IList#ilist) OldList { get; }

Gets the previous list.

#### Associated Action Types

- UpdateCard
- UpdateCardIdList

#### Remarks

For some action types, this information may be in the Manatee.Trello.IActionData.ListAfter or Manatee.Trello.IActionData.ListBefore properties.

### [Position](../Position#position) OldPosition { get; }

Gets the previous position.

#### Associated Action Types

- UpdateCard
- UpdateList
- UpdateCustomField

### string OldText { get; }

Gets the previous text value.

#### Associated Action Types

- UpdateCard
- CommentCard

### [IOrganization](../IOrganization#iorganization) Organization { get; }

Gets an associated organization.

#### Associated Action Types

- AddMemberToOrganization
- AddToOrganizationBoard
- CreateOrganization
- DeleteOrganizationInvitation
- MakeNormalMemberOfOrganization
- RemoveFromOrganizationBoard
- UnconfirmedOrganizationInvitation
- UpdateOrganization

### [IPowerUp](../IPowerUp#ipowerup) PowerUp { get; }

Gets an associated power-up.

#### Associated Action Types

- DisablePowerUp
- EnablePowerUp

### string Text { get; set; }

Gets assocated text.

#### Associated Action Types

- CommentCard

### string Value { get; }

Gets a custom value associate with the action if any.

### bool? WasArchived { get; }

Gets whether the object was previously archived.

#### Associated Action Types

- UpdateCardClosed
- UpdateListClosed

