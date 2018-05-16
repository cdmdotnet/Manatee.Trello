---
title: Searching
category: Version 2.x - API
order: 13
---

# Search

Performs a search.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Search

## Properties

### IEnumerable&lt;Action&gt; Actions { get; }

Gets the collection of actions returned by the search.

### IEnumerable&lt;Board&gt; Boards { get; }

Gets the collection of boards returned by the search.

### IEnumerable&lt;Card&gt; Cards { get; }

Gets the collection of cards returned by the search.

### IEnumerable&lt;Member&gt; Members { get; }

Gets the collection of members returned by the search.

### IEnumerable&lt;Organization&gt; Organizations { get; }

Gets the collection of organizations returned by the search.

### string Query { get; }

Gets the query.

## Methods

### void Refresh()

Marks the search to be refreshed the next time data is accessed.

# SearchFor

Provides an easy mechanism to build search queries.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- SearchFor

## Methods

### static [SearchFor](/API-Searching#searchfor) CreatedWithinDay()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the past 24 hours.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) CreatedWithinDays(int days)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the past *days* days.

**Parameter:** days

The number of days.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) CreatedWithinMonth()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the past month.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) CreatedWithinWeek()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the past week.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) DueWithinDay()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the next 24 hours.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) DueWithinDays(int days)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the next *days* hours.

**Parameter:** days

The number of days.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) DueWithinMonth()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the next month.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) DueWithinWeek()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the next week.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) EditedWithinDay()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the past 24 hours.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) EditedWithinDays(int days)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the past *days* days.

**Parameter:** days

The number of days.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) EditedWithinMonth()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the past month.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) EditedWithinWeek()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items due in the past week.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) IsArchived()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only archived items.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) IsOpen()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only unarchived items.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) IsStarred()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only starred items.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) Label(Label label)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a label search parameter.

**Parameter:** label

The label to search for.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) Label(LabelColor labelColor)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a label color search parameter.

**Parameter:** labelColor

The label color to search for.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) Member(Member member)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a member search parameter.

**Parameter:** member

The member to search for.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) Overdue()

Creates a new [SearchFor](/API-Searching#searchfor) specifying a search parameter to restrict to only items which are overdue.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) Text(string text)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a text search parameter.

**Parameter:** text

The text to search for.

**Returns:** 

### static [SearchFor](/API-Searching#searchfor) TextInCheckLists(string text)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a text search parameter specific to check lists.

**Parameter:** text

The text to search for.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) TextInComments(string text)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a text search parameter specific to card comments.

**Parameter:** text

The text to search for.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) TextInDescription(string text)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a text search parameter specific to card descriptions.

**Parameter:** text

The text to search for.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### static [SearchFor](/API-Searching#searchfor) TextInName(string text)

Creates a new [SearchFor](/API-Searching#searchfor) specifying a text search parameter specific to card names.

**Parameter:** text

The text to search for.

**Returns:** A new [SearchFor](/API-Searching#searchfor) parameter list.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

#### Filterpriority

2

# SearchModelType

Enumerates the model types for which one can search.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ValueType
- Enum
- SearchModelType

## Fields

### Actions

Indicates the search should return actions.

### Boards

Indicates the search should return boards.

### Cards

Indicates the search should return cards.

### Members

Indicates the search should return members.

### Organizations

Indicates the search should return organizations.

### All

Indicates the search should return all model types.

