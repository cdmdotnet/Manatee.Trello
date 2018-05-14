Prior to v1.9.0, Manatee.Trello maintained a dedicated thread on which all API requests would be queued, then each request would be fulfilled in turn.  This had the effect of blocking *all* threads waiting for responses.

As of v1.9.0, Manatee.Trello supports sending API requests concurrently, with some limitations.  Each thread that needs a request fulfilled will operate in a synchronous manner.  So while a single thread may be blocked temporarily while it is waiting for a response from the server, multiple threads are processed in parallel.

The number of concurrent requests that can be processed is controlled by the `TrelloProcessor.ConcurrentCallCount` static property.  This property has a default of 1, which will result in similar behavior to that of the pre-v1.9.0 libraries.  Setting this to a higher value will allow more calls to be made concurrently, effectively throttled at this number.

Because Manatee.Trello has no way of keeping the application alive if there are pending requests (threads will be terminated when the application closes), `TrelloProcessor` also exposes the `Flush()` static method.  This should be used at the end of the appliation to ensure that all requests are fulfilled.  This method may also be called at any time if the client sees a need.

Also, as of v1.11.0, there is also a `TrelloProcessor.CancelPendingRequests()` method which will stub responses for any calls still pending, effectively cancelling them.
