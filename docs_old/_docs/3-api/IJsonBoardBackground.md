---
title: IJsonBoardBackground
category: API
order: 94
---

Defines the JSON structure for the BoardBackground object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardBackground

## Properties

### string BottomColor { get; set; }

The bottom color of a gradient background.

### [BoardBackgroundBrightness](../BoardBackgroundBrightness#boardbackgroundbrightness)? Brightness { get; set; }

Gets the overall brightness of the background.

### string Color { get; set; }

Gets or sets the color.

### string Image { get; set; }

Gets or sets the url for the image.

### List&lt;[IJsonImagePreview](../IJsonImagePreview#ijsonimagepreview)&gt; ImageScaled { get; set; }

Gets or sets a collection of scaled images.

### bool? Tile { get; set; }

Gets or sets whether the image should be tiled when displayed.

### string TopColor { get; set; }

The top color of a gradient background.

