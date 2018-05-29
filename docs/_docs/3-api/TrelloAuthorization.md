# TrelloAuthorization

Contains authorization tokens needed to connect to Trello&#39;s API.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- TrelloAuthorization

## Properties

### static [TrelloAuthorization](TrelloAuthorization#trelloauthorization) Default { get; }

Gets the default authorization.

### string AppKey { get; set; }

The token which identifies the application attempting to connect.

### string UserToken { get; set; }

The token which identifies special permissions as granted by a specific user.

