# Changing the data that's downloaded

All of an entity's data and most of its child data is downloaded when refreshed.  For boards, this can include lists, cards, members, etc.  The data that is downloaded is managed by the `DownloadedFields` static property on the entity class.

```csharp
// We just want members (the users), but not memberships (permissions on the board),
// but the default is to get the memberships, not the members.  (Memberships include
// the member, but this is example code, so...)
Board.DownloadedFields &= ~Board.Fields.Memberships;
Board.DownloadedFields |= Board.Fields.Members;

var board = factory.Board("[some known ID]");
await board.Refresh();

// board.Memberships is empty, but board.Members has data!
foreach(var member in board.Members)
{
    Console.WriteLine(member); // prints member.FullName
}
```

For boards, specifically, you can download the board and all of its nested data as well in a single call, just like the Trello web UI.

```csharp
// The board.Cards and board.Lists properties are separate, unrelated collections, 
// and Trello only supports nested entities one level deep, so even when we download
// the lists, they're empty; we don't get the cards.  So we enable download of the cards.
Board.DownloadedFields |= Board.Fields.Cards;
// Now we have the cards, but they're in a flat collection, unrelated to the lists.
// The line below will turn on functionality to ensure that the cards are automatically
// added to the appropriate lists as they're downloaded.
TrelloConfiguration.EnableConsistencyProcessing = true;

var board = factory.Board("[some known ID]");
await board.Refresh();

foreach(var list in board.Lists)
{
    Console.WriteLine(list);
    foreach(var card in list.Cards)
    {
        Console.WriteLine($"- {card}");
    }
}
```