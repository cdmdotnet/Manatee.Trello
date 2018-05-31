---
title: ICommentCollection
category: API
order: 76
---

A collection of comment actions.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ICommentCollection

## Methods

### Task&lt;[IAction](../IAction#iaction)&gt; Add(string text, CancellationToken ct = default(CancellationToken))

Posts a new comment to a card.

**Parameter:** text

The content of the comment.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The Manatee.Trello.IAction associated with the comment.

