---
title: Search
category: API
order: 245
---

Performs a search.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Search

## Constructors

### Search(ISearchQuery query, int? limit = null, SearchModelType modelTypes = All, IEnumerable&lt;IQueryable&gt; context = null, TrelloAuthorization auth = null, bool isPartial = False)

Creates a new instance of the Manatee.Trello.Search object and performs the search.

**Parameter:** query

The query.

**Parameter:** limit

The maximum number of results to return.

**Parameter:** modelTypes

(Optional) The desired model types to return. Can be joined using the | operator. Default is All.

**Parameter:** context

(Optional) A collection of queryable items to serve as a context in which to search.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided,
Manatee.Trello.TrelloAuthorization.Default will be used.

**Parameter:** isPartial

(Optional) Indicates whether to include matches that &lt;em&gt;start with&lt;/em&gt; the query text. Default is false.

### Search(string query, int? limit = null, SearchModelType modelTypes = All, IEnumerable&lt;IQueryable&gt; context = null, TrelloAuthorization auth = null, bool isPartial = False)

Creates a new instance of the Manatee.Trello.Search object and performs the search.

**Parameter:** query

The query.

**Parameter:** limit

The maximum number of results to return.

**Parameter:** modelTypes

(Optional) The desired model types to return. Can be joined using the | operator. Default is All.

**Parameter:** context

(Optional) A collection of queryable items to serve as a context in which to search.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided,
Manatee.Trello.TrelloAuthorization.Default will be used.

**Parameter:** isPartial

(Optional) Indicates whether to include matches that &lt;em&gt;start with&lt;/em&gt; the query text. Default is false.

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

