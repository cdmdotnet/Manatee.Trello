---
title: IRestRequest
category: API
order: 163
---

Defines properties and methods required to make RESTful requests.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Rest

**Inheritance hierarchy:**

- IRestRequest

## Properties

### [RestMethod](../RestMethod#restmethod) Method { get; set; }

Gets and sets the method to be used in the call.

### string Resource { get; }

Gets the URI enpoint for the request.

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

