---
title: PowerUpData
category: API
order: 219
---

Represents the data associated with a plugin.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- PowerUpData

## Properties

### string Id { get; }

Gets the ID associated with this particular data instance.

### string PluginId { get; }

Gets the ID for the power-up with which this data is associated.

### string Value { get; }

Gets the data as a string. This data will be JSON-encoded.

## Methods

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the power-up data... data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

