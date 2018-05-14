# Label

A label.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Label

## Properties

### static Manatee.Trello.Label+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [Board](API-Boards#board) Board { get; }

Gets the board on which the label is defined.

### LabelColor? Color { get; set; }

Gets and sets the color. Use null for no color.

### DateTime CreationDate { get; }

Gets the creation date of the label.

### string Id { get; }

Gets the label&#39;s ID.

### string Name { get; set; }

Gets and sets the label&#39;s name.

### int? Uses { get; }

Gets the number of cards which use this label.

## Methods

### void Delete()

Permanently deletes the label and all of its usages from Trello.

#### Remarks

This instance will remain in memory and all properties will remain accessible. Any cards that have the label assigned will update as normal.

### void Refresh()

Marks the label to be refreshed the next time data is accessed.

### string ToString()

Returns the [Name](API-Labels#string-name--get-set-) and [Color](API-Labels#labelcolor-color--get-set-).

**Returns:** A string that represents the current object.

# BoardLabelCollection

A collection of labels for boards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Label&gt;
- BoardLabelCollection

# CardLabelCollection

A collection of labels for cards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;Label&gt;
- CardLabelCollection

## Methods

### void Add(Label label)

Adds an existing label to the card.

**Parameter:** label

The label to add.

### void Remove(Label label)

Removes a label from the collection.

**Parameter:** label

The label to add.

