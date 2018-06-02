---
title: ISearch
category: API
order: 172
---

Performs a search.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ISearch

## Properties

### IEnumerable&lt;[IAction](../IAction#iaction)&gt; Actions { get; }

Gets the collection of actions returned by the search.

### IEnumerable&lt;[IBoard](../IBoard#iboard)&gt; Boards { get; }

Gets the collection of boards returned by the search.

### IEnumerable&lt;[ICard](../ICard#icard)&gt; Cards { get; }

Gets the collection of cards returned by the search.

### IEnumerable&lt;[IMember](../IMember#imember)&gt; Members { get; }

Gets the collection of members returned by the search.

### IEnumerable&lt;[IOrganization](../IOrganization#iorganization)&gt; Organizations { get; }

Gets the collection of organizations returned by the search.

### string Query { get; }

Gets the query.

## Methods

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the search results.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

