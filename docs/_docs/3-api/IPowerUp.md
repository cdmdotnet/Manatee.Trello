---
title: IPowerUp
category: API
order: 145
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

### Task Refresh(CancellationToken ct = default(CancellationToken))

Refreshes the power-up data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

