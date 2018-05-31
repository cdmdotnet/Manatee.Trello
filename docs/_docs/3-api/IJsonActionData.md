---
title: IJsonActionData
category: API
order: 89
---

Defines the JSON structure for the ActionData object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonActionData

## Properties

### [IJsonAttachment](../IJsonAttachment#ijsonattachment) Attachment { get; set; }

Gets or sets an attachment associated with the action if any.

### [IJsonBoard](../IJsonBoard#ijsonboard) Board { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonBoard](../IJsonBoard#ijsonboard) BoardSource { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonBoard](../IJsonBoard#ijsonboard) BoardTarget { get; set; }

Gets or sets a board associated with the action if any.

### [IJsonCard](../IJsonCard#ijsoncard) Card { get; set; }

Gets or sets a card associated with the action if any.

### [IJsonCard](../IJsonCard#ijsoncard) CardSource { get; set; }

Gets or sets a card associated with the action if any.

### [IJsonCheckItem](../IJsonCheckItem#ijsoncheckitem) CheckItem { get; set; }

Gets or sets a check item associated with the action if any.

### [IJsonCheckList](../IJsonCheckList#ijsonchecklist) CheckList { get; set; }

Gets or sets a check list associated with the action if any.

### [IJsonCustomFieldDefinition](../IJsonCustomFieldDefinition#ijsoncustomfielddefinition) CustomField { get; set; }

Gets or sets a custom field definition associated with the action if any.

### DateTime? DateLastEdited { get; set; }

Gets or sets the last date/time that a comment was edited.

### [IJsonLabel](../IJsonLabel#ijsonlabel) Label { get; set; }

Gets or sets a label associated with the action if any.

### [IJsonList](../IJsonList#ijsonlist) List { get; set; }

Gets or sets a list associated with the action if any.

### [IJsonList](../IJsonList#ijsonlist) ListAfter { get; set; }

Gets or sets a destination list associated with the action if any.

### [IJsonList](../IJsonList#ijsonlist) ListBefore { get; set; }

Gets or sets a source list associated with the action if any.

### [IJsonMember](../IJsonMember#ijsonmember) Member { get; set; }

Gets or sets a member associated with the action if any.

### [IJsonActionOldData](../IJsonActionOldData#ijsonactionolddata) Old { get; set; }

Gets or sets any previous data associated with the action.

### [IJsonOrganization](../IJsonOrganization#ijsonorganization) Org { get; set; }

Gets or sets an organization associated with the action if any.

### [IJsonPowerUp](../IJsonPowerUp#ijsonpowerup) Plugin { get; set; }

Gets or sets plugin data associated with the action if any.

### string Text { get; set; }

Gets or sets text associated with the action if any.

### string Value { get; set; }

Gets or sets a custom value associate with the action if any.

