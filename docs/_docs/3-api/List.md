---
title: List
category: API
order: 127
---

# List

Represents a list.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- List

## Constructors

### List(string id, TrelloAuthorization auth)

Creates a new instance of the [List](List#list) object.

**Parameter:** id

The list&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](TrelloAuthorization#static-trelloauthorization-default--get-) will be used.

## Properties

### static Manatee.Trello.List+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### Manatee.Trello.IReadOnlyCollection`1[[Manatee.Trello.IAction, Manatee.Trello, Version=3.0.0.0, Culture=neutral, PublicKeyToken=f502fcc17fc907d6]] Actions { get; }

Gets the collection of actions performed on the list.

### [IBoard](IBoard#iboard) Board { get; set; }

Gets or sets the board on which the list belongs.

### [ICardCollection](ICardCollection#icardcollection) Cards { get; }

Gets the collection of cards contained in the list.

### DateTime CreationDate { get; }

Gets the creation date of the list.

### string Id { get; }

Gets the list&#39;s ID.

### bool? IsArchived { get; set; }

Gets or sets whether the list is archived.

### bool? IsSubscribed { get; set; }

Gets or sets whether the current member is subscribed to the list.

### [ICard](ICard#icard) this[string key] { get; }

Retrieves a card which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching card, or null if none found.

#### Remarks

Matches on card ID and name. Comparison is case-sensitive.

### [ICard](ICard#icard) this[int index] { get; }

Retrieves the card at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The card.

### string Name { get; set; }

Gets the list&#39;s name.

### [Position](Position#position) Position { get; set; }

Gets the list&#39;s position.

## Events

### Action&lt;IList, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the list is updated.

## Methods

### void ApplyAction(IAction action)

Applies the changes an action represents.

**Parameter:** action

The action.

### Task Refresh(CancellationToken ct)

Refreshes the label data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

