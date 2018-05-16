---
title: Getting Started
category: Version 2.x
order: 1
---

# Configuration

Before entities can be created, Manatee.Trello must be configured using the `TrelloConfiguration`, `TrelloAuthorization`, and `TrelloProcessor` classes.  To do this, several components will be required:

- Application Key & User Token - These identify the application and provide access to Trello data.  The user token is not explicitly required, but without it you will only gain read-only access to publicly available information.
- JSON Serializer/Deserializer/Factory - The serializer & deserializer facilitate object data translation between Trello's servers and Manatee.Trello.  The factory allows Manatee.Trello to create JSON objects internally.  More information can be found on the [Json Serializer](Json) page.
- REST Provider - This facilitates communication between Trello's servers and Manatee.Trello.  More information can be found on the [Rest Provider](Rest) page.

The `TrelloConfiguration` static class also exposes several other configuration options.  These can be found on the [Configuration](Configuration) page.

The code below gives an example of configuring Manatee.Trello.

    var serializer = new ManateeSerializer();
    TrelloConfiguration.Serializer = serializer;
    TrelloConfiguration.Deserializer = serializer;
    TrelloConfiguration.JsonFactory = new ManateeFactory();
    TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
    TrelloAuthorization.Default.AppKey = "[your application key]";
    TrelloAuthorization.Default.UserToken = "[your user token]";

# Reading data

The Trello API requires all users to supply an application key (AppKey) to access any data.  Supplying only an AppKey enables read-only access publicly-visible data.

For example, to find all cards which exist on a public board, simply instantiate a `Board` object by supplying the board's ID and access the `Cards` property.

    TrelloAuthorization.Default.AppKey = "[your application key]";
    var board = new Board("[your board id]");
    var cards = board.Cards;

> **Tip:** You can filter most collection types using the `Filter()` extension method.  For example, you can use `board.Cards.Filter(CardFilter.Closed)` to get all archived cards for the board.  For consistency, the extension method will return a new collection object.

It should be noted that no API calls have yet been made.  It's not until we actually enumerate the cards that the data is accessed.  If we had instead accessed one of the non-collection properties on the board, a call would have been made to download the board's data.  This would be a separate call than the one used to download the cards.  So if we were to print out the board's name and all of the names of the cards, a total of two calls will be made.

    TrelloAuthorization.Default.AppKey = "[your application key]";
    var boardId = "[your board id]";
    var board = new Board(boardId);
    Console.Writeline(board);
    foreach(var card in board.Cards)
    {
        Console.Writeline(card);
    }

> **NOTE:** Some of the objects on Trello (most notably cards) have a `shortLink` ID.  This is visible in the URL when viewing a card in a browser.  These IDs can be passed into the constructors of Manatee.Trello objects.  However a call will be made when accessing the `Id` property on these object.  This call is skipped when accessing the `Id` property for an object which has been instantiated using the full 24-character ID.

# Writing data

Writing (changing data, adding cards, etc.) is enabled by additionally supplying a user token (UserToken).  When a UserToken is supplied, access is granted based on the user who supplied the UserToken.

> **NOTE** When a UserToken is created, various levels of access are granted.  It may be that the specific UserToken was created with read-only access.  But even in these cases, you can still read the member's private data.

The following code adds a card to a list then assigns the first member of the board to the card.

    TrelloAuthorization.Default.AppKey = "[your application key]";
    TrelloAuthorization.Default.UserToken = "[your user token]";
    string listId = "[a list ID]";
    var list = new List(listId);
    var member = list.Board.Members.First();
    var card = list.Cards.Add("new card");
    card.Members.Add(member);

There are a total of four calls being made here:

- Downloading the list to access the board.
- Downloading the members on the board.
- Adding a card to the list.
- Assigning a the member to the card.

# Searching for entities

Trello has the ability to search any board, organization, or card for certain text.  This ability is exposed through the `Search` object.  With this object, you can specify the search query, the entities in which to search, which types of entities to return, and the maximum number of each type of entity to return.

The following code searches a specified board for a maximum of 100 cards which contain the text "trello" and outputs the number of cards.

    string boardId = "[a board ID]";
    string query = "trello";
    var board = new Board(boardId);
    var results = new Search(query, 100, SearchModelType.Cards, new IQueryable[] {board});
    Console.Writeline(results.Cards.Count());

