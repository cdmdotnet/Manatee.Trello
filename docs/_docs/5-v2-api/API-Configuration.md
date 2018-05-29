---
title: Configuration
category: Version 2.x - API
order: 7
---

> **NOTICE** In migrating to this new documentation, many (if not all) of the links are broken.  Please use the sidebar on the left for navigation.

# TrelloConfiguration

Exposes a set of run-time options for Manatee.Trello.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- TrelloConfiguration

## Properties

### static Manatee.Trello.Contracts.ICache Cache { get; set; }

Provides a cache to manage all Trello objects.

### static TimeSpan ChangeSubmissionTime { get; set; }

Specifies a length of time an object holds changes before it submits them. The timer is reset with every change. Default is 100 ms.

#### Remarks

Setting a value of 0 ms will result in instant upload of changes, dramatically increasing call volume and slowing performance.

### static TimeSpan DelayBetweenRetries { get; set; }

Specifies a delay between retry attempts.

### static [IDeserializer](/API-Json#ideserializer) Deserializer { get; set; }

Specifies the deserializer for the REST client.

### static TimeSpan ExpiryTime { get; set; }

Specifies a length of time after which each Trello object will be marked as expired. Default is 30 seconds.

### static [IJsonFactory](/API-Json#ijsonfactory) JsonFactory { get; set; }

Provides a factory which is used to create instances of JSON objects.

### static Manatee.Trello.Contracts.ILog Log { get; set; }

Provides logging for Manatee.Trello. The default log writes to the Console window.

### static int MaxRetryCount { get; set; }

Specifies a maximum number of retries allowed before an error is thrown.

### static [IRestClientProvider](/API-Rest#irestclientprovider) RestClientProvider { get; set; }

Specifies the REST client provider.

### static Func&lt;IRestResponse, int, bool&gt; RetryPredicate { get; set; }

Specifies a predicate to execute to determine if a retry should be attempted. The default simply uses [TrelloConfiguration.MaxRetryCount](/API-Configuration#static-int-maxretrycount--get-set-) and [TrelloConfiguration.DelayBetweenRetries](/API-Configuration#static-timespan-delaybetweenretries--get-set-).

#### Remarks

Parameters:

- [IRestResponse](/API-Rest#irestresponse) - The response object from the REST provider. Will need to be cast to the appropriate type.
- System.Int32 - The number of retries attempted.

Return value:

- System.Boolean - True if the call should be retried; false otherwise.

### static IList&lt;HttpStatusCode&gt; RetryStatusCodes { get; }

Specifies which HTTP response status codes should trigger an automatic retry.

### static [ISerializer](/API-Json#iserializer) Serializer { get; set; }

Specifies the serializer for the REST client.

### static bool ThrowOnTrelloError { get; set; }

Specifies whether the service should throw an exception when an error is received from Trello. Default is true.

# TrelloProcessor

Provides options and control for the internal request queue processor.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- TrelloProcessor

## Properties

### static int ConcurrentCallCount { get; set; }

Specifies the number of concurrent calls to the Trello API that the processor can make. Default is 1.

## Methods

### static void CancelPendingRequests()

Cancels any requests that are still pending. This applies to both downloads and uploads.

### static void Flush()

Signals the processor that the application is shutting down. The processor will perform a &quot;last call&quot; for pending requests.

# TrelloAuthorization

Contains authorization tokens needed to connect to Trello&#39;s API.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- TrelloAuthorization

## Properties

### static [TrelloAuthorization](/API-Configuration#trelloauthorization) Default { get; }

Gets the default authorization.

### string AppKey { get; set; }

The token which identifies the application attempting to connect.

### string UserToken { get; set; }

The token which identifies special permissions as granted by a specific user.

