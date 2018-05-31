---
title: MemberSearch
category: API
order: 191
---

Performs a search for members.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- MemberSearch

## Constructors

### MemberSearch(string query, int? limit = null, IBoard board = null, IOrganization organization = null, bool? restrictToOrganization = null, TrelloAuthorization auth = null)

Creates a new instance of the Manatee.Trello.MemberSearch object and performs the search.

**Parameter:** query

The query.

**Parameter:** limit

(Optional) The result limit. Can be a value from 1 to 20. The default is 8.

**Parameter:** board

(Optional) A board to which the search should be limited.

**Parameter:** organization

(Optional) An organization to which the search should be limited.

**Parameter:** restrictToOrganization

(Optional) Restricts the search to only organization members.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided,
Manatee.Trello.TrelloAuthorization.Default will be used.

## Properties

### IEnumerable&lt;[MemberSearchResult](../MemberSearchResult#membersearchresult)&gt; Results { get; }

Gets the collection of results returned by the search.

## Methods

### Task Refresh(CancellationToken ct = default(CancellationToken))

Refreshes the search results.

**Parameter:** ct

(Optional) A cancellation token for async processing.

