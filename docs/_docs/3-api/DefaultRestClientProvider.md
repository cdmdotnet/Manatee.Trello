---
title: DefaultRestClientProvider
category: API
order: 49
---

Implements Manatee.Trello.Rest.IRestClientProvider using ASP.Net&#39;s System.Net.Http.HttpClient.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- Object
- DefaultRestClientProvider

## Properties

### static [DefaultRestClientProvider](../DefaultRestClientProvider#defaultrestclientprovider) Instance { get; }

Singleton instance of Manatee.Trello.Rest.DefaultRestClientProvider.

### [IRestRequestProvider](../IRestRequestProvider#irestrequestprovider) RequestProvider { get; }

Creates requests for the client.

## Methods

### [IRestClient](../IRestClient#irestclient) CreateRestClient(string apiBaseUrl)

Creates an instance of Manatee.Trello.Rest.IRestClient.

**Parameter:** apiBaseUrl

The base URL to be used by the client

**Returns:** An instance of Manatee.Trello.Rest.IRestClient.

### void Dispose()

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

