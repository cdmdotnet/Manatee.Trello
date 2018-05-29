# ICustomField

Provides a base for [ICustomField`1](ICustomField`1#icustomfield1).

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ICustomField

## Properties

### [ICustomFieldDefinition](ICustomFieldDefinition#icustomfielddefinition) Definition { get; }

Gets the custom field definition.

## Events

### Action&lt;ICustomField, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the custom field is updated.

## Methods

### Task Refresh(CancellationToken ct)

Refreshes the custom field instance data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

