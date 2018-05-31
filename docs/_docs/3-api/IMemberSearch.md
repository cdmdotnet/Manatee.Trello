---
title: IMemberSearch
category: API
order: 140
---

Performs a search for members.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IMemberSearch

## Properties

### IEnumerable&lt;[MemberSearchResult](../MemberSearchResult#membersearchresult)&gt; Results { get; }

Gets the collection of results returned by the search.

## Methods

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the search results.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

