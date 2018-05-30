---
title: PowerUpData
category: API
order: 212
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

### Task Refresh(CancellationToken ct)

Refreshes the power-up data... data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

