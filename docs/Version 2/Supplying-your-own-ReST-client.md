Manatee.Trello has no default REST provider.  However, two additional Nuget packages [Manatee.Trello.RestSharp](https://www.nuget.org/packages/Manatee.Trello.RestSharp/) and [Manatee.Trello.WebApi](https://www.nuget.org/packages/Manatee.Trello.WebApi/) are available which provide compatible REST providers.

- Manatee.Trello.RestSharp is backed by (you guessed it) [RestSharp](http://restsharp.org/).  There have been some [issues](https://trello.com/c/9oqS8oxg) with the .Net 4.5+ versions, so it's suggested you don't use this one unless you are using .Net 4.0 or earlier.
- Manatee.Trello.WebApi is backed by the ASP.Net WebApi libraries.  Only .Net 4.5+ is supported for this library.

If you would like to use a different REST client, you will have to build the provider yourself.  The Manatee.Trello.Rest namespace contains a number of interfaces which must be implemented in order to use your preferred REST provider.

- IRestClient - This interface defines methods used to make RESTful calls.  Its implementation will serve as a wrapper for your preferred REST provider.

- IRestClientProvider - This interface defines methods required to create an instance of IRestClient.

- IRestRequest - This interface defines properties and methods required to make RESTful requests.  Its implementation serves as a wrapper for your REST provider's request objects.

- IRestRequestProvider - This interface defines methods required to generate IRequest objects used to make RESTful calls.  Its implementation will be exposed by your IRestClientProvider implementation.

- IRestResponse - Defines properties required for objects returned by RESTful calls.  Its implmentation serves as a wrapper for your REST provider's response objects.

- IRestResponse<T> - Extends IRestResponse to add a typed Data property.

Once you have your implementations, set the RestRequestProvider property on the IRestClientProvider to the IRestRequestProvider implementation, then set the RestClientProvider property on the TrelloServiceConfiguration class to the IRestClientProvider implementation.

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

That's it.  Manatee.Trello will now call your REST provider through the wrappers you have provided.

>**Note:**  If your REST provider allows using a custom JSON serializer, it is suggested that you code your `IRestClientProvider` implementation to be configurable through the use of the `TrelloServiceConfiguration` class.  I have provded a serializer implementation backed by [Manatee.Json](https://www.nuget.org/packages/Manatee.Trello.ManateeJson/).  See the JSON Serializer section of this wiki for more details on using a custom JSON serializer.

# Error handling

Manatee.Trello is designed to expect exceptions from the REST provider it is given.  This means that your REST client wrapper implementation (or the client itself) should throw exceptions when receiving invalid data.
