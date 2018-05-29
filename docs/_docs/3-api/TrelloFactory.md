---
title: TrelloFactory
category: API
order: 189
---

# TrelloFactory

Supports entity creation for dependency-injected applications.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- TrelloFactory

## Methods

### [IAction](IAction#iaction) Action(string id, TrelloAuthorization auth)

Creates an [IAction](IAction#iaction).

**Parameter:** id

The action ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [IAction](IAction#iaction)

### [IBoard](IBoard#iboard) Board(string id, TrelloAuthorization auth)

Creates an [IBoard](IBoard#iboard).

**Parameter:** id

The board ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [IBoard](IBoard#iboard)

### [ICard](ICard#icard) Card(string id, TrelloAuthorization auth)

Creates an [ICard](ICard#icard).

**Parameter:** id

The board ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [ICard](ICard#icard)

### [ICheckList](ICheckList#ichecklist) CheckList(string id, TrelloAuthorization auth)

Creates an [ICheckList](ICheckList#ichecklist).

**Parameter:** id

The checklist ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [ICheckList](ICheckList#ichecklist)

### [IDropDownOption](IDropDownOption#idropdownoption) DropDownOption(string text, LabelColor color)

Creates a Manatee.Trello.ITrelloFactory.DropDownOption(System.String,Manatee.Trello.LabelColor).

**Parameter:** text

The drop down text.

**Parameter:** color

(Optional) The drop down color.

**Returns:** An [IDropDownOption](IDropDownOption#idropdownoption)

### [IList](IList#ilist) List(string id, TrelloAuthorization auth)

Creates an [IList](IList#ilist).

**Parameter:** id

The list ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [IList](IList#ilist)

### Task&lt;IMe&gt; Me(CancellationToken ct)

Creates an [IMe](IMe#ime).

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** An [IMe](IMe#ime)

#### Remarks

This performs a call to the API to get the member authorized by [TrelloAuthorization.Default](TrelloAuthorization#static-trelloauthorization-default--get-).

### [IMember](IMember#imember) Member(string id, TrelloAuthorization auth)

Creates an [IMember](IMember#imember).

**Parameter:** id

The member ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [IMember](IMember#imember)

### [INotification](INotification#inotification) Notification(string id, TrelloAuthorization auth)

Creates an [INotification](INotification#inotification).

**Parameter:** id

The notification ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [INotification](INotification#inotification)

### [IOrganization](IOrganization#iorganization) Organization(string id, TrelloAuthorization auth)

Creates an [IOrganization](IOrganization#iorganization).

**Parameter:** id

The action ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [IOrganization](IOrganization#iorganization)

### [ISearchQuery](ISearchQuery#isearchquery) SearchQuery()

Creates a new empty [ISearchQuery](ISearchQuery#isearchquery).

**Returns:** An [ISearchQuery](ISearchQuery#isearchquery)

### [IToken](IToken#itoken) Token(string id, TrelloAuthorization auth)

Creates an [IToken](IToken#itoken).

**Parameter:** id

The action ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [IToken](IToken#itoken)

### Task`1 Webhook&lt;T&gt;(T target, string callBackUrl, string description, TrelloAuthorization auth, CancellationToken ct)

Creates an [IWebhook`1](IWebhook`1#iwebhook1) and registers a new webhook with Trello.

**Type Parameter:** T : [ICanWebhook](ICanWebhook#icanwebhook)

**Parameter:** target

The action ID.

**Parameter:** callBackUrl

A URL that Trello can POST to.

**Parameter:** description

A description.

**Parameter:** auth

(Optional) The authorization.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** An [IWebhook`1](IWebhook`1#iwebhook1)

### IWebhook`1 Webhook&lt;T&gt;(string id, TrelloAuthorization auth)

Creates an [IWebhook`1](IWebhook`1#iwebhook1) for and existing webhook.

**Type Parameter:** T : [ICanWebhook](ICanWebhook#icanwebhook)

**Parameter:** id

The action ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An [IWebhook`1](IWebhook`1#iwebhook1)

