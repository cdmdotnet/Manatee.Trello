---
title: CommentCollection
category: API
order: 38
---

# CommentCollection

A collection of [Action](Action#action)s of types [ActionType.CommentCard](ActionType#static-actiontype-commentcard) and [ActionType.CopyCommentCard](ActionType#static-actiontype-copycommentcard).

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IAction&gt;
- ReadOnlyActionCollection
- CommentCollection

## Methods

### Task&lt;IAction&gt; Add(string text, CancellationToken ct)

Posts a new comment to a card.

**Parameter:** text

The content of the comment.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The [IAction](IAction#iaction) associated with the comment.

