# WebColor

Defines a color in the RGB space.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- WebColor

## Constructors

### WebColor(ushort red, ushort green, ushort blue)

Creates a new instance of the [WebColor](WebColor#webcolor) class.

**Parameter:** red

The red component.

**Parameter:** green

The green component.

**Parameter:** blue

The blue component.

### WebColor(string serialized)

Creates a new isntance of the [WebColor](WebColor#webcolor) class.

**Parameter:** serialized

A string representation of RGB values in the format &quot;#RRGGBB&quot;.

## Properties

### ushort Blue { get; }

Gets the blue component.

### ushort Green { get; }

Gets the green component.

### ushort Red { get; }

Gets the red component.

## Methods

### string ToString()

Returns an HTML-compatible color code.

**Returns:** A string that represents the current object.

