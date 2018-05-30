---
title: IJsonSticker
category: API
order: 121
---

Defines the JSON structure for the Sticker object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonSticker

## Properties

### double? Left { get; set; }

Gets or sets the position of the left edge of the sticker.

### string Name { get; set; }

Gets or sets the name of the sticker.

### List&lt;[IJsonImagePreview](../IJsonImagePreview#ijsonimagepreview)&gt; Previews { get; set; }

Gets or sets a collection of previews for the attachment.

### int? Rotation { get; set; }

Gets or sets the rotation angle of the sticker in degrees.

### double? Top { get; set; }

Gets or sets the position of the top edge of the sticker.

### string Url { get; set; }

Gets or sets the image&#39;s URL.

### int? ZIndex { get; set; }

Gets or sets the sticker&#39;s z-index.

