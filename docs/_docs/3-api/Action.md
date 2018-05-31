---
title: Action
category: API
order: 1
---

Represents an action performed on Trello objects.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Action

## Constructors

### Action(string id, TrelloAuthorization auth = null)

Creates a new Manatee.Trello.Action instance.

**Parameter:** id

The action&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, Manatee.Trello.TrelloAuthorization.Default will be used.

## Properties

### static Action.Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### DateTime CreationDate { get; }

Gets the creation date of the action.

### [IMember](../IMember#imember) Creator { get; }

Gets the member who performed the action.

### [IActionData](../IActionData#iactiondata) Data { get; }

Gets any data associated with the action.

### DateTime? Date { get; }

Gets the date and time at which the action was performed.

### string Id { get; }

Gets an ID on which matching can be performed.

### [ActionType](../ActionType#actiontype)? Type { get; }

Gets the type of action.

## Events

### Action&lt;[IAction](../IAction#iaction), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the action is updated.

## Methods

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the action.

**Parameter:** ct

#### Remarks

This permanently deletes the action from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the action data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

