---
title: CommentCollection
category: API
order: 37
---

A collection of Manatee.Trello.Actions of types Manatee.Trello.ActionType.CommentCard and Manatee.Trello.ActionType.CopyCommentCard.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IAction&gt;
- ReadOnlyActionCollection
- CommentCollection

## Methods

### Task&lt;[IAction](../IAction#iaction)&gt; Add(string text, CancellationToken ct = default(CancellationToken))

Posts a new comment to a card.

**Parameter:** text

The content of the comment.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The Manatee.Trello.IAction associated with the comment.

