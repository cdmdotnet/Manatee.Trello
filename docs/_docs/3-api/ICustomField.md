---
title: ICustomField
category: API
order: 77
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

### Task Refresh(CancellationToken ct = default(CancellationToken))

Refreshes the custom field instance data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

