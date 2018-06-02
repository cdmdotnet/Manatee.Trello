---
title: ICustomField
category: API
order: 79
---

Provides a base for Manatee.Trello.ICustomField`1.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ICustomField

## Properties

### [ICustomFieldDefinition](../ICustomFieldDefinition#icustomfielddefinition) Definition { get; }

Gets the custom field definition.

## Events

### Action&lt;[ICustomField](../ICustomField#icustomfield), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the custom field is updated.

## Methods

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the custom field instance data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

