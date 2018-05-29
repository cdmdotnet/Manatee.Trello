---
title: IJsonMemberSearch
category: API
order: 109
---

Defines the JSON structure for a member search.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonMemberSearch

## Properties

### [IJsonBoard](../IJsonBoard#ijsonboard) Board { get; set; }

Gets or sets a board within which the search should run.

### int? Limit { get; set; }

Gets or sets the number of results to return.

### List&lt;IJsonMember&gt; Members { get; set; }

Gets or sets a list of members.

### bool? OnlyOrgMembers { get; set; }

Gets or sets whether only organization members should be returned.

### [IJsonOrganization](../IJsonOrganization#ijsonorganization) Organization { get; set; }

Gets or sets an organization within which the search should run.

### string Query { get; set; }

Gets or sets the search query.

