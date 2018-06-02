---
title: IJsonAction
category: API
order: 88
---

Defines the JSON structure for the Action object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonAction

## Properties

### [IJsonActionData](../IJsonActionData#ijsonactiondata) Data { get; set; }

Gets or sets the data associated with the action. Contents depend upon the action&#39;s type.

### DateTime? Date { get; set; }

Gets or sets the date on which the action was performed.

### [IJsonMember](../IJsonMember#ijsonmember) MemberCreator { get; set; }

Gets or sets the ID of the member who performed the action.

### string Text { get; set; }

Gets or sets the text for a comment while updating it.

### [ActionType](../ActionType#actiontype)? Type { get; set; }

Gets or sets the action&#39;s type.

