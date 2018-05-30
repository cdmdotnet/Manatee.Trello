---
title: IImagePreview
category: API
order: 84
---

Represents a preview for an image.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IImagePreview

## Properties

### DateTime CreationDate { get; }

Gets the creation date of the image preview.

### int? Height { get; }

Gets the preview&#39;s height in pixels.

### bool? IsScaled { get; set; }

Gets whether the attachment was scaled to generate the preview.

### string Url { get; }

Gets the URI where the preview data is stored.

### int? Width { get; }

Gets the preview&#39;s width in pixels.

