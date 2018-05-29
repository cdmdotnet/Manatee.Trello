---
title: DefaultRestClientProvider
category: API
order: 197
---

# DefaultRestClientProvider

Implements [IRestClientProvider](IRestClientProvider#irestclientprovider) using ASP.Net&#39;s System.Net.Http.HttpClient.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- Object
- DefaultRestClientProvider

## Properties

### static [DefaultRestClientProvider](DefaultRestClientProvider#defaultrestclientprovider) Instance { get; }

Singleton instance of [DefaultRestClientProvider](DefaultRestClientProvider#defaultrestclientprovider).

### [IRestRequestProvider](IRestRequestProvider#irestrequestprovider) RequestProvider { get; }

Creates requests for the client.

## Methods

### [IRestClient](IRestClient#irestclient) CreateRestClient(string apiBaseUrl)

Creates an instance of [IRestClient](IRestClient#irestclient).

**Parameter:** apiBaseUrl

The base URL to be used by the client

**Returns:** An instance of [IRestClient](IRestClient#irestclient).

### void Dispose()

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

