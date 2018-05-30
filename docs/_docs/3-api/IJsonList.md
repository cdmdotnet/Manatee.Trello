---
title: IJsonList
category: API
order: 106
---

Defines the JSON structure for the List object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonList

## Properties

### List&lt;[IJsonAction](../IJsonAction#ijsonaction)&gt; Actions { get; set; }

Gets or sets a collection of actions.

### [IJsonBoard](../IJsonBoard#ijsonboard) Board { get; set; }

Gets or sets the ID of the board which contains the list.

### List&lt;[IJsonCard](../IJsonCard#ijsoncard)&gt; Cards { get; set; }

Gets or sets a collection of cards.

### bool? Closed { get; set; }

Gets or sets whether the list is archived.

### string Name { get; set; }

Gets or sets the name of the list.

### [IJsonPosition](../IJsonPosition#ijsonposition) Pos { get; set; }

Gets or sets the position of the list.

### bool? Subscribed { get; set; }

Gets or sets whether the current member is subscribed to the list.

