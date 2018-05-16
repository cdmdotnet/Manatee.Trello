---
title: Exceptions
category: Version 2.x - API
order: 6
---

# TrelloInteractionException

Thrown when Trello reports an error with a request.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Exceptions

**Inheritance hierarchy:**

- Object
- Exception
- TrelloInteractionException

## Constructors

### TrelloInteractionException(Exception innerException)

Creates a new instance of the TrelloInteractionException class.

**Parameter:** innerException

The exception which occurred during the call.

### TrelloInteractionException(string message, Exception innerException)

Creates a new instance of the TrelloInteractionException class.

**Parameter:** message

A custom message.

**Parameter:** innerException

The exception which occurred during the call.

### Exception(string message, Exception innerException)

Creates a new instance of the TrelloInteractionException class.

**Parameter:** message

A custom message.

**Parameter:** innerException

The exception which occurred during the call.

# ValidationException&lt;T&gt;

Thrown when validation fails on a Trello object property.

**Type Parameter:** T (no constraints)

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Exceptions

**Inheritance hierarchy:**

- Object
- Exception
- ValidationException&lt;T&gt;

## Properties

### IEnumerable&lt;string&gt; Errors { get; }

Gets a collection of errors that occurred while validating the value.

