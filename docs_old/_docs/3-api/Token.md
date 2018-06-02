---
title: Token
category: API
order: 252
---

Represents a user token.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Token

## Constructors

### Token(string id, TrelloAuthorization auth = null)

Creates a new instance of the Manatee.Trello.Token object.

**Parameter:** id

The token&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, Manatee.Trello.TrelloAuthorization.Default will be used.

#### Remarks

The supplied ID can be either the full ID or the token itself.

## Properties

### static Token.Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### string AppName { get; }

Gets the name of the application associated with the token.

### [ITokenPermission](../ITokenPermission#itokenpermission) BoardPermissions { get; }

Gets the permissions on boards granted by the token.

### DateTime CreationDate { get; }

Gets the creation date of the token.

### DateTime? DateCreated { get; }

Gets the date and time the token was created.

### DateTime? DateExpires { get; }

Gets the date and time the token expires, if any.

### string Id { get; }

Gets the token&#39;s ID.

### [IMember](../IMember#imember) Member { get; }

Gets the member for which the token was issued.

### [ITokenPermission](../ITokenPermission#itokenpermission) MemberPermissions { get; }

Gets the permissions on members granted by the token.

### [ITokenPermission](../ITokenPermission#itokenpermission) OrganizationPermissions { get; }

Gets the permissions on organizations granted by the token.

## Methods

### Task Delete(CancellationToken ct = default(CancellationToken))

Deletes the token.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the token from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(bool force = False, CancellationToken ct = default(CancellationToken))

Refreshes the token data.

**Parameter:** force

Indicates that the refresh should ignore the value in Manatee.Trello.TrelloConfiguration.RefreshThrottle and make the call to the API.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

