---
title: IRestResponse
category: API
order: 170
---

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

### HttpStatusCode StatusCode { get; }

Gets the status code.

