# SearchQuery

Provides an easy mechanism to build search queries.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- SearchQuery

## Methods

### [ISearchQuery](ISearchQuery#isearchquery) CreatedWithinDay()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past 24 hours.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) CreatedWithinDays(int days)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past *days* days.

**Parameter:** days

The number of days.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) CreatedWithinMonth()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past month.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) CreatedWithinWeek()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past week.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) DueWithinDay()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the next 24 hours.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) DueWithinDays(int days)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the next *days* hours.

**Parameter:** days

The number of days.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) DueWithinMonth()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the next month.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) DueWithinWeek()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the next week.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) EditedWithinDay()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past 24 hours.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) EditedWithinDays(int days)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past *days* days.

**Parameter:** days

The number of days.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) EditedWithinMonth()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past month.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) EditedWithinWeek()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past week.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) IsArchived()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only archived items.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) IsOpen()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only unarchived items.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) IsStarred()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only starred items.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) Label(ILabel label)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a label search parameter.

**Parameter:** label

The label to search for.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) Label(LabelColor labelColor)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a label color search parameter.

**Parameter:** labelColor

The label color to search for.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) Member(IMember member)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a member search parameter.

**Parameter:** member

The member to search for.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) Overdue()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items which are overdue.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) Text(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter.

**Parameter:** text

The text to search for.

**Returns:** 

### [ISearchQuery](ISearchQuery#isearchquery) TextInCheckLists(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter specific to check lists.

**Parameter:** text

The text to search for.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) TextInComments(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter specific to card comments.

**Parameter:** text

The text to search for.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) TextInDescription(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter specific to card descriptions.

**Parameter:** text

The text to search for.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### [ISearchQuery](ISearchQuery#isearchquery) TextInName(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter specific to card names.

**Parameter:** text

The text to search for.

**Returns:** A new [ISearchQuery](ISearchQuery#isearchquery) parameter list.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

#### Filterpriority

2

