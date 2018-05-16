---
title: Tokens
category: Version 2.x - API
order: 15
---

# Token

Represents a user token.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Token

## Constructors

### Token(string id, TrelloAuthorization auth)

Creates a new instance of the [Token](/API-Tokens#token) object.

**Parameter:** id

The token&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](/API-Configuration#static-trelloauthorization-default--get-) will be used.

#### Remarks

The supplied ID can be either the full ID or the token itself.

## Properties

### static Manatee.Trello.Token+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### string AppName { get; }

Gets the name of the application associated with the token.

### [TokenPermission](/API-Tokens#tokenpermission) BoardPermissions { get; }

Gets the permissions on boards granted by the token.

### DateTime CreationDate { get; }

Gets the creation date of the token.

### DateTime? DateCreated { get; }

Gets the date and time the token was created.

### DateTime? DateExpires { get; }

Gets the date and time the token expires, if any.

### string Id { get; }

Gets the token&#39;s ID.

### [Member](/API-Members#member) Member { get; }

Gets the member for which the token was issued.

### [TokenPermission](/API-Tokens#tokenpermission) MemberPermissions { get; }

Gets the permissions on members granted by the token.

### [TokenPermission](/API-Tokens#tokenpermission) OrganizationPermissions { get; }

Gets the permissions on organizations granted by the token.

## Methods

### void Delete()

Permanently deletes the token from Trello.

#### Remarks

This instance will remain in memory and all properties will remain accessible.

### void Refresh()

Marks the token to be refreshed the next time data is accessed.

### string ToString()

Returns the [AppName](/API-Tokens#string-appname--get-).

**Returns:** A string that represents the current object.

# TokenPermission

Represents permissions granted by a token.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- TokenPermission

## Properties

### bool? CanRead { get; }

Gets whether a token can read values.

### bool? CanWrite { get; }

Gets whether a token can write values.

