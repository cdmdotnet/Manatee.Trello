---
title: IJsonSearch
category: API
order: 120
---

Defines the JSON structure for the Search object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonSearch

## Properties

### List&lt;IJsonAction&gt; Actions { get; set; }

Lists the IDs of actions which match the query.

### List&lt;IJsonBoard&gt; Boards { get; set; }

Lists the IDs of boards which match the query.

### List&lt;IJsonCard&gt; Cards { get; set; }

Lists the IDs of cards which match the query.

### List&lt;IJsonCacheable&gt; Context { get; set; }

Gets or sets a collection of boards, cards, and organizations within which the search should run.

### int? Limit { get; set; }

Gets or sets how many results to return;

### List&lt;IJsonMember&gt; Members { get; set; }

Lists the IDs of members which match the query.

### List&lt;IJsonOrganization&gt; Organizations { get; set; }

Lists the IDs of organizations which match the query.

### bool Partial { get; set; }

Gets or sets whether the search should match on partial words.

### string Query { get; set; }

Gets or sets the search query.

### [SearchModelType](../SearchModelType#searchmodeltype)? Types { get; set; }

Gets or sets which types of objects should be returned.

