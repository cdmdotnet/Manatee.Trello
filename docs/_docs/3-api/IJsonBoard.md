---
title: IJsonBoard
category: API
order: 215
---

# IJsonBoard

Defines the JSON structure for the Board object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoard

## Properties

### List&lt;IJsonAction&gt; Actions { get; set; }

Gets or sets a list of actions.

### [IJsonBoard](IJsonBoard#ijsonboard) BoardSource { get; set; }

Gets or sets a board to be used as a template.

### List&lt;IJsonCard&gt; Cards { get; set; }

Gets or sets a list of cards.

### bool? Closed { get; set; }

Gets or sets whether this board is closed.

### List&lt;IJsonCustomFieldDefinition&gt; CustomFields { get; set; }

Gets or sets a list of custom field definitions.

### DateTime? DateLastActivity { get; set; }

Gets or sets the date the board last had activity.

### DateTime? DateLastView { get; set; }

Gets or sets the date the board was last viewed.

### string Desc { get; set; }

Gets or sets the board&#39;s description.

### List&lt;IJsonLabel&gt; Labels { get; set; }

Gets or sets a list of labels.

### List&lt;IJsonList&gt; Lists { get; set; }

Gets or sets a list of lists.

### List&lt;IJsonMember&gt; Members { get; set; }

Gets or sets a list of members.

### List&lt;IJsonBoardMembership&gt; Memberships { get; set; }

Gets or sets a list of memberships.

### string Name { get; set; }

Gets or sets the board&#39;s name.

### [IJsonOrganization](IJsonOrganization#ijsonorganization) Organization { get; set; }

Gets or sets the ID of the organization, if any, to which this board belongs.

### bool? Pinned { get; set; }

Gets or sets whether the board is pinned.

### List&lt;IJsonPowerUpData&gt; PowerUpData { get; set; }

Gets or sets a list of power-up data.

### List&lt;IJsonPowerUp&gt; PowerUps { get; set; }

Gets or sets a list of power-ups.

### [IJsonBoardPreferences](IJsonBoardPreferences#ijsonboardpreferences) Prefs { get; set; }

Gets or sets a set of preferences for the board.

### string ShortLink { get; set; }

Gets or sets the short link (ID).

### string ShortUrl { get; set; }

Gets or sets the short URL.

### bool? Starred { get; set; }

Gets or sets whether the board is starred on the member&#39;s dashboard.

### bool? Subscribed { get; set; }

Gets or sets whether the user is subscribed to this board.

### string Url { get; set; }

Gets or sets the URL for this board.

