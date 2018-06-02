# Getting all of the boards for the owner of the token

```csharp
// TrelloFactory.Me() is one of two awaitable factory methods.  This is to get the member ID.
// Entities created from other methods will need to be refreshed before data can be accessed.
var me = await factory.Me();

foreach(var board in me.Boards)
{
    Console.WriteLine(board); // prints board.Name
}
```
