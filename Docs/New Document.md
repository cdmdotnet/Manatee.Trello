###INTRODUCTION

Manatee.Trello is a wrapper for [Trello’s RESTful API](https://trello.com/docs/api/index.html) written C#.NET.  The goal of this library is to expose the functionality of Trello as a intuitive, fully object-oriented design which implements a _Lazy Initialization_ design pattern; that is, no object is loaded until its details have been requested.

Every object retrieved through the TrelloService object possesses the properties one would expect to find by simply using the [Trello web UI](http://trello.com).  For example, a Board has a collection of Lists, each of which has a collection of Cards.  The Cards also reference back to the List and Board in which they are contained.  There is usage of raw IDs and manually making separate calls to retrieve related objects.  The service object also caches all of the objects it returns so that memory is not wasted on multiple references to a single object.

Furthermore, each object is self-updating with a globally configurable expiration, which defaults to 60 seconds.  To minimize calls to the RESTful API, each object only pulls updates if it has expired, and then only when it is accessed.  For example, if a Member object is retrieved from the service object, and its Boards collection is never accessed, no details for those boards are ever downloaded.  Writes to object properties also push those changes to the website immediately.

Finally, all collections returned by the API update automatically and are fully LINQ-compatible, enabling some of the functionality not directly exposed by the wrapper.

###HISTORY

I am a software support developer, which means that when other people’s code does something it’s not supposed to, I’m on the team that determines where the bug is and fixes it.  As such we have a plethora of systems dedicated to managing our list of found problems.

The business users (my bosses) wanted a simple report to show the statuses of all of the problems we were working on as well as what was in our backlog and what we were planning on tackling next.  Enter Trello.  Following the example of Trello's own [Dev Board](https://trello.com/dev), I created one for my team so that my business users could simply load up our board and see the information in an easy-to-read manner.

After about a month of manually updating cards in both our problem management systems and on Trello, I decided it was time to investigate options for pulling data from our systems and uploading automatically to our board.  I found that while Trello exposed a rich API to manipulate the site programmatically, the existing .Net wrappers were insufficient for my need.  The existing wrappers only exposed a service-based architecture, which didn’t seem very object-oriented, and it was very obvious that they were mere wrappers around function calls provided by Trello’s development team.  I wanted objects with properties which I could manipulate, and I didn’t want to see any hint of the RESTful API.

Thus, Manatee.Trello was conceived.
 
###ARCHITECTURE

If one were to use the Trello web interface, they would find that everything is represented as objects which contain and/or reference other objects:  Boards contain a collection of Lists, each of which contains a collection of cards, etc.  These concepts are the basis for the architecture behind Manatee.Trello.

There are nine basic object types:

- Member (Trello user)
- Organization
- Board
- List
- Card
- CheckList
- CheckItem
- Action
- Notification

The relationships between these object types are shown by the figure below.

![](OrgChart.emf)

This object graph is replicated in .Net by their respective entity classes.  Each entity class contains properties for those values which can be individually set.  For example, a Card’s description is an editable property because it is a value which can be set without having to provide additional information.

Other values on the entity class are edited via methods because they are either part of collections (all collections are exposed as IEnumerable&lt;T&gt;) or Trello does not allow the value to be edited (e.g. the Url property on the Board entity is controlled by Trello).

###USAGE

Whether reading or writing, the first task one must do is instantiate an instance of the TrelloService.  Through this service object, one can retrieve any of the entity types listed above with the exception of CheckItem.  Once an entity is retrieved, one can manipulate it at will or navigate to any related item through the properties exposed by that type.  The specifics of what each object exposes is in the [API Reference (under construction)]().

####Reading data

The Trello API requires all users to supply an authorization key (AuthKey) to access any data.  Supplying only an AuthKey enables read-only access to all of the data readable by the user who supplied the key.

For an example, to find all cards assigned to a particular member, first instantiate TrelloService and call the Retrieve method to get the Member object:

    string authKey = "[your authKey]";
    string memberId = "[member ID or username]";
    var service = new TrelloService(authKey);
    var member = service.Retrieve<Member>(memberId);

From here the properties on the Member object, along with some deft LINQage, can be used to retrieve entities which represent the cards:

    var personalBoards = member.Boards;
    var organizationBoards = member.Organization.Boards;
    var allBoards = personalBoards.Union(organizationBoards);
    var allLists = allBoards.SelectMany(b => b.Lists);
    var allCards = allLists.SelectMany(l => l.Cards);
    var memberCards = allCards.Where(c => c.Members.Contains(member));

####Writing data

Writing (changing data, adding cards, etc.) is enabled by additionally supplying an authorization token (AuthToken).  Like reading data, writing is limited to that which is accessible by the user who supplied the token.  Furthermore, the AuthKey and AuthToken much be supplied by the same user.

The following code adds a card to a List then assigns the first member of the board to the card.

    string authKey = "[your authKey]";
    string authToken = "[your authToken]";
    string listId = "[a list ID]";
    var service = new TrelloService(authKey, authToken);
    var list = service.Retrieve<List>(listId);
    var member = list.Board.Members.First();
    var card = list.AddCard("new card");
    card.AssignMember(member);

####More on authorization

The AuthKey and AuthToken must be supplied by an existing user of Trello.  All required information regarding obtaining these values may be found on [Trello's developer documentation pages](http://trello.com/docs/).  Currently, only a single user is supported: the user whose AuthKey and AuthToken are supplied.

###SUPPORT

If you find you need help with Manatee.Trello, please feel free to post a question on the forum located on [SourceForge](http://sourceforge.net/p/manateetrello/discussion/?source=navbar).  Alternatively, you can email me at [littlecrabsolutions@yahoo.com](mailto:littlecrabsolutions@yahoo.com).

For feature requests, bug reports, or documentation errata reports, please see the [development board for Manatee.Trello](https://trello.com/board/manatee-trello-net/5144051cbd0da6681200201e).