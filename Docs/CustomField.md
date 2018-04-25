# CustomField

Represents a custom field instance.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- CustomField

## Properties

### [ICustomFieldDefinition](ICustomFieldDefinition#icustomfielddefinition) Definition { get; }

Gets the custom field definition.

### string Id { get; }

Gets an ID on which matching can be performed.

## Events

### Action&lt;ICustomField, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the custom field is updated.

## Methods

### Task Refresh(CancellationToken ct)

Refreshes the custom field instance data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

