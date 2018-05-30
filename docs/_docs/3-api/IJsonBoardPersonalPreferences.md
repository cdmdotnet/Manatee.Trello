---
title: IJsonBoardPersonalPreferences
category: API
order: 93
---

Defines the JSON structure for the BoardPersonalPreferences object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardPersonalPreferences

## Properties

### [IJsonList](../IJsonList#ijsonlist) EmailList { get; set; }

Gets or sets the list for new cards when they are added via email.

### [IJsonPosition](../IJsonPosition#ijsonposition) EmailPosition { get; set; }

Gets or sets the position of new cards when they are added via email.

### bool? ShowListGuide { get; set; }

Gets or sets whether the list guide (left side of the screen) is expanded.

### bool? ShowSidebar { get; set; }

Gets or sets whether the side bar (right side of the screen) is shown

### bool? ShowSidebarActivity { get; set; }

Gets or sets whether the activity section of the side bar is shown.

### bool? ShowSidebarBoardActions { get; set; }

Gets or sets whether the board actions (Add List/Add Member/Filter Cards) section of the side bar is shown.

### bool? ShowSidebarMembers { get; set; }

Gets or sets whether the members section of the list of the side bar is shown.

