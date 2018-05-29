# Webhook&lt;T&gt;

Represents a webhook.

**Type Parameter:** T : [ICanWebhook](ICanWebhook#icanwebhook)

The type of object to which the webhook is attached.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Webhook&lt;T&gt;

## Constructors

### Webhook&lt;T&gt;(string id, TrelloAuthorization auth)

Creates a new instance of the [Webhook`1](Webhook`1#webhook1) object for a webhook which has already been registered with Trello.

**Parameter:** id

The id.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](TrelloAuthorization#static-trelloauthorization-default--get-) will be used.

## Properties

### string CallBackUrl { get; set; }

Gets or sets a callback URL for the webhook.

### DateTime CreationDate { get; }

Gets the creation date of the webhook.

### string Description { get; set; }

Gets or sets a description for the webhook.

### string Id { get; }

Gets the webhook&#39;s ID.

### bool? IsActive { get; set; }

Gets or sets whether the webhook is active.

### T Target { get; set; }

Gets or sets the webhook&#39;s target.

## Events

### Action`2 Updated

Raised when data on the webhook is updated.

## Methods

### Task Delete(CancellationToken ct)

Deletes the webhook.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the webhook from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct)

Refreshes the webhook data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

