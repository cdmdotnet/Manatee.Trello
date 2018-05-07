If one were to use the Trello web interface, they would find that everything is represented as objects which contain and/or reference other objects:  Boards contain a collection of Lists, each of which contains a collection of Cards, etc.  These concepts are the basis for the architecture behind Manatee.Trello.

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

Manatee.Trello also exposes a number of other objects, but these represent the main idea behind Trello.  The relationships between these object types are shown by the figure below.

![Architecture Chart](https://bytebucket.org/gregsdennis/manatee.trello/wiki/Resources/OrgChart.jpg?token=1db5a0cb406edd73ec16b7623df990a7dc54a485)

This object graph is replicated in .Net by their respective entity classes.  Each entity class exposes the data you would expect to see from the web UI.  Most of these properties are read/write, but some are read-only (e.g. the URL property on a Board).  Also, all collection properties implement `IEnumerable<T>` and some expose additional methods for collection modification.