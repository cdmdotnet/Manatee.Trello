# IJsonCheckList

Defines the JSON structure for the CheckList object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCheckList

## Properties

### [IJsonBoard](IJsonBoard#ijsonboard) Board { get; set; }

Gets or sets the ID of the board which contains this checklist.

### [IJsonCard](IJsonCard#ijsoncard) Card { get; set; }

Gets or sets the ID of the card which contains this checklist.

### List&lt;IJsonCheckItem&gt; CheckItems { get; set; }

Gets or sets the collection of items in this checklist.

### [IJsonCheckList](IJsonCheckList#ijsonchecklist) CheckListSource { get; set; }

Gets or sets a checklist to copy during creation.

### string Name { get; set; }

Gets or sets the name of this checklist.

### [IJsonPosition](IJsonPosition#ijsonposition) Pos { get; set; }

Gets or sets the position of this checklist.

