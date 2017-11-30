# Manatee.Trello

[![Gitter](https://badges.gitter.im/gregsdennis/Manatee.Trello.svg)](https://gitter.im/gregsdennis/Manatee.Trello?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge) <a href="http://www.jetbrains.com/resharper"><img src="http://i61.tinypic.com/15qvwj7.jpg" alt="ReSharper" title="ReSharper"></a>

The primary goal of Manatee.Trello is to provide an intuitive, object-oriented representation of Trello entities.  Other API wrappers that I encounter contain service objects with functions that return little more than non-functional DTOs (data transfer objects) to represent the entities.

The architecture of Manatee.Trello ensures:

- All data is exposed in an intuitive manner
- API calls are minimized
- A safe multithreading experience

The main components of Manatee.Trello are:

- The base library ([Manatee.Trello](https://www.nuget.org/packages/Manatee.Trello/))
- A JSON parser ([Manatee.Trello.ManateeJson](https://www.nuget.org/packages/Manatee.Trello.ManateeJson/))
- A REST client ([Manatee.Trello.WebApi](https://www.nuget.org/packages/Manatee.Trello.WebApi/))

There are also a series of packages that implement Trello PowerUps:

- Custom fields ([Manatee.Trello.CustomFields](http://www.nuget.org/packages/Manatee.Trello.CustomFields/))
- more to come...

The parser and the client are merely implementations of interfaces defined in the main library.  If you want to use different providers for this functionality, you can provide your own implementations during setup!

## Setup

Before any data is retrieved, some configuration is required.  It's fairly simple:

    var serializer = new ManateeSerializer();
    TrelloConfiguration.Serializer = serializer;
    TrelloConfiguration.Deserializer = serializer;
    TrelloConfiguration.JsonFactory = new ManateeFactory();
    TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
    TrelloAuthorization.Default.AppKey = "[your application key]";
    TrelloAuthorization.Default.UserToken = "[your user token]";

This will initialize using the libraries linked above.

## Reading data

Most entities can be accessed directly simply by calling their constructors and passing the entity's ID.

    var board = new Board("[your board id]");

>**NOTE** These contructors can work with both a long and short ID.  The short IDs for boards and cards are pretty easy to find, too: they're in the URL!  For example, the Trello API Dev board ( https://trello.com/b/cI66RoQS/trello-public-api) has a short ID of `cI66RoQS`.  The long IDs can be found in the `Id` property of any of the entities; they're not exposed through the website in any way.

Once you have an entity, you can get any entities related to it through its properties or extension methods.

    var cards = board.Cards;

## Minimizing API calls

Manatee.Trello holds requests to the API until data is actually requested.  This means that with the above code, no calls have yet been made, even though we have created a board and a collection of cards.

It's not until we attempt to read/write data about the board or enumerate the card collection that a request will be made.

For example, let's take a look at the following code snippet:

    string listId = "[a list ID]";
    var list = new List(listId);
    var member = list.Board.Members.First();
    var card = list.Cards.Add("new card");
    card.Members.Add(member);

There are four calls being made here:

- Download the list to get the board's ID
- Download the members of the board
- Add a card to the list
- Assign a member to the card

Note that the board details are never downloaded, only its ID which is included as part of the list (`list.Board`).  The `Board.Members` simply creates a collection object that points to the members of the board with this ID.  When we call `Members.First()`, the collection is enumerated triggering another call.

In addition to the above optimizations, Manatee.Trello will consolidate multiple rapid changes to the same object into a single call.  So the following code snippet only produces a single call:

    card.Name = "A New Hope";
    card.Description = "The original Star Wars film is still considered by many to be the best of the entire series.";
    card.DueDate = DateTime.Now;

>**NOTE** The limit here is that this only supports direct changes to the card object itself.  Collections on the card (such as `Checklists` are considered separate objects and additional calls will be made for these changes.

On top of all of this, Manatee.Trello maintains an internal cache (for which you can supply your own implementation, if you choose) that holds every entity that has been downloaded.  Any time one entity references another that has already been cached, the cached entity is used rather than downloading and instantiating a copy.

Lastly, each entity will automatically update itself on-demand, throttled by a configurable timeout.  So, if it's been a while since you checked the name of a card and someone has updated it online, the card will automatically refresh.

Of course, all of this functionality is configurable and completely abstracted from you, the client.

See the wiki pages for more information on how to use this wonderful library!

## Contributing

If you like this library and would like to contribute, feel free to fork it and create a pull request.

If you experience any problems or just have an idea to improve, please create an issue.
