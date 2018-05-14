# IRestClientProvider

Defines methods required to create an instance of IRestClient.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestClientProvider

## Properties

### [IRestRequestProvider](API-Rest#irestrequestprovider) RequestProvider { get; }

Creates requests for the client.

## Methods

### [IRestClient](API-Rest#irestclient) CreateRestClient(string apiBaseUrl)

Creates an instance of IRestClient.

**Parameter:** apiBaseUrl

The base URL to be used by the client

**Returns:** An instance of IRestClient.

# WebApiClientProvider

Implements IRestClientProvider for WebApi.

**Assembly:** Manatee.Trello.WebApi.dll

**Namespace:** Manatee.Trello.WebApi

**Inheritance hierarchy:**

- Object
- WebApiClientProvider

## Constructors

### WebApiClientProvider()

Creates a new instance of the [WebApiClientProvider](API-Rest#webapiclientprovider) class.

## Properties

### [IRestRequestProvider](API-Rest#irestrequestprovider) RequestProvider { get; }

Creates requests for the client.

## Methods

### [IRestClient](API-Rest#irestclient) CreateRestClient(string apiBaseUrl)

Creates an instance of IRestClient.

**Parameter:** apiBaseUrl

The base URL to be used by the client

**Returns:** An instance of IRestClient.

# IRestClient

Defines methods required to make RESTful calls.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestClient

## Methods

### [IRestResponse](API-Rest#irestresponse) Execute(IRestRequest request)

Makes a RESTful call and ignores any return data.

**Parameter:** request

The request.

### IRestResponse`1 Execute&lt;T&gt;(IRestRequest request)

Makes a RESTful call and expects a single object to be returned.

**Type Parameter:** T (no constraints)

The expected type of object to receive in response.

**Parameter:** request

The request.

**Returns:** The response.

# IRestRequestProvider

Defines methods to generate IRequest objects used to make RESTful calls.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestRequestProvider

# WebApiRequestProvider

Implements IRestRequestProvider for WebApi.

**Assembly:** Manatee.Trello.WebApi.dll

**Namespace:** Manatee.Trello.WebApi

**Inheritance hierarchy:**

- Object
- WebApiRequestProvider

# IRestRequest

Defines properties and methods required to make RESTful requests.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestRequest

## Properties

### [RestMethod](API-Rest#restmethod) Method { get; set; }

Gets and sets the method to be used in the call.

### string Resource { get; }

Gets the URI enpoint for the request.

### [IRestResponse](API-Rest#irestresponse) Response { get; set; }

Stores the response returned by the server.

## Methods

### void AddBody(Object body)

Adds a body to the request.

**Parameter:** body

The body.

### void AddParameter(string name, Object value)

Explicitly adds a parameter to the request.

**Parameter:** name

The name.

**Parameter:** value

The value.

# RestMethod

Enumerates the RESTful call methods required by TrelloService.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- RestMethod

## Fields

### Get

Indicates an HTTP GET operation.

### Put

Indicates an HTTP PUT operation.

### Post

Indicates an HTTP POST operation.

### Delete

Indicates an HTTP DELETE operation.

# RestFile

Defines a file to be included in a REST request.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- Object
- RestFile

## Fields

### static string ParameterKey

Defines a key to be used when attaching a file to a REST request.

## Properties

### Byte[] ContentBytes { get; set; }

The file data

### string FileName { get; set; }

The file name to use for the uploaded file

# IRestResponse

Defines properties required for objects returned by RESTful calls.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestResponse

## Properties

### string Content { get; }

The JSON content returned by the call.

### Exception Exception { get; set; }

Gets any exception that was thrown during the call.

### System.Net.HttpStatusCode StatusCode { get; }

Gets the status code.

# IRestResponse&lt;T&gt;

Defines required properties returned by RESTful calls.

**Type Parameter:** T (no constraints)

The type expected to be returned by the call.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestResponse&lt;T&gt;

## Properties

### T Data { get; }

The deserialized data.

