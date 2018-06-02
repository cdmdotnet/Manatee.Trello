---
title: IBoardBackground
category: API
order: 60
---

Represents a background image for a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IBoardBackground

## Properties

### [WebColor](../WebColor#webcolor) BottomColor { get; }

Gets the bottom color of a gradient background.

### [BoardBackgroundBrightness](../BoardBackgroundBrightness#boardbackgroundbrightness)? Brightness { get; }

Gets the brightness of the background.

### [WebColor](../WebColor#webcolor) Color { get; }

Gets the color of a stock solid-color background.

### string Image { get; }

Gets the image of a background.

### bool? IsTiled { get; }

Gets whether the image is tiled when displayed.

### IReadOnlyCollection&lt;[IImagePreview](../IImagePreview#iimagepreview)&gt; ScaledImages { get; }

Gets a collections of scaled background images.

### [WebColor](../WebColor#webcolor) TopColor { get; }

Gets the top color of a gradient background.