> **NOTE:**  All collection objects implement IEnumerable<T>, so the `Count()` method is the `Enumerable.Count()` method defined in System.Linq.

While the context list can contain any object, only the first 24 each of boards, organizations, and cards will be included in the search.  This is a limitation from Trello, not this wrapper.  Also, the `SearchModelType` values may be combined using the bit-wise OR operator '`|`'.

You can also build queries using the `SearchFor` class.  This class contains a number of static methods which will produce an instance specifying what you want to search for.  There are also extension methods which allow you to append search criteria.  For example, the following code will search for:

- Cards
- On a specific board
- Assigned to `Member.Me`
- Containing the text `"trello"` in a comment, and
- Due in the next week

<p/>

    string boardId = "[a board ID]";
    var query = SearchFor.Member(Member.Me)
                         .AndTextInComments("trello")
                         .AndDueWithinWeek();
    var board = new Board(boardId);
    var results = new Search(query, 100, SearchModelType.Cards, new IQueryable[] {board});
    Console.Writeline(results.Cards.Count());

> **NOTE:**  All of the methods available on `SearchFor` are also created as extension methods which allow the chaining above.  The extension methods all start with `And`.

This query produces the string `"@me comment:trello due:week"`.  See [Trello's blog post](http://help.trello.com/customer/portal/articles/1145462-searching-for-cards-all-boards-) for more information on search parameters.  All of these have been implemented.

# More on authorization

The AppKey and UserToken must be supplied by existing users of Trello.  All required information regarding obtaining these values may be found on [Trello's developer documentation pages](http://developers.trello.com/authorize).  In short:

- The AppKey is tied to a specific user (can be a service account created for the application).
- The AppKey is required and will grant read access to all data which is visible to the user who issued it.
- The UserToken is optional and will grant access as approved by the user who issued it.

> **Note:**  A UserToken is a string, whereas the `Token` object will provide information on a UserToken, including the name of the application that requested it and the member who created it.

*New functionality as of v1.3.0*

Manatee.Trello can use multiple authorizations simultaneously.  This can be extremely useful when bridging two systems or user profiles.  By using multiple authorizations, you can be consistent between the two systems (the correct users make cards or add comments, etc.).

To enable this, simply create a new instance of teh `TrelloAuthorization` class and pass it into the constructor of your entity.  The authorization will be retained throughout the lifetime of that entity.

    var auth = new TrelloAuthorization
    {
        AppKey = "[your application key]",
        UserToken = "[your user token]"
    }
    var card = new Card("[your card i]", auth);

This authorization parameter is optional.  The entity will use `TrelloAuthorization.Default` in its absence.

# Error handling

Manatee.Trello will pass on any errors returned by Trello's API wrapped in a `TrelloInteractionException`.

For entities which feature a Delete() method, once Delete() has been called that entity essentially becomes "dead."  That is, all of the data which has been already downloaded will remain available however updates will have no effect.

# Request Processing

Prior to v1.9.0, Manatee.Trello maintained a dedicated thread on which all API requests would be queued, then each request would be fulfilled in turn.  This had the effect of blocking *all* threads waiting for responses.

As of v1.9.0, Manatee.Trello supports sending API requests concurrently, with some limitations.  Each thread that needs a request fulfilled will operate in a synchronous manner.  So while a single thread may be blocked temporarily while it is waiting for a response from the server, multiple threads are processed in parallel.

The number of concurrent requests that can be processed is controlled by the `TrelloProcessor.ConcurrentCallCount` static property.  This property has a default of 1, which will result in similar behavior to that of the pre-v1.9.0 libraries.  Setting this to a higher value will allow more calls to be made concurrently, effectively throttled at this number.

Because Manatee.Trello has no way of keeping the application alive if there are pending requests (threads will be terminated when the application closes), `TrelloProcessor` also exposes the `Flush()` static method.  This should be used at the end of the appliation to ensure that all requests are fulfilled.  This method may also be called at any time if the client sees a need.

Also, as of v1.11.0, there is also a `TrelloProcessor.CancelPendingRequests()` method which will stub responses for any calls still pending, effectively cancelling them.
