---
title: Actions
category: Version 2.x - API
order: 1
---

# Action

Documents all of the activities in Trello.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Action

## Constructors

### Action(string id, TrelloAuthorization auth)

Creates a new [Action](/API-Actions#action) instance.

**Parameter:** id

The action&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](/API-Configuration#static-trelloauthorization-default--get-) will be used.

## Properties

### static Manatee.Trello.Action+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### DateTime CreationDate { get; }

Gets the creation date.

### [Member](/API-Members#member) Creator { get; }

Gets the member who performed the action.

### [ActionData](/API-Actions#actiondata) Data { get; }

Gets any associated data.

### DateTime? Date { get; }

Gets the date and time at which the action was performed.

### string Id { get; }

Gets the action&#39;s ID.

### [ActionType](/API-Actions#actiontype)? Type { get; }

Gets the type of action.

## Events

### Action&lt;Action, IEnumerable&lt;string&gt;&gt; Updated

Raised when any data on the [Action](/API-Actions#action) instance is updated.

## Methods

### void Delete()

Permanently deletes the action from Trello.

#### Remarks

This instance will remain in memory and all properties will remain accessible.

### string ToString()

Returns a string that represents the action. The content will vary based on the value of [Type](/API-Actions#actiontype-type--get-).

**Returns:** A string that represents the action.

# ActionData

Exposes any data associated with an action.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ActionData

## Properties

### [Attachment](/API-Attachments#attachment) Attachment { get; }

Gets an assocated attachment.

#### Associated Action Types

- AddAttachmentToCard
- DeleteAttachmentFromCard

### [Board](/API-Boards#board) Board { get; }

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

### [Board](/API-Boards#board) BoardSource { get; }

Gets an assocated board.

#### Associated Action Types

- CopyBoard

### [Board](/API-Boards#board) BoardTarget { get; }

Gets an assocated board.

#### Associated Action Types

- CopyBoardx

### [Card](/API-Cards#card) Card { get; }

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

### [Card](/API-Cards#card) CardSource { get; }

Gets an assocated card.

#### Associated Action Types

- CopyCard

### [CheckItem](/API-Cards#checkitem) CheckItem { get; }

Gets an assocated checklist item.

#### Associated Action Types

- ConvertToCardFromCheckItem
- UpdateCheckItemStateOnCard

### [CheckList](/API-Cards#checklist) CheckList { get; }

Gets an assocated checklist.

#### Associated Action Types

- AddChecklistToCard
- RemoveChecklistFromCard
- UpdateChecklist

### [Label](/API-Labels#label) Label { get; }

Gets the associated label.

### DateTime? LastEdited { get; }

Gets the date/time a comment was last edited.

### [List](/API-Lists#list) List { get; }

Gets an assocated list.

#### Associated Action Types

- CreateList
- MoveListFromBoard
- MoveListToBoard
- UpdateList
- UpdateListClosed
- UpdateListName

### [List](/API-Lists#list) ListAfter { get; }

Gets the current list.

#### Associated Action Types

- UpdateCardIdList

### [List](/API-Lists#list) ListBefore { get; }

Gets the previous list.

#### Associated Action Types

- UpdateCardIdList

### [Member](/API-Members#member) Member { get; }

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

### [List](/API-Lists#list) OldList { get; }

Gets the previous list.

#### Associated Action Types

- UpdateCard
- UpdateCardIdList

### [Position](/API-Common-Types#position) OldPosition { get; }

Gets the previous position.

#### Associated Action Types

- UpdateCard
- UpdateList

### string OldText { get; }

Gets the previous text value.

#### Associated Action Types

- UpdateCard
- CommentCard

### [Organization](/API-Organizations#organization) Organization { get; }

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

### [PowerUpBase](/API-Power-Ups#powerupbase) PowerUp { get; }

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

# ActionType

Enumerates known types of [Action](/API-Actions#action)s.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- ActionType

## Fields

### static [ActionType](/API-Actions#actiontype) AddAdminToBoard

Indicates a [Member](/API-Members#member) was added to a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) AddAdminToOrganization

Indicates a [Member](/API-Members#member) was added to a [Organization](/API-Organizations#organization).

### static [ActionType](/API-Actions#actiontype) AddAttachmentToCard

Indicates an [Attachment](/API-Attachments#attachment) was added to a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) AddBoardsPinnedToMember

Indicates a [Member](/API-Members#member) pinned a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) AddChecklistToCard

Indicates a [CheckList](/API-Cards#checklist) was added to a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) AddLabelToCard

Indicates a [Label](/API-Labels#label) was added to a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) AddMemberToBoard

Indicates a [Member](/API-Members#member) was added to a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) AddMemberToCard

Indicates a [Member](/API-Members#member) was added to a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) AddMemberToOrganization

Indicates a [Member](/API-Members#member) was added to an [Organization](/API-Organizations#organization).

### static [ActionType](/API-Actions#actiontype) AddToOrganizationBoard

Indicates a [Organization](/API-Organizations#organization) was added to a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) CommentCard

Indicates a comment was added to a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) ConvertToCardFromCheckItem

Indicates a [CheckList](/API-Cards#checklist) item was converted to [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) CopyBoard

Indicates a [Board](/API-Boards#board) was copied.

### static [ActionType](/API-Actions#actiontype) CopyCard

Indicates a [Card](/API-Cards#card) was copied.

### static [ActionType](/API-Actions#actiontype) CopyChecklist

Indicates a [CheckList](/API-Cards#checklist) was copied.

### static [ActionType](/API-Actions#actiontype) CopyCommentCard

Indicates a comment was copied from one [Card](/API-Cards#card) to another.

### static [ActionType](/API-Actions#actiontype) CreateBoard

Indicates a [Board](/API-Boards#board) was created.

### static [ActionType](/API-Actions#actiontype) CreateBoardInvitation

Indicates a [Member](/API-Members#member) was invided to a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) CreateBoardPreference

Indicates a [Board](/API-Boards#board) preference was created.

### static [ActionType](/API-Actions#actiontype) CreateCard

Indicates a [Card](/API-Cards#card) was created.

### static [ActionType](/API-Actions#actiontype) CreateChecklist

Indicates a [CheckList](/API-Cards#checklist) was created.

### static [ActionType](/API-Actions#actiontype) CreateLabel

Indicates a [Label](/API-Labels#label) was created.

### static [ActionType](/API-Actions#actiontype) CreateList

Indicates a [List](/API-Lists#list) was created.

### static [ActionType](/API-Actions#actiontype) CreateOrganization

Indicates an [Organization](/API-Organizations#organization) was created.

### static [ActionType](/API-Actions#actiontype) CreateOrganizationInvitation

Indicates a [Member](/API-Members#member) was invided to an [Organization](/API-Organizations#organization).

### static [ActionType](/API-Actions#actiontype) DeleteAttachmentFromCard

Indicates an [Attachment](/API-Attachments#attachment) was deleted from a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) DeleteBoardInvitation

Indicates an invitation to a [Board](/API-Boards#board) was rescinded.

### static [ActionType](/API-Actions#actiontype) DeleteCard

Indicates a [Card](/API-Cards#card) was deleted.

### static [ActionType](/API-Actions#actiontype) DeleteCheckItem

Indicates a [CheckItem](/API-Cards#checkitem) was deleted.

### static [ActionType](/API-Actions#actiontype) DeleteLabel

Indicates a [Label](/API-Labels#label) was deleted.

### static [ActionType](/API-Actions#actiontype) DeleteOrganizationInvitation

Indicates an invitation to an [Organization](/API-Organizations#organization) was rescinded.

### static [ActionType](/API-Actions#actiontype) DisablePlugin

Indicates a power-up was disabled.

### static [ActionType](/API-Actions#actiontype) DisablePowerUp

Indicates a power-up was disabled.

### static [ActionType](/API-Actions#actiontype) EmailCard

Indicates a [Card](/API-Cards#card) was created via email.

### static [ActionType](/API-Actions#actiontype) EnablePlugin

Indicates a power-up was enabled.

### static [ActionType](/API-Actions#actiontype) EnablePowerUp

Indicates a power-up was enabled.

### static [ActionType](/API-Actions#actiontype) MakeAdminOfBoard

Indicates a [Member](/API-Members#member) was made an admin of a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) MakeAdminOfOrganization

Indicates a [Member](/API-Members#member) was made an admin of an [Organization](/API-Organizations#organization).

### static [ActionType](/API-Actions#actiontype) MakeNormalMemberOfBoard

Indicates a [Member](/API-Members#member) was made a normal [Member](/API-Members#member) of a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) MakeNormalMemberOfOrganization

Indicates a [Member](/API-Members#member) was made a normal [Member](/API-Members#member) of an [Organization](/API-Organizations#organization).

### static [ActionType](/API-Actions#actiontype) MakeObserverOfBoard

Indicates a [Member](/API-Members#member) was made an observer of a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) MemberJoinedTrello

Indicates a [Member](/API-Members#member) joined Trello.

### static [ActionType](/API-Actions#actiontype) MoveCardFromBoard

Indicates a [Card](/API-Cards#card) was moved from one [Board](/API-Boards#board) to another.

### static [ActionType](/API-Actions#actiontype) MoveCardToBoard

Indicates a [Card](/API-Cards#card) was moved from one [Board](/API-Boards#board) to another.

### static [ActionType](/API-Actions#actiontype) MoveListFromBoard

Indicates a [List](/API-Lists#list) was moved from one [Board](/API-Boards#board) to another.

### static [ActionType](/API-Actions#actiontype) MoveListToBoard

Indicates a [List](/API-Lists#list) was moved from one [Board](/API-Boards#board) to another.

### static [ActionType](/API-Actions#actiontype) RemoveAdminFromBoard

Indicates a [Member](/API-Members#member) was removed as an admin from a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) RemoveAdminFromOrganization

Indicates a [Member](/API-Members#member) was removed as an admin from an [Organization](/API-Organizations#organization).

### static [ActionType](/API-Actions#actiontype) RemoveBoardsPinnedFromMember

Indicates an [Member](/API-Members#member) unpinnned a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) RemoveChecklistFromCard

Indicates a [CheckList](/API-Cards#checklist) was removed from a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) RemoveFromOrganizationBoard

Indicates an [Organization](/API-Organizations#organization) was removed from a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) RemoveLabelFromCard

Indicates a [Label](/API-Labels#label) was removed from a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) RemoveMemberFromBoard

Indicates a [Member](/API-Members#member) was removed from a [Board](/API-Boards#board).

### static [ActionType](/API-Actions#actiontype) RemoveMemberFromCard

Indicates a [Member](/API-Members#member) was removed from a [Card](/API-Cards#card).

### static [ActionType](/API-Actions#actiontype) RemoveMemberFromOrganization

Indicates a [Member](/API-Members#member) was removed from an [Organization](/API-Organizations#organization).

### static [ActionType](/API-Actions#actiontype) UnconfirmedBoardInvitation

Indicates an invitation to a [Board](/API-Boards#board) was created.

### static [ActionType](/API-Actions#actiontype) UnconfirmedOrganizationInvitation

Indicates an invitation to an [Organization](/API-Organizations#organization) was created.

### static [ActionType](/API-Actions#actiontype) Unknown

Not recognized. May have been created since the current version of this API.

#### Remarks

This value is not supported by Trello&#39;s API.

### static [ActionType](/API-Actions#actiontype) UpdateBoard

Indicates a [Board](/API-Boards#board) was updated.

### static [ActionType](/API-Actions#actiontype) UpdateCard

Indicates a [Card](/API-Cards#card) was updated.

### static [ActionType](/API-Actions#actiontype) UpdateCheckItem

Indicates a [CheckItem](/API-Cards#checkitem) was updated.

### static [ActionType](/API-Actions#actiontype) UpdateCheckItemStateOnCard

Indicates a [CheckList](/API-Cards#checklist) was updated.

### static [ActionType](/API-Actions#actiontype) UpdateChecklist

Indicates a [CheckList](/API-Cards#checklist) was updated.

### static [ActionType](/API-Actions#actiontype) UpdateLabel

Indicates a [Label](/API-Labels#label) was updated.

### static [ActionType](/API-Actions#actiontype) UpdateList

Indicates a [List](/API-Lists#list) was updated.

### static [ActionType](/API-Actions#actiontype) UpdateMember

Indicates a [Member](/API-Members#member) was updated.

### static [ActionType](/API-Actions#actiontype) UpdateOrganization

Indicates an [Organization](/API-Organizations#organization) was updated.

### static [ActionType](/API-Actions#actiontype) VoteOnCard

Indicates a [Member](/API-Members#member) voted for a [Card](/API-Cards#card).

## Properties

### static [ActionType](/API-Actions#actiontype) All { get; }

Indicates all action types

### static [ActionType](/API-Actions#actiontype) DefaultForCardActions { get; }

Indictes the default set of values returned by [Actions](/API-Cards#readonlyactioncollection-actions--get-).

## Methods

### static String[] GetNames()

Gets the names of this ActionType enumerated type.

**Returns:** 

### static Manatee.Trello.ActionType[] GetValues()

Gets all the values of this ActionType enumerated type.

**Returns:** 

### static [ActionType](/API-Actions#actiontype) op_BitwiseAnd(ActionType lhs, ActionType rhs)

AND operator. And together ActionType instances.

**Parameter:** lhs



**Parameter:** rhs



**Returns:** 

### static [ActionType](/API-Actions#actiontype) op_BitwiseOr(ActionType lhs, ActionType rhs)

OR operator. Or together ActionType instances.

**Parameter:** lhs



**Parameter:** rhs



**Returns:** 

### static bool op_Equality(ActionType lhs, ActionType rhs)

Equality operator.

**Parameter:** lhs



**Parameter:** rhs



**Returns:** 

### static [ActionType](/API-Actions#actiontype) op_ExclusiveOr(ActionType lhs, ActionType rhs)

XOR operator. Xor together ActionType instances.

**Parameter:** lhs



**Parameter:** rhs



**Returns:** 

### static bool op_Inequality(ActionType lhs, ActionType rhs)

Inequality operator.

**Parameter:** lhs



**Parameter:** rhs



**Returns:** 

### int CompareTo(ActionType other)

Compares based on highest bit set. Instance with higher
bit set is bigger.

**Parameter:** other



**Returns:** 

### bool Equals(Object obj)

Overridden. Compares equality with another object.

**Parameter:** obj



**Returns:** 

### bool Equals(ActionType other)

Strongly-typed equality method.

**Parameter:** other



**Returns:** 

### bool Equals(Object obj)

Overridden. Compares equality with another object.

**Parameter:** obj



**Returns:** 

### bool Equals(Object obj)

Overridden. Compares equality with another object.

**Parameter:** obj



**Returns:** 

### int GetHashCode()

Overridden. Gets the hash code of the internal BitArray.

**Returns:** 

### TypeCode GetTypeCode()

Returns TypeCode.Object.

**Returns:** 

### bool HasFlag(ActionType flags)

Checks *flags* to see if all the bits set in
that flags are also set in this flags.

**Parameter:** flags



**Returns:** 

### string ToString()

Overridden. Returns a comma-separated string.

**Returns:** 

# ReadOnlyActionCollection

A read-only collection of actions.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Action&gt;
- ReadOnlyActionCollection

# CommentCollection

A collection of [Action](/API-Actions#action)s of types [ActionType.CommentCard](/API-Actions#static-actiontype-commentcard) and [ActionType.CopyCommentCard](/API-Actions#static-actiontype-copycommentcard).

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Action&gt;
- ReadOnlyActionCollection
- CommentCollection

## Methods

### [Action](/API-Actions#action) Add(string text)

Posts a new comment to a card.

**Parameter:** text

The content of the comment.

**Returns:** The [Action](/API-Actions#action) associated with the comment.

