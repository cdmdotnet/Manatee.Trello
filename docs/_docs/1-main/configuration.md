---
title: Configuration
category:
order: 2
---

Out of the box, there isn't much to configure for Manatee.Trello.  Just set up authorization, and you're ready to go.

If, however, you're one to tinker, this page describes the various ways Manatee.Trello can be configured.

## Authorization

The `TrelloAuthorization` class enables entities to communicate with the Trello API by identifying the application and/or a user.  It exposes two properties:

- `AppKey` identifies the application.  Each user has one.  Trello suggests that you create a dedicated account for your application to serve as a service account.
- `UserToken` identifies a user.  This is optional, however without it, only publicly visible data will be accessible, and then only for reading.  When supplied, you can also get access to any private data, and the token will specify the type of access.

You can read more about authorization in [Trello's documentation](https://developers.trello.com/v1.0/reference#api-key-tokens).

### Multiple authorizations

In most cases, you'll only need to set the default keys via the `TrelloAuthorization.Default` static property.  However, in some cases, you may want to access different entities as different users.

To support this, all of the entity constructors and factory methods take an optional authorization parameter.  Simply create a new `TrelloAuthorization` instance with the alternate keys, and supply it to one of these methods.

Once an entity is created, it will use the same authorization throughout its lifetime.

## General configuration

All other configuration is performed via the `TrelloConfiguration` static class.

### Basic behavior

- `Log` allows you to supply your own logging solution.  The default implementation simply logs to the Debug window.  Implement `ILog` to provide your own solution.
- `ThrowOnTrelloError` indicates whether Manatee.Trello will throw a `TrelloInteractionException` when receiving a bad response from Trello.  The default is true.
- `ChangeSubmissionTime` specifies how long Manatee.Trello will wait for additional changes to an entity before submitting them to Trello.  Setting 0 ms here will result in immediate submission and will disable call aggregation.  The default value is 100 ms.
- `RefreshThrottle` defines a time during which an entity cannot be refreshed twice.  This serves to prevent rapid calls for the same entity.  Setting 0 ms here will result in all `Refresh()` invocations making a call to Trello's API.  The default is 5 seconds.
- `RegisterPowerUp()` registers custom power-up implementations, allowing Manatee.Trello to create instances.  This is important when enumerating `Board.PowerUps`.

### Automatic retries

Manatee.Trello can automatically retry calls on failures, delaying a final error until the final retry fails.  The following settings configure this behavior.  By default, this functionality is disabled.

- `RetryStatusCodes` defines which HTTP status codes warrant a retry.
- `MaxRetryCount` specifies the number of retries will be performed before finally failing.
- `DelayBetweenRetries` indicates a static retry delay to use between each retry.
- `RetryPredicate` allows you to implement custom logic.  If set, the above values will not be used (unless used by your logic).

### Components

In addition to the behavioral settings above, Manatee.Trello defines several seams where you can supply your own implementations of certain components.

- `Serializer`/`Deserializer` allows you to provide your own JSON serializer.  For more information, please see the [JSON serialier page](/1-main/supplying-your-own-JSON-serializer).  The built-in implementation uses [Manatee.Json](https://github.com/gregsdennis/Manatee.Json)
- `RestClientProvider` allows you to provide your own HTTP client.  For more information, please see the [REST provider page](/1-main/supplying-your-own-ReST-client).  The built-in implementation uses ASP.Net's `WebClient`.

> **NOTE** For all you version 2 users, you may recall that these components were supplied in separate Nuget packages.  This is no longer the case, as those packages have been incorporated as of version 3 to serve as default implementations.  This means that you no longer have to set these properties as part of your initial configuration.