---
title: IStarredBoard
category: API
order: 175
---

Represents a member&#39;s board star.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IStarredBoard

## Properties

### [IBoard](../IBoard#iboard) Board { get; }

Gets the board that is starred.

### [Position](../Position#position) Position { get; set; }

Gets or sets the position in the member&#39;s starred boards list.

## Events

### Action&lt;[IStarredBoard](../IStarredBoard#istarredboard), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the star is updated.

## Methods

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the star.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the star from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the star data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

