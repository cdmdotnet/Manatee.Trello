---
title: BoardBackground
category: API
order: 10
---

Represents a background image for a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- BoardBackground

## Properties

### static [BoardBackground](../BoardBackground#boardbackground) Blue { get; }

The standard blue board background.

### static [BoardBackground](../BoardBackground#boardbackground) Green { get; }

The standard green board background.

### static [BoardBackground](../BoardBackground#boardbackground) Grey { get; }

The standard grey board background.

### static [BoardBackground](../BoardBackground#boardbackground) Lime { get; }

The standard bright green board background.

### static [BoardBackground](../BoardBackground#boardbackground) Orange { get; }

The standard orange board background.

### static [BoardBackground](../BoardBackground#boardbackground) Pink { get; }

The standard pink board background.

### static [BoardBackground](../BoardBackground#boardbackground) Purple { get; }

The standard purple board background.

### static [BoardBackground](../BoardBackground#boardbackground) Red { get; }

The standard red board background.

### static [BoardBackground](../BoardBackground#boardbackground) Sky { get; }

The standard light blue board background.

### [WebColor](../WebColor#webcolor) BottomColor { get; }

Gets the bottom color of a gradient background.

### [BoardBackgroundBrightness](../BoardBackgroundBrightness#boardbackgroundbrightness)? Brightness { get; }

Gets the brightness of the background.

### [WebColor](../WebColor#webcolor) Color { get; }

Gets the color of a stock solid-color background.

### string Id { get; }

Gets the background&#39;s ID.

### string Image { get; }

Gets the image of a background.

### bool? IsTiled { get; }

Gets whether the image is tiled when displayed.

### IReadOnlyCollection&lt;[IImagePreview](../IImagePreview#iimagepreview)&gt; ScaledImages { get; }

Gets a collections of scaled background images.

### [WebColor](../WebColor#webcolor) TopColor { get; }

Gets the top color of a gradient background.

