---
title: IJsonToken
category: API
order: 122
---

Defines the JSON structure for the Token object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonToken

## Properties

### DateTime? DateCreated { get; set; }

Gets or sets the date the token was created.

### DateTime? DateExpires { get; set; }

Gets or sets the date the token will expire, if any.

### string Identifier { get; set; }

Gets or sets the identifier of the application which requested the token.

### [IJsonMember](../IJsonMember#ijsonmember) Member { get; set; }

Gets or sets the ID of the member who issued the token.

### List&lt;[IJsonTokenPermission](../IJsonTokenPermission#ijsontokenpermission)&gt; Permissions { get; set; }

Gets or sets the collection of permissions granted by the token.

