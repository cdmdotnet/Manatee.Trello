---
title: Deleting a check item
category: Examples
order: 5
---

```csharp
var card = factory.Card("[some known ID]");
// Check items are a special case for refreshing in that they download when refreshing
// the card.  No other collections download two levels deep like this.
await card.Refresh();
var checkList = card.CheckLists[0];
// Many collections allow indexing by a string.  The properties that this matches
// on varies by collection.  Here's we're indexing by the check item's name.
var checkItem = checkList.CheckItems["Bake cookies"];

await checkItem.Delete();
```

