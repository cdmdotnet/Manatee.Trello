---
title: IJsonCheckItem
category: API
order: 223
---

# IJsonCheckItem

Defines the JSON structure for the CheckItem object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCheckItem

## Properties

### [IJsonCheckList](IJsonCheckList#ijsonchecklist) CheckList { get; set; }

Gets or sets the check list for the check item.

### string Name { get; set; }

Gets or sets the name of the checklist item.

### [IJsonPosition](IJsonPosition#ijsonposition) Pos { get; set; }

Gets or sets the position of the checklist item.

### [CheckItemState](CheckItemState#checkitemstate)? State { get; set; }

Gets or sets the check state of the checklist item.

