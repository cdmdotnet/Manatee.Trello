---
title: Getting Started
category:
order: 1
---

## Download the package

The first thing to do is download and install the Nuget package.

This can be performed from the Nuget Package Manager command line or through the Visual Studio UI.

```
> Install-Package Manatee.Trello
```

## Configuration

There is very little configuration that is required to get Manatee.Trello going out of the box.

> **NOTE** For users of Manatee.Trello versions 1 or 2, the REST provider and JSON serializer have been integrated into the main Nuget package and are initialized as the default services.  These no longer need to be configured.

The only values that must be provided are for authorization, which you can read about [later in this document](#authorization).

Additional configuration is discussed on the [Configuration page](../configuration).

## Reading data

In Manatee.Trello, entities can be created using one of two methods:

- via the factory

    ```csharp
    var factory = new TrelloFactory();
    var board = factory.Board("<board ID>")
    ```
- via a constructor

    ```csharp
    var board = new Board("<board ID>");
    ```

The factory returns interfaces and is ideal for dependency injection scenarios and for supporting unit testability.

Before any of the data is available, the `Refresh()` method must be called.  This is an asynchronous method that should be awaited.

> **NOTE** In previous versions, the data would be automatically downloaded, which was a synchronous and blocking process.  The approach was abandoned to support an async/await model as well as to give the client more control over when data is downloaded.  The only exception to this is the `Id` property on boards and cards (these can be instantiated via shorter IDs, and the system will block to resolve the full ID).

If you don't need any of the data from the board itself, but you just want the collection of lists, you can call `Refresh()` directly on the `Lists` collection.  Otherwise, by default, the lists will be downloaded as part of the board refresh as well.  While this results in rather large data transfers per call, it reduces the number of required calls greatly which provides an overall performance increase.

### Customizing the download

Most of the entities define a flag-enabled `Fields` enumeration and a `DownloadedFields` static property.  This property determines what data is downloaded when the `Refresh()` method is called.

For example, if you don't care about downloading the actions or the URL for a board, you can use the following to disable download for these properties:

```csharp
Board.DownloadedFields &= ~Board.Fields.Actions & ~Board.Fields.Url;
```

To re-enable these fields for download, simply invert the statement:

```csharp
Board.DownloadedFields |= Board.Fields.Actions | Board.Fields.Url;
```

By default, all of the data is downloaded for all entities.  The exceptions are:

- Boards don't download members (in favor of memberships) or cards (in favor of getting cards when refreshing lists)
- Organizations don't download members (in favor of memberships)

If an entity has already been downloaded and cached, and new data for it is retrieved as part of an unrelated refresh (e.g. refreshing a card may also download data for the list which contains it), the new data will be merged into the existing entity.  Manatee.Trello attempts to use *all* of the data provided by the API to ensure that information is as up-to-date as possible.

## Writing data

Sending data back to Trello is quite easy.

Many edits are updating fields, such as the description on a card or the name of a board.  For tasks like this, you only need to set the property.  This will trigger an asynchronous process to submit the new data to Trello.

When a property is set, a timer starts, the duration of which is determined by `TrelloConfiguration.ChangeSubmissionTime`.  Once the timer expires, it sends the change.  But there's a bonus: if another change for the same entity is made (i.e. setting another property), the timer is reset.  When the timer expires, *all* changes are sent as a single request.  If you need to, you can wait for property changes to be sent to Trello by calling `await TrelloProcessor.Flush()`.  This will ensure that the updates are sent before continuing.

Other writes are generally deletions or creations.

When you need to delete an entity, simply call its `Delete()` method.  Done.

Creating entities can be accomplished via methods on various collections.  For instance, to add a card to a list, do this:

```csharp
var newCard = await list.Cards.Add("a new card");
```

There are usually overloads for the `Add()` methods.  See the API documentation pages of this wiki for more information on what calls are available.

### A note about positioning

Several of the entities, like cards and lists, can be sorted or otherwise have their order changed.  This is controlled by the `Position` property.

Trello records an entity's position using a floating point number system.  If the user moves card **A** between cards **B** and **C**, Trello takes the numeric position values of **B** and **C**, averages them, and assigns that value to card **A**.

## Searching for entities

Trello has the ability to search any board, organization, or card for certain text.  This ability is exposed through the `Search` object.  With this object, you can specify the search query, the entities in which to search, which types of entities to return, and the maximum number of each type of entity to return.

The following code searches a specified board for a maximum of 100 cards which contain the text "trello" and outputs the number of cards.

```csharp
string boardId = "[a board ID]";
string query = "trello";
var board = factory.Board(boardId);
var search = factory.Search(query, 100, SearchModelType.Cards, new IQueryable[] {board});

await search.Refresh();

Console.Writeline(search.Cards.Count());
```

> **NOTE**  All collection objects implement `IEnumerable<T>`, so the `Count()` method is the `Enumerable.Count()` method defined in System.Linq.

While the context list can contain any object, only the first 24 each of boards, organizations, and cards will be included in the search.  This is a limitation from Trello, not this wrapper.  Also, the `SearchModelType` values may be combined using the bit-wise OR operator `|`.

You can also build queries using the `SearchQuery` class.  This class contains a number of static methods which will produce an instance specifying what you want to search for.  There are also extension methods which allow you to append search criteria.  For example, the following code will search for:

- Cards
- On a specific board
- Assigned to `Member.Me`
- Containing the text "trello" in a comment, and
- Due in the next week

```csharp
string boardId = "[a board ID]";
var query = new SearchQuery().Member(Member.Me)
                             .AndTextInComments("trello")
                             .AndDueWithinWeek();
var board = new Board(boardId);
var results = new Search(query, 100, SearchModelType.Cards, new IQueryable[] {board});
Console.Writeline(results.Cards.Count());
```

This query is equivalent to the string `"@me comment:trello due:week"`.  See [Trello's blog post](http://help.trello.com/customer/portal/articles/1145462-searching-for-cards-all-boards-) for more information on search parameters.  All of the special parameters listed in this post have been implemented.

> **NOTE** When creating entities, it will take some time for the new items to be indexed on the Trello servers.  During this time, these entities will not be returned in searches.  This may also affect searching for entities that have recently changed.

## Authorization

An *application key* and *authentication token* are string values that enable Trello to recognize who is requesting access and through which application they are doing so.  All required information regarding obtaining these values may be found on [Trello's developer documentation pages](https://developers.trello.com/page/authorization).

### Application key

The application key is stored in `TrelloAuthorization.AppKey`.  This value is tied to a specific user.  Trello advises that this should be a service account created specifically for the application.

Alone, the application key grants read access to any publicly-readable data.  To enable write access or access to protected data, an authentication token must be provided as well.

### Authentication token

The authentication token is store in `TrelloAuthorization.UserToken` and will grant access as approved by the user who issued it.  Users have the power to grant read, write, and admin privileges to a token for a specific duration or indefinitely.

> **NOTE**  A authentication is a string, whereas the `Token` object will provide information on an authentication token, including the name of the application that requested it and the member who created it.

Manatee.Trello can use multiple authorizations simultaneously.  This can be extremely useful when bridging two systems or user profiles.  By using multiple authorizations, you can be consistent between the two systems (the correct users make cards or add comments, etc.).

To enable this, simply create a new instance of the `TrelloAuthorization` class and pass it into the factory method for your entity.  The authorization will be retained throughout the lifetime of that entity.

```csharp
var auth = new TrelloAuthorization
{
    AppKey = "[your application key]",
    UserToken = "[your user token]"
}
var card = new Card("[your card ID]", auth);
```

This authorization parameter is optional.  The entity will use `TrelloAuthorization.Default` in its absence.

## Error handling

Manatee.Trello will pass on any errors returned by Trello's API wrapped in a `TrelloInteractionException`.

For entities which feature a `Delete()` method, once `Delete()` has been called that entity essentially becomes "dead."  That is, all of the data which has been already downloaded will remain available however updates will have no effect, and the entity will be permanently removed from Trello's servers.
