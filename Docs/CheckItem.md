# CheckItem

Represents a checklist item.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- CheckItem

## Properties

### static Manatee.Trello.CheckItem+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [ICheckList](ICheckList#ichecklist) CheckList { get; set; }

Gets or sets the checklist to which the item belongs.

### DateTime CreationDate { get; }

Gets the creation date of the checklist item.

### string Id { get; }

Gets or sets the checklist item&#39;s ID.

### string Name { get; set; }

Gets or sets the checklist item&#39;s name.

### [Position](Position#position) Position { get; set; }

Gets or sets the checklist item&#39;s position.

### [CheckItemState](CheckItemState#checkitemstate)? State { get; set; }

Gets or sets the checklist item&#39;s state.

## Events

### Action&lt;ICheckItem, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the checklist item is updated.

## Methods

### Task Delete(CancellationToken ct)

Deletes the checklist item.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the checklist item from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct)

Refreshes the checklist item data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

#### Filterpriority

2

