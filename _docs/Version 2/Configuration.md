There are several ways that one can configure Manatee.Trello.  This configuration is exposed via the `TrelloConfiguration` static class.  These options include:

- `Log` - This allows Manatee.Trello to log communications and exceptions.  More information can be found on the [Logging](/gregsdennis/manatee.trello/wiki/Logging) page.
- `Cache` - Every object caches itself on creation.  Manatee.Trello utilizes this behavior to prevent creation of two instances which represent the same object.  (It should be noted that multiple instances can be explicitly created, however.)
- `ThrowOnTrelloError` -  This sets behavior on whether the system will throw an exception on errors received from Trello or merely log them.
- `ExpiryTime` - This sets how long entities will consider their data valid.  After this time the data is considered stale and any access to the object will trigger a call to refresh the data.
- `ChangeSubmissionTime` - This value defines an idle time for entity changes.  Once this idle time is met, any changes will be submitted.  This allows multiple changes to be submitted with a single call.

In addition to the `TrelloConfiguration` static class, there is also a `TrelloProcessor` static class which manages how the request processor thread behaves when the application is shutting down.

- `ConcurrentCallCount` - Acts as a throttle on the number of concurrent API calls that can be made.  The default is 1.
- `Flush()` - Signals the processor to flush any pending requests.  Will block until all requests have been processed.  This can be used at any time.  This should be called at the end of the application to ensure that all requests are fulfilled.
- *Obsolete and unused as of v1.9.0* `WaitForPendingRequests` - `true` initializes the processor thread to start in the foreground, which will hold the application open until the `Shutdown()` method is called.  `false` will allow the application to close immediately, but may leave some requests unsent.  The default is `false`.
- *Obsolete as of v1.9.0* `Shutdown()` - Signals the processor thread that the application is closing.  When this occurs, it calls for all requests to be added to the queue so that they can be processed, shortcutting the idle time configured by `TrelloConfiguration.ChangeSubmissionTime`. *As of v1.9.0, this method simply forwards its call to `Flush()`.*

See [Request Processing](Requests) for more information on how requests are processed.

# Default configuration

Manatee.Trello will provide fallback implementations for the cache and log.  The cache is a simple, thread-safe, list-based cache, and the log only writes to the Debug window.  These will be used whenever these properties are set to `null`.