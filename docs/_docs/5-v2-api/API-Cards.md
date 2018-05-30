---
title: Cards
category: Version 2.x - API
order: 4
---

> **NOTICE** In migrating to this new documentation, many (if not all) of the links are broken.  Please use the sidebar on the left for navigation.

# Card

Represents a card.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Card

## Constructors

### Card(string id, TrelloAuthorization auth)

Creates a new instance of the [Card](/API-Cards#card) object.

**Parameter:** id

The card&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](/API-Configuration#static-trelloauthorization-default--get-) will be used.

#### Remarks

The supplied ID can be either the full or short ID.

## Properties

### static Manatee.Trello.Card+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [ReadOnlyActionCollection](/API-Actions#readonlyactioncollection) Actions { get; }

Gets the collection of actions performed on this card.

#### Remarks

By default imposed by Trello, this contains actions of type [ActionType.CommentCard](/API-Actions#static-actiontype-commentcard).

### [AttachmentCollection](/API-Attachments#attachmentcollection) Attachments { get; }

Gets the collection of attachments contained in the card.

### [Badges](/API-Cards#badges) Badges { get; }

Gets the badges summarizing the card&#39;s content.

### [Board](/API-Boards#board) Board { get; }

Gets the board to which the card belongs.

### [CheckListCollection](/API-Cards#checklistcollection) CheckLists { get; }

Gets the collection of checklists contained in the card.

### [CommentCollection](/API-Actions#commentcollection) Comments { get; }

Gets the collection of comments made on the card.

### DateTime CreationDate { get; }

Gets the creation date of the card.

### string Description { get; set; }

Gets or sets the card&#39;s description.

### DateTime? DueDate { get; set; }

Gets or sets the card&#39;s due date.

### string Id { get; }

Gets the card&#39;s ID.

### bool? IsArchived { get; set; }

Gets or sets whether the card is archived.

### bool? IsComplete { get; set; }

Gets or sets whether the card is complete. Associated with [DueDate](/API-Cards#datetime-duedate--get-set-).

### bool? IsSubscribed { get; set; }

Gets or sets whether the current member is subscribed to the card.

### [CheckList](/API-Cards#checklist) this[string key] { get; }

Retrieves a check list which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list, or null if none found.

#### Remarks

Matches on [Id](/API-Cards#string-id--get-) and [Name](/API-Cards#string-name--get-set-). Comparison is case-sensitive.

### [CheckList](/API-Cards#checklist) this[int index] { get; }

Retrieves the check list at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The check list.

### [CardLabelCollection](/API-Labels#cardlabelcollection) Labels { get; }

Gets the collection of labels on the card.

### DateTime? LastActivity { get; }

Gets the most recent date of activity on the card.

### [List](/API-Lists#list) List { get; set; }

Gets or sets the list to the card belongs.

### [MemberCollection](/API-Members#membercollection) Members { get; }

Gets the collection of members who are assigned to the card.

### string Name { get; set; }

Gets or sets the card&#39;s name.

### [Position](/API-Common-Types#position) Position { get; set; }

Gets or sets the card&#39;s position.

### [ReadOnlyPowerUpDataCollection](/API-Power-Ups#readonlypowerupdatacollection) PowerUpData { get; }

Gets card-specific power-up data.

### int? ShortId { get; }

Gets the card&#39;s short ID.

### string ShortUrl { get; }

Gets the card&#39;s short URL.

#### Remarks

Because this value does not change, it can be used as a permalink.

### [CardStickerCollection](/API-Stickers#cardstickercollection) Stickers { get; }

Gets the collection of stickers which appear on the card.

### string Url { get; }

Gets the card&#39;s full URL.

#### Remarks

Trello will likely change this value as the name changes. You can use [ShortUrl](/API-Cards#string-shorturl--get-) for permalinks.

### [ReadOnlyMemberCollection](/API-Members#readonlymembercollection) VotingMembers { get; }

Gets all members who have voted for this card.

## Events

### Action&lt;Card, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the card is updated.

## Methods

### void ApplyAction(Action action)

Applies the changes an action represents.

**Parameter:** action

The action.

### void Delete()

Permanently deletes the card from Trello.

#### Remarks

This instance will remain in memory and all properties will remain accessible.

### void Refresh()

Marks the card to be refreshed the next time data is accessed.

### string ToString()

Returns the [Name](/API-Cards#string-name--get-set-), or [ShortId](/API-Cards#int-shortid--get-) if the card has been deleted.

**Returns:** A string that represents the current object.

# ReadOnlyCardCollection

A read-only collection of cards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Card&gt;
- ReadOnlyCardCollection

## Properties

### [Card](/API-Cards#card) this[string key] { get; }

Retrieves a card which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching card, or null if none found.

#### Remarks

Matches on [Id](/API-Cards#string-id--get-) and [Name](/API-Cards#string-name--get-set-). Comparison is case-sensitive.

# CardCollection

A collection of cards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Card&gt;
- ReadOnlyCardCollection
- CardCollection

## Methods

### [Card](/API-Cards#card) Add(Card source)

Creates a new card by copying a card.

**Parameter:** source

A card to copy. Default is null.

**Returns:** The [Card](/API-Cards#card) generated by Trello.

### [Card](/API-Cards#card) Add(string name, string sourceUrl)

Creates a new card by importing data from a URL.

**Parameter:** name

The name of the card to add.

**Parameter:** sourceUrl



**Returns:** 

# Badges

Represents a collection of badges which summarize the contents of a card.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Badges

## Properties

### int? Attachments { get; }

Gets the number of attachments on this card.

### int? CheckItems { get; }

Gets the number of check items on this card.

### int? CheckItemsChecked { get; }

Gets the number of check items on this card which are checked.

### int? Comments { get; }

Gets the number of comments on this card.

### DateTime? DueDate { get; }

Gets the due date for this card.

### string FogBugz { get; }

Gets some FogBugz information.

### bool? HasDescription { get; }

Gets whether this card has a description.

### bool? HasVoted { get; }

Gets whether the current member has voted for this card.

### bool? IsComplete { get; }

Gets wheterh this card has been marked complete.

### bool? IsSubscribed { get; }

Gets whether the current member is subscribed to this card.

### int? Votes { get; }

Gets the number of votes for this card.

# CheckList

Represents a checklist.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- CheckList

## Constructors

### CheckList(string id, TrelloAuthorization auth)

Creates a new instance of the [CheckList](/API-Cards#checklist) object.

**Parameter:** id

The check list&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](/API-Configuration#static-trelloauthorization-default--get-) will be used.

## Properties

### static Manatee.Trello.CheckList+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [Board](/API-Boards#board) Board { get; }

Gets the board on which the checklist belongs.

### [Card](/API-Cards#card) Card { get; set; }

Gets or sets the card on which the checklist belongs.

### [CheckItemCollection](/API-Cards#checkitemcollection) CheckItems { get; }

Gets the collection of items in the checklist.

### DateTime CreationDate { get; }

Gets the creation date of the checklist.

### string Id { get; }

Gets the checklist&#39;s ID.

### [CheckItem](/API-Cards#checkitem) this[string key] { get; }

Retrieves a check list item which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list item, or null if none found.

#### Remarks

Matches on [Id](/API-Cards#string-id--get-) and [Name](/API-Cards#string-name--get-set-). Comparison is case-sensitive.

### [CheckItem](/API-Cards#checkitem) this[int index] { get; }

Retrieves the check list item at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The check list item.

### string Name { get; set; }

Gets the checklist&#39;s name.

### [Position](/API-Common-Types#position) Position { get; set; }

Gets the checklist&#39;s position.

## Events

### Action&lt;CheckList, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the check list is updated.

## Methods

### void Delete()

Permanently deletes the check list from Trello.

#### Remarks

This instance will remain in memory and all properties will remain accessible.

### void Refresh()

Marks the check list to be refreshed the next time data is accessed.

### string ToString()

Returns the [Name](/API-Cards#string-name--get-set-).

**Returns:** A string that represents the current object.

# CheckListCollection

A collection of checklists.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;CheckList&gt;
- ReadOnlyCheckListCollection
- CheckListCollection

## Methods

### [CheckList](/API-Cards#checklist) Add(string name, CheckList source)

Creates a new checklist, optionally by copying a checklist.

**Parameter:** name

The name of the checklist to add.

**Parameter:** source

A checklist to use as a template.

**Returns:** The [CheckList](/API-Cards#checklist) generated by Trello.

# CheckItem

Represents a checklist item.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- CheckItem

## Properties

### static Manatee.Trello.CheckItem+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [CheckList](/API-Cards#checklist) CheckList { get; set; }

Gets or sets the checklist to which the item belongs.

### DateTime CreationDate { get; }

Gets the creation date of the checklist item.

### string Id { get; }

Gets or sets the checklist item&#39;s ID.

### string Name { get; set; }

Gets or sets the checklist item&#39;s name.

### [Position](/API-Common-Types#position) Position { get; set; }

Gets or sets the checklist item&#39;s position.

### [CheckItemState](/API-CheckLists#checkitemstate)? State { get; set; }

Gets or sets the checklist item&#39;s state.

## Events

### Action&lt;CheckItem, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the checklist item is updated.

## Methods

### void Delete()

Deletes the checklist item.

#### Remarks

This permanently deletes the checklist item from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### void Refresh()

Marks the checklist item to be refreshed the next time data is accessed.

### string ToString()

Returns the [Name](/API-Cards#string-name--get-set-).

**Returns:** A string that represents the current object.

# CheckItemCollection

A collection of checklist items.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;CheckItem&gt;
- ReadOnlyCheckItemCollection
- CheckItemCollection

## Methods

### [CheckItem](/API-Cards#checkitem) Add(string name)

Creates a new checklist item.

**Parameter:** name

The name of the checklist item to add.

**Returns:** The [CheckItem](/API-Cards#checkitem) generated by Trello.

# ReadOnlyMemberCollection

A read-only collection of members.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Member&gt;
- ReadOnlyMemberCollection

## Properties

### [Member](/API-Members#member) this[string key] { get; }

Retrieves a member which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching member, or null if none found.

#### Remarks

Matches on [Id](/API-Members#string-id--get-), [FullName](/API-Members#string-fullname--get-), and [UserName](/API-Members#string-username--get-). Comparison is case-sensitive.

# MemberCollection

A collection of members.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Member&gt;
- ReadOnlyMemberCollection
- MemberCollection

## Methods

### void Add(Member member)

Adds a member to the collection.

**Parameter:** member

The member to add.

### void Remove(Member member)

Removes a member from the collection.

**Parameter:** member

The member to remove.

