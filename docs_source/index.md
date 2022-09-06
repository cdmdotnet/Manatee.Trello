# Welcome!

Manatee.Trello is a .Net wrapper for [Trello’s RESTful API](https://developers.trello.com/advanced-reference) written C#.  The goal of this library is to expose the functionality of Trello as an intuitive, fully object-oriented design that allows the developer to interact with Trello objects in a way familiar to the average .Net developer.

Every object possesses the properties one would expect to find by simply using the [Trello web UI](http://trello.com).  For example, a Board has a collection of Lists, each of which has a collection of Cards.  A Card also references back to the List and Board in which it is contained.

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
- Batch downloads
- Functions:
    - Boards
        - Add/Edit/Delete
        - Add/Edit board-wide preferences & permissions
        - Add/Edit personal preferences
        - Add/Remove members
        - Add/Edit/Delete custom fields
        - Add/Remove to/from organization
        - Enable/Disable power-ups
        - Set custom backgrounds
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
        - Show/Hide custom fields on cover
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
        - Read custom board backgrounds
        - Delete custom board backgrounds for authenticated member
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

# Who uses Manatee.Trello?

<div class="image-row image-center">
    <a href="https://octopus.com/"><img src="https://octopus.com/images/company/Logo-Text-BlueBlackTransparent-400_rgb.png"></a>
    <a href="https://www.nirvc.com/"><img src="https://www.nirvc.com/img/Logo.png"></a>
    <a href="https://www.vs-sistemas.com/inicio"><img src="https://www.vs-sistemas.com/Content/images/logo_vs_sistemas.png"></a>
    <a href="http://www.satorianalytics.com/"><img src="images/clients/satori-logo.png"></a>
    <a href="https://www.alliancereservations.com/"><img src="images/clients/arnlogo-white.png"></a>
    <a href="http://www.softiceit.co.za/"><img src="images/clients/SOFTICEITNew.png"></a>
</div>

# How it all started

When I started this project in 2012, I was a software support developer, which meant that when other people’s code did something it wasn't supposed to, I was on the team that determined where the bug was and fixed it.  As such we had a plethora of systems dedicated to managing our list of found problems.

The business users (my bosses) wanted a simple report to show the statuses of all of the problems we were working on as well as what was in our backlog and what we were planning on tackling next.  Enter Trello.  Following the example of Trello's own [Dev Board](https://trello.com/dev), I created an organization for the company and a board for my team so that my internal customers could simply load up our board and see the information in an easy-to-read manner.

After about a month of manually updating cards in both our problem management systems and on Trello, I decided it was time to investigate options for pulling data from our systems and uploading automatically to our board.  I found that while Trello exposed a rich API to manipulate the site programmatically, the existing .Net wrappers were insufficient for my need.  The existing wrappers only exposed a service-based architecture, which didn’t seem very object-oriented, and it was very obvious that they were mere wrappers around function calls provided by Trello’s development team.  I wanted objects with properties which I could manipulate, and I didn’t want to see any hint of an API.

Thus, Manatee.Trello was conceived.

Since its conception, I have moved through the ranks (and the world) to a Senior Developer role, implementing design patterns, best practices, and architectures all over everywhere.  I have endeavored to apply that knowledge and experience to this library.
