---
title: IBoardPersonalPreferences
category: API
order: 65
---

Represents the user-specific preferences for a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IBoardPersonalPreferences

## Properties

### [IList](../IList#ilist) EmailList { get; set; }

Gets or sets the Manatee.Trello.List which will be used to post new cards submitted by email.

### [Position](../Position#position) EmailPosition { get; set; }

Gets or sets the Manatee.Trello.Position within a Manatee.Trello.List which will be used to post new cards submitted by email.

### bool? ShowListGuide { get; set; }

Gets or sets whether to show the list guide.

#### Remarks

It appears that this may be deprecated by Trello.

### bool? ShowSidebar { get; set; }

Gets or sets whether to show the side bar.

### bool? ShowSidebarActivity { get; set; }

Gets or sets whether to show the activity list in the side bar.

#### Remarks

It appears that this may be deprecated by Trello.

### bool? ShowSidebarBoardActions { get; set; }

Gets or sets whether to show the board action list in the side bar.

#### Remarks

It appears that this may be deprecated by Trello.

### bool? ShowSidebarMembers { get; set; }

Gets or sets whether to show the board members in the side bar.

#### Remarks

It appears that this may be deprecated by Trello.

