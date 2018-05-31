---
title: Card
category: API
order: 24
---

Represents a card.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Card

## Constructors

### Card(string id, TrelloAuthorization auth = null)

Creates a new instance of the Manatee.Trello.Card object.

**Parameter:** id

The card&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, Manatee.Trello.TrelloAuthorization.Default will be used.

#### Remarks

The supplied ID can be either the full or short ID.

## Properties

### static Card.Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### IReadOnlyCollection&lt;[IAction](../IAction#iaction)&gt; Actions { get; }

Gets the collection of actions performed on this card.

#### Remarks

By default imposed by Trello, this contains actions of type Manatee.Trello.ActionType.CommentCard.

### [IAttachmentCollection](../IAttachmentCollection#iattachmentcollection) Attachments { get; }

Gets the collection of attachments contained in the card.

### [IBadges](../IBadges#ibadges) Badges { get; }

Gets the badges summarizing the card&#39;s content.

### [IBoard](../IBoard#iboard) Board { get; }

Gets the board to which the card belongs.

### [ICheckListCollection](../ICheckListCollection#ichecklistcollection) CheckLists { get; }

Gets the collection of checklists contained in the card.

### [ICommentCollection](../ICommentCollection#icommentcollection) Comments { get; }

Gets the collection of comments made on the card.

### DateTime CreationDate { get; }

Gets the creation date of the card.

### IReadOnlyCollection&lt;[ICustomField](../ICustomField#icustomfield)&gt; CustomFields { get; }

Gets the collection of custom field values for the card.

### string Description { get; set; }

Gets or sets the card&#39;s description.

### DateTime? DueDate { get; set; }

Gets or sets the card&#39;s due date.

### string Id { get; }

Gets the card&#39;s ID.

### bool? IsArchived { get; set; }

Gets or sets whether the card is archived.

### bool? IsComplete { get; set; }

Gets or sets whether the card is complete. Associated with Manatee.Trello.Card.DueDate.

### bool? IsSubscribed { get; set; }

Gets or sets whether the current member is subscribed to the card.

### [ICheckList](../ICheckList#ichecklist) this[string key] { get; }

Retrieves a check list which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list, or null if none found.

#### Remarks

Matches on checklist ID and name. Comparison is case-sensitive.

### [ICheckList](../ICheckList#ichecklist) this[int index] { get; }

Retrieves the check list at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The check list.

### [ICardLabelCollection](../ICardLabelCollection#icardlabelcollection) Labels { get; }

Gets the collection of labels on the card.

### DateTime? LastActivity { get; }

Gets the most recent date of activity on the card.

### [IList](../IList#ilist) List { get; set; }

Gets or sets the list to the card belongs.

### [IMemberCollection](../IMemberCollection#imembercollection) Members { get; }

Gets the collection of members who are assigned to the card.

### string Name { get; set; }

Gets or sets the card&#39;s name.

### [Position](../Position#position) Position { get; set; }

Gets or sets the card&#39;s position.

### IReadOnlyCollection&lt;[IPowerUpData](../IPowerUpData#ipowerupdata)&gt; PowerUpData { get; }

Gets card-specific power-up data.

### int? ShortId { get; }

Gets the card&#39;s short ID.

### string ShortUrl { get; }

Gets the card&#39;s short URL.

#### Remarks

Because this value does not change, it can be used as a permalink.

### [ICardStickerCollection](../ICardStickerCollection#icardstickercollection) Stickers { get; }

Gets the collection of stickers which appear on the card.

### string Url { get; }

Gets the card&#39;s full URL.

#### Remarks

Trello will likely change this value as the name changes. You can use Manatee.Trello.Card.ShortUrl for permalinks.

### IReadOnlyCollection&lt;[IMember](../IMember#imember)&gt; VotingMembers { get; }

Gets all members who have voted for this card.

## Events

### Action&lt;[ICard](../ICard#icard), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the card is updated.

## Methods

### void ApplyAction(IAction action)

Applies the changes an action represents.

**Parameter:** action

The action.

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the card.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the card from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct = default(CancellationToken))

Refreshes the card data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

