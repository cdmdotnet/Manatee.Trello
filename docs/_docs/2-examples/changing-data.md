---
title: Changing the data that's downloaded
category: Examples
order: 3
---

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

