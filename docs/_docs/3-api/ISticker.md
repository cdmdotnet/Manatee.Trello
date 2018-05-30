---
title: ISticker
category: API
order: 170
---

Represents a sticker on a card.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ISticker

## Properties

### string ImageUrl { get; }

Gets the URL for the sticker&#39;s image.

### double? Left { get; set; }

Gets or sets the position of the left edge.

### string Name { get; }

Gets the name of the sticker.

### IReadOnlyCollection&lt;[IImagePreview](../IImagePreview#iimagepreview)&gt; Previews { get; }

Gets the collection of previews.

### int? Rotation { get; set; }

Gets or sets the rotation.

#### Remarks

Rotation is clockwise and in degrees.

### double? Top { get; set; }

Gets or sets the position of the top edge.

### int? ZIndex { get; set; }

Gets or sets the z-index.

## Events

### Action&lt;[ISticker](../ISticker#isticker), IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the sticker is updated.

## Methods

### Task Delete(CancellationToken ct)

Deletes the sticker.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the sticker from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct)

Refreshes the sticker data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

