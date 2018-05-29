---
title: IJsonAttachment
category: API
order: 88
---

Defines the JSON structure for the Attachment object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonAttachment

## Properties

### int? Bytes { get; set; }

Gets or sets the size of the attachment.

### DateTime? Date { get; set; }

Gets or sets the date on which the attachment was created.

### string EdgeColor { get; set; }

Gets or sets the border color of the attachment preview on the card.

### bool? IsUpload { get; set; }

?

### [IJsonMember](../IJsonMember#ijsonmember) Member { get; set; }

Gets or sets the ID of the member who created the attachment.

### string MimeType { get; set; }

Gets or sets the type of attachment.

### string Name { get; set; }

Gets or sets the name of the attachment.

### [IJsonPosition](../IJsonPosition#ijsonposition) Pos { get; set; }

Gets or sets the attachment&#39;s position.

### List&lt;IJsonImagePreview&gt; Previews { get; set; }

Gets or sets a collection of previews for the attachment.

### string Url { get; set; }

Gets or sets the attachment storage location.

