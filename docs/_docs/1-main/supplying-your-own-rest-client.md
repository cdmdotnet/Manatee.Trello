---
title: Supplying your own ReST client
category:
order: 6
---

By default, Manatee.Trello uses a REST client backed by ASP.Net's `WebClient` class.

If you would like to use a different client, you will have to build the provider yourself.  The Manatee.Trello.Rest namespace contains a number of interfaces which must be implemented in order to use your preferred REST provider.

- `IRestClient` - This interface defines methods used to make RESTful calls.  Its implementation will serve as a wrapper for your preferred REST provider.
- `IRestClientProvider` - This interface defines methods required to create an instance of `IRestClient`.  Within your implementation, you can maintain a single instance of the client object, if the client supports it, but Manatee.Trello will request the client object for each request.
- `IRestRequest` - This interface defines properties and methods required to make RESTful requests.  Its implementation serves as a wrapper for your REST provider's request objects.
- `IRestRequestProvider` - This interface defines methods required to generate `IRequest` objects used to make RESTful calls.  Its implementation will be exposed by your `IRestClientProvider` implementation.
- `IRestResponse` - Defines properties required for objects returned by RESTful calls.  Its implmentation serves as a wrapper for your REST provider's response objects.
- `IRestResponse<T>` - Extends `IRestResponse` to add a typed `Data` property.

Once you have your implementations, set the `RestRequestProvider` property on the `IRestClientProvider` implementation to the `IRestRequestProvider` implementation, then set the `RestClientProvider` static property on the `TrelloConfiguration` static class to your `IRestClientProvider` implementation.

```csharp
public class MyRestRequestProvider : IRestRequestProvider
{
    ...
}
public class MyRestClientProvider : IRestClientProvider
{
    ...
}

public static void Main()
{
...
    var clientProvider = new MyRestClientProvider();
    var requestProvider = new MyRestRequestProvider();
    clientProvider.RestRequestProvider = requestProvider;
    TrelloConfiguration.RestClientProvider = clientProvider;
    ...
}
```

That's it.  Manatee.Trello will now call your REST provider through the wrappers you have provided.

>**NOTE**  Your REST provider should allow using a custom JSON serializer.  It is suggested that you code your `IRestClientProvider` implementation to use the serializer defined by the `TrelloConfiguration` stataic class.  The default serializer implementation is backed by [Manatee.Json](https://github.com/gregsdennis/Manatee.Json).  See the [JSON Serializer](/1-main/Supplying-your-own-JSON-serializer) section of this wiki for more details on using a custom JSON serializer.

# Error handling

Manatee.Trello is designed to expect exceptions from the REST provider it is given.  This means that your REST client wrapper implementation (or the client itself) should throw exceptions when receiving invalid data.
