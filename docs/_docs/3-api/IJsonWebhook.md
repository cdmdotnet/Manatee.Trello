---
title: IJsonWebhook
category: API
order: 249
---

# IJsonWebhook

Defines the JSON structure for the Webhook object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonWebhook

## Properties

### bool? Active { get; set; }

Gets or sets whether the webhook is active.

### string CallbackUrl { get; set; }

Gets or sets the URL which receives notification messages.

### string Description { get; set; }

Gets or sets the description of the webhook.

### string IdModel { get; set; }

Gets or sets the ID of the entity which the webhook monitors.

