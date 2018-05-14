# Webhook

Provides a common base class for the generic Webhook classes.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Webhook

## Methods

### static void ProcessNotification(string content, TrelloAuthorization auth)

Processes webhook notification content.

**Parameter:** content

The string content of the notification.

**Parameter:** auth

The [TrelloAuthorization](API-Configuration#trelloauthorization) under which the notification should be processed

# Webhook&lt;T&gt;

Represents a webhook.

**Type Parameter:** T : Manatee.Trello.Contracts.ICanWebhook

The type of object to which the webhook is attached.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Webhook
- Webhook&lt;T&gt;

## Constructors

### Webhook&lt;T&gt;(string id, TrelloAuthorization auth)

Creates a new instance of the [Webhook`1](API-Webhooks#webhook1) object for a webhook which has already been registered with Trello.

**Parameter:** id

The id.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](API-Configuration#static-trelloauthorization-default--get-) will be used.

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

### Action&lt;Webhook, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the webhook is updated.

## Methods

### void Delete()

Permanently deletes the webhook from Trello.

#### Remarks

This instance will remain in memory and all properties will remain accessible.

### void Refresh()

Marks the webhook to be refreshed the next time data is accessed.

