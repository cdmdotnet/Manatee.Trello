# IPowerUpData

Represents the data associated with a power-up.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IPowerUpData

## Properties

### string PluginId { get; }

Gets the ID for the plugin with which this data is associated.

### string Value { get; }

Gets the data as a string. This data will be JSON-encoded.

## Methods

### Task Refresh(CancellationToken ct)

Refreshes the power-up data... data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

