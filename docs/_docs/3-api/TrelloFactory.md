---
title: TrelloFactory
category: API
order: 247
---

Supports entity creation for dependency-injected applications.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- TrelloFactory

## Methods

### [IAction](../IAction#iaction) Action(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.IAction.

**Parameter:** id

The action ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.IAction

### [IBoard](../IBoard#iboard) Board(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.IBoard.

**Parameter:** id

The board ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.IBoard

### [ICard](../ICard#icard) Card(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.ICard.

**Parameter:** id

The board ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.ICard

### [ICheckList](../ICheckList#ichecklist) CheckList(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.ICheckList.

**Parameter:** id

The checklist ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.ICheckList

### [IDropDownOption](../IDropDownOption#idropdownoption) DropDownOption(string text, LabelColor color)

Creates a Manatee.Trello.ITrelloFactory.DropDownOption(System.String,Manatee.Trello.LabelColor).

**Parameter:** text

The drop down text.

**Parameter:** color

(Optional) The drop down color.

**Returns:** An Manatee.Trello.IDropDownOption

### [IList](../IList#ilist) List(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.IList.

**Parameter:** id

The list ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.IList

### Task&lt;[IMe](../IMe#ime)&gt; Me(CancellationToken ct)

Creates an Manatee.Trello.IMe.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** An Manatee.Trello.IMe

#### Remarks

This performs a call to the API to get the member authorized by Manatee.Trello.TrelloAuthorization.Default.

### [IMember](../IMember#imember) Member(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.IMember.

**Parameter:** id

The member ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.IMember

### [IMemberSearch](../IMemberSearch#imembersearch) MemberSearch(string query, int? limit, IBoard board, IOrganization organization, bool? restrictToOrganization, TrelloAuthorization auth)

Creates a new instance of the Manatee.Trello.IMemberSearch object and performs the search.

**Parameter:** query

The query.

**Parameter:** limit

(Optional) The result limit. Can be a value from 1 to 20. The default is 8.

**Parameter:** board

(Optional) A board to which the search should be limited.

**Parameter:** organization

(Optional) An organization to which the search should be limited.

**Parameter:** restrictToOrganization

(Optional) Restricts the search to only organization members.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided,
Manatee.Trello.TrelloAuthorization.Default will be used.

### [INotification](../INotification#inotification) Notification(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.INotification.

**Parameter:** id

The notification ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.INotification

### [IOrganization](../IOrganization#iorganization) Organization(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.IOrganization.

**Parameter:** id

The action ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.IOrganization

### [ISearch](../ISearch#isearch) Search(ISearchQuery query, int? limit, SearchModelType modelTypes, IEnumerable&lt;IQueryable&gt; context, bool isPartial, TrelloAuthorization auth)

Creates an Manatee.Trello.ISearch.

**Parameter:** query

The search query.

**Parameter:** limit

(Optional) The maximum number of items to return.

**Parameter:** modelTypes

(Optional) The model types desired.

**Parameter:** context

(Optional) The context (scope) of the search.

**Parameter:** isPartial

(Optional) Allow &quot;starts with&quot; matches.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.ISearch

### [ISearch](../ISearch#isearch) Search(string query, int? limit, SearchModelType modelTypes, IEnumerable&lt;IQueryable&gt; context, bool isPartial, TrelloAuthorization auth)

Creates an Manatee.Trello.ISearch.

**Parameter:** query

The search query.

**Parameter:** limit

(Optional) The maximum number of items to return.

**Parameter:** modelTypes

(Optional) The model types desired.

**Parameter:** context

(Optional) The context (scope) of the search.

**Parameter:** isPartial

(Optional) Allow &quot;starts with&quot; matches.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.ISearch

### [ISearchQuery](../ISearchQuery#isearchquery) SearchQuery()

Creates a new empty Manatee.Trello.ISearchQuery.

**Returns:** An Manatee.Trello.ISearchQuery

### [IToken](../IToken#itoken) Token(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.IToken.

**Parameter:** id

The action ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.IToken

### Task&lt;[IWebhook`1](../IWebhook`1#iwebhook1)&gt; Webhook&lt;T&gt;(T target, string callBackUrl, string description, TrelloAuthorization auth, CancellationToken ct)

Creates an Manatee.Trello.IWebhook`1 and registers a new webhook with Trello.

**Type Parameter:** T : [ICanWebhook](../ICanWebhook#icanwebhook)

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

**Returns:** An Manatee.Trello.IWebhook`1

### [IWebhook`1](../IWebhook`1#iwebhook1) Webhook&lt;T&gt;(string id, TrelloAuthorization auth)

Creates an Manatee.Trello.IWebhook`1 for and existing webhook.

**Type Parameter:** T : [ICanWebhook](../ICanWebhook#icanwebhook)

**Parameter:** id

The action ID.

**Parameter:** auth

(Optional) The authorization.

**Returns:** An Manatee.Trello.IWebhook`1

