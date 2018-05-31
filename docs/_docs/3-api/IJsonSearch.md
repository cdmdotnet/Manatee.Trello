---
title: IJsonSearch
category: API
order: 124
---

Defines the JSON structure for the Search object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonSearch

## Properties

### List&lt;[IJsonAction](../IJsonAction#ijsonaction)&gt; Actions { get; set; }

Lists the IDs of actions which match the query.

### List&lt;[IJsonBoard](../IJsonBoard#ijsonboard)&gt; Boards { get; set; }

Lists the IDs of boards which match the query.

### List&lt;[IJsonCard](../IJsonCard#ijsoncard)&gt; Cards { get; set; }

Lists the IDs of cards which match the query.

### List&lt;[IJsonCacheable](../IJsonCacheable#ijsoncacheable)&gt; Context { get; set; }

Gets or sets a collection of boards, cards, and organizations within which the search should run.

### int? Limit { get; set; }

Gets or sets how many results to return;

### List&lt;[IJsonMember](../IJsonMember#ijsonmember)&gt; Members { get; set; }

Lists the IDs of members which match the query.

### List&lt;[IJsonOrganization](../IJsonOrganization#ijsonorganization)&gt; Organizations { get; set; }

Lists the IDs of organizations which match the query.

### bool Partial { get; set; }

Gets or sets whether the search should match on partial words.

### string Query { get; set; }

Gets or sets the search query.

### [SearchModelType](../SearchModelType#searchmodeltype)? Types { get; set; }

Gets or sets which types of objects should be returned.

