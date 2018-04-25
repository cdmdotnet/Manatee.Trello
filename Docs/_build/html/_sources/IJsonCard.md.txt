# IJsonCard

Defines the JSON structure for the Card object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonCard

## Properties

### List&lt;IJsonAction&gt; Actions { get; set; }

Gets or sets a collection of actions.

### List&lt;IJsonAttachment&gt; Attachments { get; set; }

Gets or sets a collection of attachments.

### [IJsonBadges](IJsonBadges#ijsonbadges) Badges { get; set; }

Gets or set the badges displayed on the card cover.

### [IJsonBoard](IJsonBoard#ijsonboard) Board { get; set; }

Gets or sets the ID of the board which contains the card.

### [IJsonCard](IJsonCard#ijsoncard) CardSource { get; set; }

Gets or sets a card to be used as a template during creation.

### List&lt;IJsonCheckList&gt; CheckLists { get; set; }

Gets or sets a collection of checklists.

### bool? Closed { get; set; }

Gets or sets whether a card has been archived.

### List&lt;IJsonAction&gt; Comments { get; set; }

Gets or sets a collection of comments.

### List&lt;IJsonCustomField&gt; CustomFields { get; set; }

Gets or sets a collection of custom field instances.

### DateTime? DateLastActivity { get; set; }

Gets or sets the date of last activity for a card.

### string Desc { get; set; }

Gets or sets the card&#39;s description.

### DateTime? Due { get; set; }

Gets or sets the card&#39;s due date.

### bool? DueComplete { get; set; }

Gets or sets whether the card is complete.

### bool ForceDueDate { get; set; }

Gets or sets whether the due date should be serialized, even if it is null.

### string IdAttachmentCover { get; set; }

Gets or sets the ID of the attachment cover image.

### string IdLabels { get; set; }

Gets or sets a comma-delimited list of label IDs. Used during card creation.

### string IdMembers { get; set; }

Gets or sets a comma-delimited list of member IDs. Used during card creation.

### int? IdShort { get; set; }

Gets or sets the card&#39;s short ID.

### List&lt;IJsonLabel&gt; Labels { get; set; }

Gets or sets the labels assigned to this card.

### [IJsonList](IJsonList#ijsonlist) List { get; set; }

Gets or sets the ID of the list which contains the card.

### bool? ManualCoverAttachment { get; set; }

Gets or sets whether the cover attachment was manually selected

### List&lt;IJsonMember&gt; Members { get; set; }

Gets or sets a collection of members.

### List&lt;IJsonMember&gt; MembersVoted { get; set; }

Gets or sets a collection of members who have voted for the card.

### string Name { get; set; }

Gets or sets the card&#39;s name

### [IJsonPosition](IJsonPosition#ijsonposition) Pos { get; set; }

Gets or sets the card&#39;s position.

### List&lt;IJsonPowerUpData&gt; PowerUpData { get; set; }

Gets or sets a collection of power-up data.

### string ShortUrl { get; set; }

Gets or sets the short URL for this card.

### List&lt;IJsonSticker&gt; Stickers { get; set; }

Gets or sets a collection of stickers.

### bool? Subscribed { get; set; }

Gets or sets whether the current member is subscribed to this card.

### string Url { get; set; }

Gets or sets the URL for this card.

### Object UrlSource { get; set; }

Gets or sets a URL to be imported during creation.

