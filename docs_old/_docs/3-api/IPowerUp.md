---
title: IPowerUp
category: API
order: 150
---

Defines the basis of a power-up.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IPowerUp

## Properties

### bool? IsPublic { get; }

Gets whether the power-up is public. (Really, I don&#39;t know what this is, and Trello&#39;s not talking.)

### string Name { get; }

Gets the power-up name.

## Methods

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the power-up data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

