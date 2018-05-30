---
title: Search
category: API
order: 237
---

Performs a search.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Search

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

### Task Refresh(CancellationToken ct)

Refreshes the search results.

**Parameter:** ct

(Optional) A cancellation token for async processing.

