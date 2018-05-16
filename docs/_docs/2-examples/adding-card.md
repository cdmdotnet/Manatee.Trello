---
title: Adding a card to a list
category: Examples
order: 3
---

## by getting the list from the board

```csharp
var board = factory.Board();
// We don't care about the board; we just want the lists.  By just refreshing
// the list collection, we reduce the amount of data that must be retrieved.
await board.Lists().Refresh();

// Refreshing the list collection downloaded all of the data for the lists as well.
var backlog = board.Lists.FirstOrDefault(l => l.Name == "Backlog");
var newCard = await backlog.Cards.Add("a new card");
// The card is returned with all of its data, so it doesn't need to be refreshed.
```

## by getting the list using its ID

```csharp
var backlog = factory.List("[some known ID]");
// To add a card, we don't need the list's data; no refresh needed.
var newCard = await backlog.Cards.Add("a new card");
// The card is returned with all of its data, so it doesn't need to be refreshed.
```
