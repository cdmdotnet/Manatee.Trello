---
title: CustomField
category: API
order: 39
---

Represents a custom field instance.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- CustomField

## Properties

### [ICustomFieldDefinition](../ICustomFieldDefinition#icustomfielddefinition) Definition { get; }

Gets the custom field definition.

### string Id { get; }

Gets an ID on which matching can be performed.

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

