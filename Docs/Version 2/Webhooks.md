As an alternative to simply letting entities expire and refresh when they are referenced, the developer has the option of using webhooks to listen for notifications from Trello.com.  Webhooks are subscriptions to changes for specific entities.  When an entity is altered outside of Manatee.Trello, an HTML POST message will be sent by Trello to a URI that the developer specifies which contains an Action describing what occurred.

Manatee.Trello can interpret these Actions and apply any changes to affected entities.  It should be noted that this does not automatically disable the default update mechanism.

# Creating webhooks

Registering webhooks has been made simple through one of the `Webhook<T>` constructors which takes an object implementing the `ICanWebhook` interface.  Like the constructors for the other objects, the second constructor on the `Webhook<T>` class takes an ID and downloads the data for a webhook that has already been created.

    var member = new Member("myusername");
    var webhook = new Webook<Member>(member, "http://myurl.com/inbox/");

The URI which is passed in must be set up to receive POST messages.  Beyond that, the only thing to do is take the messasge body as a string and pass it on to the `Webhook` class' `ProcessNotification()` static method.  Manatee.Trello will do the rest.

> **NOTE:** In the example above, the member is cached upon creation.  If a message is received for an entity which has not been downloaded, there is no change.

> **NOTE:** Only entities are updated automatically.  Collections (e.g. the Cards property on the List entity) are not affected.

# Working with webhooks

There are several properties on the webhook object which can be edited in order to modify the behavior of webhooks.

- CallbackUrl - Changes the callback URL to which Trello will send notification messages.
- Description - Changes an informational description of the webhook.
- Entity - Changes the entity to which the webhook listens.  It should be noted that since the `Webhook<T>` class is generic, it will be strongly typed to the same kind of entity for which it was created.  This is a requirement imposed by Manatee.Trello, not Trello's API.
- IsActive - Exposes the ability to temporarily disable notifications for the webhook.  When disabled, Trello will not send notifications.

Webhooks also expose a `Delete()` method.  This will permanently delete the webhook from Trello.
