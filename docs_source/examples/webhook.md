# Creating a webhook

```csharp
var card = factory.Card("[some known ID]");
// ITrelloFactory.Webhook<T>() is the other awaitable factory method.  This is
// because a call is made to create a new webhook.  The webhook data is downloaded,
// so no refresh call is required.
var webhook = await factory.Webhook(card, "http://post.back/url");
```

## Processing a webhook notification

This code would be placed inside a ASP.Net controller's POST method.  The JSON content would need to be read as a string and passed to `TrelloProcessor.ProcessNotification()` as shown below.

```csharp
var content = await Request.ReadAsStringAsync();
TrelloProcessor.ProcessNotification(content);
```

The processor will update the entity *if it exists in the cache*.  If the entity does not exist in the cache, no processing will occur.