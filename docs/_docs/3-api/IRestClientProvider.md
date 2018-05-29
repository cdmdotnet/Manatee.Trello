---
title: IRestClientProvider
category: API
order: 199
---

# IRestClientProvider

Defines methods required to create an instance of IRestClient.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestClientProvider

## Properties

### [IRestRequestProvider](IRestRequestProvider#irestrequestprovider) RequestProvider { get; }

Creates requests for the client.

## Methods

### [IRestClient](IRestClient#irestclient) CreateRestClient(string apiBaseUrl)

Creates an instance of IRestClient.

**Parameter:** apiBaseUrl

The base URL to be used by the client

**Returns:** An instance of IRestClient.

