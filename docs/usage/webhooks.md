# Webhooks

As an alternative to refreshing entities manually, you have the option of using webhooks to listen for notifications from Trello.  Webhooks are basically subcriptions to entities.  When an entity is altered outside of Manatee.Trello, an HTML POST message will be sent by Trello to a URI that the developer specifies which contains an Action describing what occurred.

Manatee.Trello can interpret these Actions and apply any changes to affected entities.  It should be noted that this does not disable manually refreshing entities.

# Creating webhooks

You can create webhooks via either the `Webhook<T>` constructor or a method on the entity factory which takes an object implementing the `ICanWebhook` interface.  Like the other entities, you can also download the information for a webhook that already exists by passing the webhook's ID.

```csharp
var member = factory.Member("myusername");
var webhook = factory.Webook<Member>(member, "http://myurl.com/inbox/");
```

The URI which is passed in must be set up to receive POST messages.  Within the controller, extract the message body as a string, and pass it to the `TrelloProcessor.ProcessNotification()` static method.  Manatee.Trello will find the entity (if it's been downloaded already) and update it.

> **NOTE** In the example above, the member is cached upon creation.  If a message is received for an entity which has not been downloaded, there is no change.

> **IMPORTANT** When you create a webhook, Trello will make a HEAD request to callbackURL you provide to verify that it is a valid URL. Failing to respond to the HEAD request will result in the webhook failing to be created.

# Working with webhooks

There are several properties on the `Webhook` entity that you can use to modify its behavior.

- `CallbackUrl` - Changes the callback URL to which Trello will send notification messages.
- `Description` - Changes an informational description of the webhook.
- `Entity` - Changes the entity to which the webhook listens.  It should be noted that since the `Webhook<T>` class is generic, it will be strongly typed to the same kind of entity for which it was created.  This is a requirement imposed by Manatee.Trello, not Trello's API.
- `IsActive` - Exposes the ability to temporarily disable notifications for the webhook.  When disabled, Trello will not send notifications.

Webhooks also expose a `Delete()` method which will permanently delete the webhook from Trello.
