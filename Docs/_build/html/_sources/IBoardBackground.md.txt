# IBoardBackground

Represents a background image for a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IBoardBackground

## Properties

### [WebColor](WebColor#webcolor) BottomColor { get; }

Gets the bottom color of a gradient background.

### [BoardBackgroundBrightness](BoardBackgroundBrightness#boardbackgroundbrightness)? Brightness { get; }

Gets the brightness of the background.

### [WebColor](WebColor#webcolor) Color { get; }

Gets the color of a stock solid-color background.

### string Image { get; }

Gets the image of a background.

### bool? IsTiled { get; }

Gets whether the image is tiled when displayed.

### Manatee.Trello.IReadOnlyCollection`1[[Manatee.Trello.IImagePreview, Manatee.Trello, Version=3.0.0.0, Culture=neutral, PublicKeyToken=f502fcc17fc907d6]] ScaledImages { get; }

Gets a collections of scaled background images.

### [WebColor](WebColor#webcolor) TopColor { get; }

Gets the top color of a gradient background.

