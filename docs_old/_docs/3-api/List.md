---
title: List
category: API
order: 189
---

Represents a list.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- List

## Constructors

### List(string id, TrelloAuthorization auth = null)

Creates a new instance of the Manatee.Trello.List object.

**Parameter:** id

The list&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, Manatee.Trello.TrelloAuthorization.Default will be used.

## Properties

### static List.Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [IReadOnlyActionCollection](../IReadOnlyActionCollection#ireadonlyactioncollection) Actions { get; }

Gets the collection of actions performed on the list.

### [IBoard](../IBoard#iboard) Board { get; set; }

Gets or sets the board on which the list belongs.

### [ICardCollection](../ICardCollection#icardcollection) Cards { get; }

Gets the collection of cards contained in the list.

### DateTime CreationDate { get; }

Gets the creation date of the list.

### string Id { get; }

Gets the list&#39;s ID.

### bool? IsArchived { get; set; }

Gets or sets whether the list is archived.

### bool? IsSubscribed { get; set; }

Gets or sets whether the current member is subscribed to the list.

### [ICard](../ICard#icard) this[string key] { get; }

Retrieves a card which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching card, or null if none found.

#### Remarks

Matches on card ID and name. Comparison is case-sensitive.

### [ICard](../ICard#icard) this[int index] { get; }

Retrieves the card at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The card.

### string Name { get; set; }

Gets the list&#39;s name.

### [Position](../Position#position) Position { get; set; }

Gets the list&#39;s position.

## Events

### Action&lt;[IList](../IList#ilist), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the list is updated.

## Methods

### void ApplyAction(IAction action)

Applies the changes an action represents.

**Parameter:** action

The action.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the label data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

