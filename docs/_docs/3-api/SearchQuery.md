---
title: SearchQuery
category: API
order: 239
---

Provides an easy mechanism to build search queries.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- SearchQuery

## Methods

### [ISearchQuery](../ISearchQuery#isearchquery) CreatedWithinDay()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the past 24 hours.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) CreatedWithinDays(int days)

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the past *days* days.

**Parameter:** days

The number of days.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) CreatedWithinMonth()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the past month.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) CreatedWithinWeek()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the past week.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) DueWithinDay()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the next 24 hours.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) DueWithinDays(int days)

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the next *days* hours.

**Parameter:** days

The number of days.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) DueWithinMonth()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the next month.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) DueWithinWeek()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the next week.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) EditedWithinDay()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the past 24 hours.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) EditedWithinDays(int days)

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the past *days* days.

**Parameter:** days

The number of days.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) EditedWithinMonth()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the past month.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) EditedWithinWeek()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items due in the past week.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) IsArchived()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only archived items.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) IsOpen()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only unarchived items.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) IsStarred()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only starred items.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) Label(ILabel label)

Creates a new Manatee.Trello.ISearchQuery specifying a label search parameter.

**Parameter:** label

The label to search for.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) Label(LabelColor labelColor)

Creates a new Manatee.Trello.ISearchQuery specifying a label color search parameter.

**Parameter:** labelColor

The label color to search for.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) Member(IMember member)

Creates a new Manatee.Trello.ISearchQuery specifying a member search parameter.

**Parameter:** member

The member to search for.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) Overdue()

Creates a new Manatee.Trello.ISearchQuery specifying a search parameter to restrict to only items which are overdue.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) Text(string text)

Creates a new Manatee.Trello.ISearchQuery specifying a text search parameter.

**Parameter:** text

The text to search for.

**Returns:** 

### [ISearchQuery](../ISearchQuery#isearchquery) TextInCheckLists(string text)

Creates a new Manatee.Trello.ISearchQuery specifying a text search parameter specific to check lists.

**Parameter:** text

The text to search for.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) TextInComments(string text)

Creates a new Manatee.Trello.ISearchQuery specifying a text search parameter specific to card comments.

**Parameter:** text

The text to search for.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) TextInDescription(string text)

Creates a new Manatee.Trello.ISearchQuery specifying a text search parameter specific to card descriptions.

**Parameter:** text

The text to search for.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### [ISearchQuery](../ISearchQuery#isearchquery) TextInName(string text)

Creates a new Manatee.Trello.ISearchQuery specifying a text search parameter specific to card names.

**Parameter:** text

The text to search for.

**Returns:** A new Manatee.Trello.ISearchQuery parameter list.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

#### Filterpriority

2

