---
title: IWebhook&lt;T&gt;
category: API
order: 181
---

Represents a webhook.

**Type Parameter:** T : [ICanWebhook](../ICanWebhook#icanwebhook)

The type of object to which the webhook is attached.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IWebhook&lt;T&gt;

## Properties

### string CallBackUrl { get; set; }

Gets or sets a callback URL for the webhook.

### DateTime CreationDate { get; }

Gets the creation date of the webhook.

### string Description { get; set; }

Gets or sets a description for the webhook.

### bool? IsActive { get; set; }

Gets or sets whether the webhook is active.

### T Target { get; set; }

Gets or sets the webhook&#39;s target.

## Events

### Action&lt;[IWebhook`1](../IWebhook`1#iwebhook1), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the webhook is updated.

## Methods

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the webhook.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the webhook from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the webhook data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

