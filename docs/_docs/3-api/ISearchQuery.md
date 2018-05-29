---
title: ISearchQuery
category: API
order: 106
---

# ISearchQuery

Builds a search query.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ISearchQuery

## Methods

### [ISearchQuery](ISearchQuery#isearchquery) CreatedWithinDay()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past 24 hours.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) CreatedWithinDays(int days)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past *days* days.

**Parameter:** days

The number of days.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) CreatedWithinMonth()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past month.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) CreatedWithinWeek()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past week.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) DueWithinDay()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the next 24 hours.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) DueWithinDays(int days)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the next *days* hours.

**Parameter:** days

The number of days.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) DueWithinMonth()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the next month.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) DueWithinWeek()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the next week.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) EditedWithinDay()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past 24 hours.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) EditedWithinDays(int days)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past *days* days.

**Parameter:** days

The number of days.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) EditedWithinMonth()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past month.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) EditedWithinWeek()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items due in the past week.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) IsArchived()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only archived items.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) IsOpen()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only unarchived items.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) IsStarred()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only starred items.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) Label(ILabel label)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a label search parameter.

**Parameter:** label

The label to search for.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) Label(LabelColor labelColor)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a label color search parameter.

**Parameter:** labelColor

The label color to search for.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) Member(IMember member)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a member search parameter.

**Parameter:** member

The member to search for.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) Overdue()

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a search parameter to restrict to only items which are overdue.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) Text(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter.

**Parameter:** text

The text to search for.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) TextInCheckLists(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter specific to check lists.

**Parameter:** text

The text to search for.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) TextInComments(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter specific to card comments.

**Parameter:** text

The text to search for.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) TextInDescription(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter specific to card descriptions.

**Parameter:** text

The text to search for.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

### [ISearchQuery](ISearchQuery#isearchquery) TextInName(string text)

Creates a new [ISearchQuery](ISearchQuery#isearchquery) specifying a text search parameter specific to card names.

**Parameter:** text

The text to search for.

**Returns:** The [ISearchQuery](ISearchQuery#isearchquery) instance.

