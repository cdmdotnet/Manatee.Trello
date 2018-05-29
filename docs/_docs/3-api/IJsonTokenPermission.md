---
title: IJsonTokenPermission
category: API
order: 123
---

Defines the JSON structure for the TokenPermission object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonTokenPermission

## Properties

### string IdModel { get; set; }

Gets or sets the ID of the model to which a token grants permissions.

### [TokenModelType](../TokenModelType#tokenmodeltype)? ModelType { get; set; }

Gets or sets the type of the model.

### bool? Read { get; set; }

Gets or sets whether a token grants read permissions to the model.

### bool? Write { get; set; }

Gets or sets whether a token grants write permissions to the model.

