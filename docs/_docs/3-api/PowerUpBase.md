---
title: PowerUpBase
category: API
order: 210
---

Provides a base implementation for Trello Power-Ups.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- PowerUpBase

## Properties

### string AdditionalInfo { get; }

Gets a URI that provides JSON-formatted data about the plugin.

### string Id { get; }

Gets the power-up&#39;s ID.

### bool? IsPublic { get; }

Gets or sets whether this power-up is closed.

### string Name { get; }

Gets the name of the power-up.

## Methods

### Task Refresh(CancellationToken ct = default(CancellationToken))

Refreshes the power-up data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns the Manatee.Trello.PowerUpBase.Name

**Returns:** A string that represents the current object.

