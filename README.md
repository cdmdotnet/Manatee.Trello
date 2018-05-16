# Manatee.Trello

[![Build status](https://ci.appveyor.com/api/projects/status/qdlvb960nc7eik2w/branch/master?svg=true)](https://ci.appveyor.com/project/gregsdennis/manatee-trello/branch/master)
[![MyGet Build Status](https://www.myget.org/BuildSource/Badge/littlecrabsolutions?identifier=b70114b6-4cba-4624-97a9-be505a253c12)](https://www.myget.org/)<br>
[![NuGet version (Manatee.Trello)](https://img.shields.io/nuget/v/Manatee.Trello.svg?style=flat-square)](https://www.nuget.org/packages/Manatee.Trello/)
[![Discuss on Slack](/Resources/Slack_RGB.png)](https://join.slack.com/t/manateeopensource/shared_invite/enQtMzU4MjgzMjgyNzU3LWQ0ODM5ZTVhMTVhODY1Mjk5MTIxMjgxZjI2NWRiZWZkYmExMDM0MDRjNGE4OWRkMjYxMTc1M2ViMTZiYzM0OTI)<br>
<a href="http://www.jetbrains.com/resharper"><img src="http://i61.tinypic.com/15qvwj7.jpg" alt="ReSharper" title="ReSharper"></a>

The primary goal of Manatee.Trello is to provide an intuitive, object-oriented representation of Trello entities.  Other API wrappers that I encounter contain service objects with functions that return little more than non-functional DTOs (data transfer objects) to represent the entities.

The architecture of Manatee.Trello ensures:

- All data is exposed in an intuitive manner
- API calls are minimized
- A safe multithreading experience
- Full configurability

## Setup

Before any data is retrieved, some configuration is required.  It's fairly simple:

    TrelloAuthorization.Default.AppKey = "[your application key]";
    TrelloAuthorization.Default.UserToken = "[your user token]";

This will initialize using the libraries linked above.

## Reading data

Most entities can be accessed directly simply by:

1. calling their constructors and passing the entity's ID.

    ```csharp
    var board = new Board("[your board id]");
    ```

2. calling the corresponding method on the `TrelloFactory` class.

    ```csharp
    ITrelloFactory factory = new TrelloFactory();
    var board = factory.Board("[your board id]");
    ```

> **NOTE** The entity contructors and the factory methods can work with both a long and short ID.  The short IDs for boards and cards are pretty easy to find, too: they're in the URL!  For example, the Trello API Changelog board (https://trello.com/b/dQHqCohZ/trello-platform-changelog) has a short ID of `dQHqCohZ`.  The long IDs can be found in the `Id` property of any of the entities; they're not exposed through the website in any way.

Once you have an entity, you can get any entities related to it through its properties or extension methods.

```csharp
var cards = board.Cards;
```

## Minimizing API calls

Manatee.Trello holds requests to the API until data is actually requested.  This means that with the above code, no calls have yet been made, even though we have created a board and accessed its collection of cards.

It's not until we attempt to read/write data about the board or enumerate the card collection that a request will be made.

For example, let's take a look at the following code snippet:

```csharp
ITrelloFactory factory = new TrelloFactory();
string boardId = "[a board ID]";
var board = factory.List(listId);
await board.Refresh();
var list = board.Lists.First();
var card = list.Cards.Add("new card");
var member = board.Members.First();
card.Members.Add(member);
```

There are three calls being made here:

- Download the board (also downloads members, lists, and a host of other information)
- Add a card to the first list
- Assign a member to the card

In addition to the above optimizations, Manatee.Trello will consolidate multiple rapid changes to the same object into a single call.  So the following code snippet only produces a single call:

```csharp
card.Name = "A New Hope";
card.Description = "The original Star Wars film is still considered by many to be the best of the entire series.";
card.DueDate = DateTime.Now;
```

> **NOTE** The limit here is that this only supports direct changes to the card object itself.  Collections on the card (such as `Checklists` are considered separate objects and additional calls will be made for these changes.

On top of all of this, Manatee.Trello maintains an internal cache (for which you can supply your own implementation, if you choose) that holds every entity that has been downloaded.  Any time one entity references another that has already been cached, the cached entity is used rather than downloading and instantiating a copy.

Lastly, each entity will automatically update itself on-demand, throttled by a configurable timeout.  So, if it's been a while since you checked the name of a card and someone has updated it online, the card will automatically refresh.

Of course, all of this functionality is configurable and completely abstracted from you, the client.

## Additional Features

Features:

- Supports:
    - .Net Framework 4.5
    - .Net Standard 1.3
    - .Net Standard 2.0
- Fully asynchronous implementation
- Full customization of data management (download only what you need)
- Data upload aggregation (set multiple properties on an entity with a single call)
- All collections are LINQ-compatible
- All entities implement interfaces to support unit testing & dependency injection
- Extensible framework to support Trello PowerUps
- CustomFields, Voting, and Card Aging PowerUps built in (must be enabled on the board)
- Supports simultaneous usage of multiple Trello accounts
- Entity caching to avoid unnecessary duplication of entities
- Seams available for providing custom implementations of:
    - Cache
    - REST client
    - JSON serializer
- Webhook integration
- Update notification via .Net events
- Functions:
    - Boards
        - Add/Edit/Delete
        - Add/Edit board-wide preferences & permissions
        - Add/Edit personal preferences
        - Add/Remove members
        - Add/Edit/Delete custom fields
        - Add/Remove to/from organization
        - Enable/Disable power-ups
    - Lists
        - Add/Edit/Reorder
        - Move to board
    - Cards
        - Add/Copy/Edit/Delete/Reorder
        - Add/Edit/Clear custom field data
        - Add/Edit/Remove stickers
        - Add/Edit/Delete comments
        - Add/Copy/Edit/Delete/Reorder checklists
        - Add/Edit/Delete/Reorder checklist items
        - Move to list
    - Organizations (Teams)
        - Add/Edit/Delete
        - Read/Set org-wide preferences & permissions
        - Add/Remove members
        - Add/Edit/Clear custom field data
    - Members
        - Read public member data
        - Read/Update authenticated member data
        - Read notifications for authenticated member
    - PowerUps
        - Provided base class
    - Searches
        - General search
        - Member search
        - Refresh to rerun query
    - Tokens
        - Read/Delete
    - Webhooks
        - Add/Edit/Delete

See the [docs folder](https://github.com/gregsdennis/Manatee.Trello/blob/master/docs) for more information on how to use this wonderful library!

## Extended use

As of version 3, a [licensing model](https://github.com/gregsdennis/Manatee.Trello/blob/master/docs/7.%20Licensing.md) has been introduced.

If you're planning on using this for your organization, please consider purchasing a license.

## Contributing

If you have questions, experience problems, or feature ideas, please create an issue.

If you'd like to help out with the code, please feel free to fork and create a pull request.

### The Project

This code uses C# 7 features, so a compiler/IDE that supports these features is required.

The solution consists of a multi-targeted project and .Net Framework 4.6.2 test projects.

### Building

During development, building within Visual Studio should be fine.

To run the tests, you'll need to store your user token in an environment variable called `TRELLO_USER_TOKEN`.

### Code style and maintenance

I use [Jetbrains Resharper](https://www.jetbrains.com/resharper/) in Visual Studio to maintain the code style (and for many of the other things that it does).  The solution is set up with team style settings, so if you're using Resharper the settings should automatically load.  Please follow the suggestions.
