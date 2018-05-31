---
title: IRestRequestProvider
category: API
order: 164
---

Defines methods to generate IRequest objects used to make RESTful calls.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestRequestProvider

## Methods

### [IRestRequest](../IRestRequest#irestrequest) Create(string endpoint, IDictionary&lt;string, Object&gt; parameters = null)

Creates a general request using a collection of objects and an additional parameter to generate the resource string and an object to supply additional parameters.

**Parameter:** endpoint

The method endpoint the request calls.

**Parameter:** parameters

A list of paramaters to include in the request.

**Returns:** An IRestRequest instance which can be sent to an IRestClient.

