# IJsonLabel

Defines the JSON structure for the Label object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonLabel

## Properties

### [IJsonBoard](IJsonBoard#ijsonboard) Board { get; set; }

Gets and sets the board on which the label is defined.

### [LabelColor](LabelColor#labelcolor)? Color { get; set; }

Gets and sets the color of the label.

### bool ForceNullColor { set; }

Determines if the color property should be submitted even if it is null.

#### Remarks

This property is not part of the JSON structure.

### string Name { get; set; }

Gets and sets the name of the label.

### int? Uses { get; set; }

Gets and sets how many cards use this label.

