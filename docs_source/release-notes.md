# 4.3.0

*Released on 6 Mar, 2020*

<span id="feature">feature</span> 

([#332](https://github.com/gregsdennis/Manatee.Trello/issues/332)) Added `Enterprise` to `BoardPermissionLevel`.

# 4.2.3

*Released on 22 Jan, 2020*

<span id="patch">patch</span> 

([#327](https://github.com/gregsdennis/Manatee.Trello/issues/327)) Changed `reopenedBoard` to `reopenBoard` in `NotificationType`

# 4.2.2

*Released on 22 Jan, 2020*

<span id="patch">patch</span> 

([#327](https://github.com/gregsdennis/Manatee.Trello/issues/327)) New `NotificationType` discovered: `reopenBoard`.  (We'll get it right eventually.)

# 4.2.1

*Released on 22 Jan, 2020*

<span id="patch">patch</span> 

([#327](https://github.com/gregsdennis/Manatee.Trello/issues/327)) New `ActionType` discovered: `reopenBoard`.

# 4.2.0

*Released on 22 Jan, 2020*

<span id="feature">feature</span> 

([#323](https://github.com/gregsdennis/Manatee.Trello/issues/323)) Auth information supplied in `ITrelloFactory.Me()` is not propagated to the created object.  See also [#313](https://github.com/gregsdennis/Manatee.Trello/issues/313).

Upgraded Manatee.Json reference to v12.0.3 (major bump).

# 4.1.1

*Released on 15 Dec, 2019*

<span id="patch">patch</span> 

Fixed issue where `Action.Creator` is not populated when Trello returns `memberCreator` instead of `idMemberCreator`.

# 4.1.0

*Released on 12 Dec, 2019*

<span id="feature">feature</span> 

([#313](https://github.com/gregsdennis/Manatee.Trello/issues/313)) Augment `Me()` constructor by adding a `TrelloAuthorization` parameter to allow downloading multiple `Me` objects.  Note that this potentially allows simultaneous admin access to multiple accounts based on the level of access granted by the tokens.

# 4.0.0

*Released on 13 Nov, 2019*

<span id="break">breaking change</span> <span id="patch">patch</span> 

## Breaking Changes

The library is now only built for .Net Standard 2.0.  This change removes .Net Framework 4.5 and .Net Core 2.0 as build targets.

All types used to manage licenses have been removed.

## Other changes

([#308](https://github.com/gregsdennis/Manatee.Trello/issues/308)) Memory leak found.  A significant part of this was the synchronization contexts used to manage entity data.  Updating to weak references in some areas alleviates this.

Improved logging around requests.

# 3.10.3

*Released on 25 Sept, 2019*

<span id="patch">patch</span> 

Marked `License` class obsolete.  Please use the following license key with this and previous versions until a new major version is release, removing the `License` class

```
656622119-qUT8+J05IvRlosEnAaIZTsGeQBA7mcVNDiTaW49xIOgOq6O/Ay5z9dVFK0dJaQaalYMPLtMj5NeajqSG/Rmkykbi1a13COTZoy15wajYcG/SkcD1eWFwmWFR5ucBWwPOcjhJPfIoofUwe2qhaPd9CEcSayv2zlUlBlSSqI0cM1t7IklkIjo2NTY2MjIxMTksIkV4cGlyeURhdGUiOiIyMDIwLTA4LTAxVDAwOjAwOjAwIiwiVHlwZSI6IlRyZWxsb0J1c2luZXNzIn0=
```

# 3.10.2

*Released on 25 Sept, 2019*

<span id="patch">patch</span> 

([#310](https://github.com/gregsdennis/Manatee.Trello/issues/310)) Trello board download returns error: `invalid value for plugins`.  Trello recently started validating this as one of `[none, enabled, available, all]`, though what Manatee.Trello was passing (`true`) seemed to have been working.  This update changes the passed value to `enabled`, which was the originally intended functionality.  Also see [this issue filed with Trello](https://community.developer.atlassian.com/t/custom-fields-in-the-api-broken/32133?u=gregsdennis).

# 3.10.1

*Released on 7 Sept, 2019*

<span id="patch">patch</span> 

([#306](https://github.com/gregsdennis/Manatee.Trello/issues/306)) Fixes a performance issue when consistency processing is disabled.

# 3.10.0

*Released on 6 Aug, 2019*

<span id="feature">feature</span> 

Added new notification types which correspond with the action types added in 3.9.0, some of which are *not* listed in their documentation.

# 3.9.0

*Released on 1 Aug, 2019*

Added new action types, some of which are *not* listed in their documentation.

# 3.8.1

*Released on 17 Apr, 2019*

<span id="patch">patch</span> 

([#292](https://github.com/gregsdennis/Manatee.Trello/issues/292)) (again) Custom field names not downloaded as part of board refresh.  Culprit was that the JSON for the board's custom fields collection wasn't being merged when the collection was updated, so items were added but not updated.

# 3.8.0

*Released on 17 Apr, 2019*

<span id="feature">feature</span><span id="patch">patch</span> 

`Card.Actions` was not being populated even though data was being downloaded.

Added Licensing usage properties.

# 3.7.1

*Released on 5 Mar, 2019*

<span id="patch">patch</span> 

([#249](https://github.com/gregsdennis/Manatee.Trello/issues/249)) Comment creator wasn't being downloaded.

([#292](https://github.com/gregsdennis/Manatee.Trello/issues/292)) Custom field names not downloaded as part of board refresh.  This appeared to succeed most of the time, but in scenarios where it didn't, a refresh on the `board.CustomFields` collection directly fixed the issue.

# 3.7.0

*Released on 5 Mar, 2019*

<span id="feature">feature</span>

([#136](https://github.com/gregsdennis/Manatee.Trello/issues/136)) Emoji reactions can now be added/removed from card comments.

# 3.6.1

*Released on 25 Feb, 2019*

<span id="patch">patch</span> 

([#283](https://github.com/gregsdennis/Manatee.Trello/issues/283)) Adding attachments to cards via URLs was sad because the attachment URL wouldn't be properly encoded.  This resulted in the removal of parameters from the attachment URL (it'd be absorbed into the API URL).

([#284](https://github.com/gregsdennis/Manatee.Trello/issues/284)) Card collection not refreshing custom fields.  Found that nested resources weren't being included in the request correctly.  This fix correctly includes cards as nested fields in the request to refresh a board.  [#286](https://github.com/gregsdennis/Manatee.Trello/issues/286) has been created to validate other nested resources are being called correctly.

# 3.6.0

*Released on 30 Jan, 2019*

<span id="feature">feature</span> <span id="patch">patch</span> 

([#280](https://github.com/gregsdennis/Manatee.Trello/issues/280)) ([#281](https://github.com/gregsdennis/Manatee.Trello/issues/281)) New action type for deleted custom fields.  This manifested when refreshing a card that contained data for a custom field after that custom field definition was deleted.

# 3.5.1

*Released on 27 Dec, 2018*

<span id="patch">patch</span>

([#277](https://github.com/gregsdennis/Manatee.Trello/issues/277)) Custom fields parse incorrectly in non-English cultures.  (See issue for explanation.)

`ArgumentNullException` when working with token objects.  The API calls for tokens can only be used with the token value, not the object ID (like all other objects).  The library has been updated to use the token value for these API calls instead.

# 3.5.0

*Released on 9 Dec, 2018*

<span id="patch">patch</span>

([#251](https://github.com/gregsdennis/Manatee.Trello/issues/251)) Added `TrelloConfiguration.HttpClientFactory` to provide a mechanism to initialize `HttpClient` instances for the default REST client.

# 3.4.4

*Released on 9 Dec, 2018*

<span id="patch">patch</span>

Fixed exception when refreshing board members collection but when `Board.DownloadedFields` does not contain `Board.Fields.Members`.

# 3.4.3

*Released on 1 Nov, 2018*

<span id="patch">patch</span>

([#270](https://github.com/gregsdennis/Manatee.Trello/issues/270)) In some cases, having consistency processing on was causing entities to be added to collections twice.

# 3.4.2

*Released on 24 Oct, 2018*

<span id="patch">patch</span>

Fixed issue where licensing restrictions weren't being reset after an hour, resulting in the inability to use the library for mulitple hours after all calls have been consumed.

# 3.4.1

*Released on 23 Oct, 2018*

<span id="patch">patch</span>

([#260](https://github.com/gregsdennis/Manatee.Trello/issues/260)) Date-based filtering for card collections wasn't working at all.  Thanks to GitHub user [@zodchiy](https://github.com/zodchiy) for finding and fixing this one!

([#262](https://github.com/gregsdennis/Manatee.Trello/issues/262)) `TrelloFactory.Me()` now refreshes automatically.  Since you have to await it anyway, it was redundant to have to also call `await me.Refresh()`.

([#263](https://github.com/gregsdennis/Manatee.Trello/issues/263)) A previously-unknown notification type was found: `memberJoinedTrello`.  This is included for the member which invited someone to Trello when the invitation is accepted.

([#266](https://github.com/gregsdennis/Manatee.Trello/issues/266)) Refreshing member collections was throwing a `DuplicateKeyException` because it was trying to add the `fields` query parameter twice.

([#267](https://github.com/gregsdennis/Manatee.Trello/issues/267)) Filtering on card, board, list, and notification collections was broken when consistency processing was introduced.

# 3.4.0

*Released on 20 Sep, 2018*

<span id="feature">feature</span> <span id="patch">patch</span> 

Updated JSON support to use Manatee.Json v10.

Added `ActionType.CreatedCustomField`.

# 3.3.3

*Released on 20 Sep, 2018*

<span id="patch">patch</span>

([#254](https://github.com/gregsdennis/Manatee.Trello/issues/254)) When refreshing a board's cards collection, the `List` property would end up null.  This fix corrects the query parameters to correctly return the lists.

# 3.3.2

*Released on 15 Aug, 2018.*

<span id="patch">patch</span>

([#252](https://github.com/gregsdennis/Manatee.Trello/issues/252)) Collections aren't respecting the static `DownloadedField` property of the objects they were downloading.  This change adds the required query parameters to download all of the selected details.

# 3.3.1

*Released on 15 Aug, 2018.*

<span id="patch">patch</span>

([#249](https://github.com/gregsdennis/Manatee.Trello/issues/249)) Member data is being overwritten because actions and notifications are attempting to merge in creator data.  This change prevents overwriting during the merge.

# 3.3.0

*Released on 29 Jul, 2018.*

<span id="feature">feature</span><span id="patch">patch</span>

([PR 245](https://github.com/gregsdennis/Manatee.Trello/pull/245) by [@zodchiy](https://github.com/zodchiy)) Fixed issue with `Member.StarredBoards`.

([#241](https://github.com/gregsdennis/Manatee.Trello/issues/241)) Boards can now download most of their nested data depending on the value of the new `TrelloConfiguration.EnableDeepDownloads` static property.  By default, this property is set to true.  The effect is fully realized when `TrelloConfiguration.EnableConsistencyProcessing` (from #222) is also set to true.

([#222 (cont.)](https://github.com/gregsdennis/Manatee.Trello/issues/222)) While fixing #241, I discovered that some portions of #222 had been missed.  These have also been fixed.

# 3.2.1

*Released on 14 Jul, 2018.*

<span id="patch">patch</span>

([#241](https://github.com/gregsdennis/Manatee.Trello/issues/241)) Added a missing notification type: addAttachmentToCard.

# 3.2.0

*Released on 5 Jul, 2018.*

<span id="feature">feature</span> <span id="patch">patch</span> 

## Summary

([#3](https://github.com/gregsdennis/Manatee.Trello/issues/3)) Support for reading, uploading, and deleting custom board backgrounds.  (Uploading new backgrounds requires a Trello Gold account.)

([#222](https://github.com/gregsdennis/Manatee.Trello/issues/222)) Entities and the collections that contain them are more relational.  For instance if a card is moved to a new list (by assigning the `List` property or by refreshing the card after an online change), the source list's card collection removes the card and the destination list's card collection adds the card.  This is performed completely internally without having to make additional API calls.  This functionality is opt-in via `TrelloConfiguration.EnableConsistencyProcessing`.

([#235](https://github.com/gregsdennis/Manatee.Trello/issues/235)) An issue was discovered where refreshing collections would not raise the `Updated` event on the entities that owned them.  For instance, when a list is refreshed, it fetches the cards as part of the call and the event is raised with "Cards" in the list of properties that updated.  But if the `Cards` property were directly refreshed, the event would not be raised.  Also, none of the collections expose an `Updated` event.  This results in no notification that an update as occurred.  This change raises the event on the entity (in this case, the list) when any of its collections are updated.

([#227](https://github.com/gregsdennis/Manatee.Trello/issues/227)) When copying cards, it's possible to indicate what subset of additional data to copy besides merely property information. Any combination of attachments, checklists, comments, due date,labels, members, and stickers are supported.  This change adds an optional `keep` parameter to the `CardCollection.Add()` overload that takes a source card to duplicate.

([#239](https://github.com/gregsdennis/Manatee.Trello/issues/239)) Trello supports up to 10 items to be retrieved simultaneously through a single bulk call.  To support this, several changes have been introduced, most notably `TrelloProcessor.Refresh()` which takes a collection of entities and manages them into appropriate batches based on size and authorization, if multiple authorizations have been used.

## Changes

### New members

- `IJsonBoardBackground.Type`
- `IBoardBackground.Type`
- `IBoardBackground.Delete()`
- `BoardBackground.Type`
- `BoardBackground.Delete()`
- `IMember.BoardBackgrounds`
- `Member.BoardBackgrounds`
- `IMe.BoardBackgrounds`
- `Me.BoardBackgrounds`
- `static Member.Fields.BoardBackgrounds`
- `static TrelloConfiguration.EnableConsistencyProcessing`
- `IJsonCard.KeepFromSource`
- `static TrelloProcessor.Refresh()`

### New types

- `ReadOnlyBoardBackgroundCollection`
- `IBoardBackgroundCollection`
- `BoardBackgroundCollection`
- `BoardBackgroundType`
- `CardCopyKeepFromSourceOptions`
- `IJsonBatch`
- `IJsonBatchItem`
- `IRefreshable`
- `IBatchRefreshable`

### Functional changes

- Custom board backgrounds now downloaded by default as part of member.
- `ReadOnlyCustomFieldCollection` now properly implements `IReadOnlyCollection<ICustomField>` instead of `IReadOnlyCollection<CustomField>` (interface vs. class).
- Refreshing a collection now raises the `Updated` event on the collection's owner (e.g. calling `List.Cards.Refresh()` raises `List.Updated`).
- `CardCollection.Add(ICard card, CancellationToken ct = default(CancellationToken))` becomes `CardCollection.Add(ICard card, CardCopyKeepFromSourceOptions keep = CardCopyKeepFromSourceOptions.None, CancellationToken ct = default(CancellationToken))`.
- `CustomField.Refresh()` and `PowerUpBase.Refresh()` now work as expected.  Previously, these may have done nothing or failed completely.

# 3.1.0

*Released on 1 Jun, 2018.*

<span id="trello">trello</span> <span id="feature">feature</span>

## Summary

### In line with changes in the Trello API:

([#178](https://github.com/gregsdennis/Manatee.Trello/issues/178)) Getting member avatar images have been augmented. Now the client must specify an image size.  The default is 170x170 which was previously the only option.  Now 30x30 and 50x50 are available as well. [Trello's change](https://trello.com/c/VX8B4ndj)

(no issue logged) Label uses have been removed from the API. [Trello's change](https://trello.com/c/qlIE6fkg)

(no issue logged) Custom fields can now be configured to show or not show on the front of a card. [Trello's change](https://trello.com/c/F3j0G136)

### New library features:

([#187](https://github.com/gregsdennis/Manatee.Trello/issues/187)) Starred boards are represented as objects in the Trello API.  Previous library versions only exposed `Board.IsStarred` as a read-only property.  These can now be listed and manipulated through the `StarredBoard` entity and its collection on the `Member` and `Me` entities, respectively.

([#224](https://github.com/gregsdennis/Manatee.Trello/issues/224)) Updated collection `Add()` methods to include optional parameters so that the data can be included as part of the creation process rather than having to set properties which would require at least one additional call.

([#211](https://github.com/gregsdennis/Manatee.Trello/issues/211)) All entities and collections can now be forcibly refreshed.

## Changes

New members:

- `static Meember.AvatarSize`
- `Member.Fields.AvatarUrl`
- `ICustomFieldDefinition.DisplayInfo`
- `CustomFieldDefinition.DisplayInfo`
- `IJsonMember.StarredBoards`
- `Member.StarredBoards`
- `Me.StarredBoards`
- Optional `description` parameter for `IBoardCollection.Add()`
- Optional `description` parameter for `BoardCollection.Add()`
- Optional `position` parameter for `IListCollection.Add()`
- Optional `position` parameter for `ListCollection.Add()`
- Optional `description` and `name` parameters for `IOrganizationCollection.Add()`
- Optional `description` and `name` parameters for `OrganizationCollection.Add()`

New types:

- `AvatarSize`
- `ICustomFieldDisplayInfo`
- `CustomFieldDisplayInfo`
- `IJsonStarredBoard`
- `StarredBoard`
- `ReadOnlyStarredBoardCollection`
- `IStarredBoardCollection`
- `StarredBoardCollection`

Functional changes:

- `Member.AvatarUrl` now returns sized image assigned by `static Member.AvatarSize`

Obsoleted:

- `Member.Fields.AvatarHash`
- `Member.Fields.AvatarSource`
- `Member.Fields.GravatarHash`
- `Member.Fields.UploadedAvatarHash`
- `Member.AvatarSource` (now just returns null)
- `Label.Uses` (now just returns null)
- `IJsonLabel.Uses`

# 3.0.12

*Released on 30 May, 2018.*

<span id="feature">feature</span> <span id="patch">patch</span> 

Fixes issue for webhook processing where the property list provided by the event were inaccurate.  Also resolves an issue of updating cached entities with potentially stale data downloaded from `Action`s that indicated past activities.  **As a result, `Action.Data` and `Notification.Data` no longer use cached entities.**

Additionally, the properties reported for sub-entities (e.g. `Card.Badges`) are now prefixed with the container property.  So if `Card.Badges.Comments` (a count of the comments on the card) changes, the `Card.Updated` event would report that `Badges.Comments` was updated.  Previously, the property report would be only `Comments` which conflicts with the `Card.Comments` property.

Fixed a deserialization issue for cards.  `ShortLink` does not directly translate to `ShortUrl` and needs some formatting.

`Refresh()` on collection types is no longer virtual.  (Should have been sealed on all implementations anyway.)

# 3.0.11

*Released on 18 May, 2018.*

<span id="patch">patch</span> 

Fixed issue for all entities where processing webhook data would not fire the `Updated` event.

# 3.0.10

*Released on 14 May, 2018.*

<span id="patch">patch</span> 

Attachment image previews have their ID property serialized as `_id` rather than `id`.

# 3.0.9

*Released on 12 May, 2018.*

<span id="patch">patch</span> 

Changed serialization of numbers when setting custom field values to use invariant culture.

# 3.0.8

*Released on 12 May, 2018.*

<span id="feature">feature</span> <span id="patch">patch</span> 

Updated file location for license usage details to local app data for the current user.

Updated power-up implementation:

- `IBoard.PowerUps` is now `IPowerUpCollection` (was `IReadOnlyCollection<IPowerUp>`)
    - Adds `EnablePowerUp()` and `DisablePowerUp()`
- Fixed issues with setting number, string, and drop-down custom fields

# 3.0.7

*Released on 11 May, 2018.*

<span id="patch">patch</span> 

Bug fix for setting dropdown and text custom fields on cards without values.

Internal updates to collection classes.

# 3.0.6

*Released on 7 May, 2018.*

<span id="patch">patch</span> 

Updated boards and cards to only cache themselves once the full ID has been downloaded.

# 3.0.5

*Released on 4 May, 2018.*

<span id="patch">patch</span> 

Fixed further issues with deserialization.

- `IJsonBoard`
- `IJsonList`
- `IJsonOrganizationMembership`
- `IJsonToken`

# 3.0.4

*Released on 4 May, 2018.*

<span id="patch">patch</span> 

Fixed issue with `IJsonBoardBackground` deserialization.

# 3.0.3

*Released on 3 May, 2018.*

<span id="patch">patch</span> 

Fixed issue with `DropDownOption` that caused `ArgumentNullException` when attempting to add the entity to the cache.

# 3.0.2

*Released on 2 May, 2018.*

<span id="trello">trello</span> <span id="feature">feature</span> <span id="patch">patch</span> 

The following are now read-only as these requests are not supported by Trello.

- `Board.IsPinned`
- `Board.IsStarred`
- `CheckList.Card`

`ICache` changed to take `ICacheable` instead of any object to help support better thread safety.

`Webhook` now implements `ICacheable`

# 3.0.1

*Released on 1 May, 2018.*

<span id="patch">patch</span> 

Fixed description attribute for:

- `CheckList.Fields.Board`
- `CheckList.Fields.Card`

`IDropDownCollection` fixed to inherit `IReadOnlyCollection<IDropDownOption>`.

Added missing `DropDownOption` method to `TrelloFactory` to provide a mechanism for creating new options for custom fields.  Also added matching `static DropDownOption.Create()` method.

Added `CheckList` to `IJsonCheckItem`

Fixed serialization issues for:

- cards
- check items
- check lists

# 3.0.0

*Released on 27 Apr, 2018.*

<span id="break">breaking change</span> <span id="trello">trello</span> <span id="feature">feature</span> <span id="patch">patch</span> 

## Supported frameworks

Now multi-targets:

- .Net Framework 4.5
- .Net Standard 1.3
- .Net Standard 2.0

## Added asynchronous processing

All methods that perform requests (`Refresh()`, `Delete()`, collection `Add()` methods, etc.) are now async methods and should be awaited.

Request processing no longer occurs in a queue.  Instead, the .Net *async/await* model is used.

## Data access

Getting properties no longer produces requests.  Calling `Refresh()` is required.

Setting properties works as before.

When refreshing entities and collections, more data is downloaded with a single call.  Additionally, all data is used to update any available entities.  This results in fewer required calls.

## Entities

Interfaces have been extracted for all entities to support unit testing.

All properties have been altered to return interfaces rather than concrete types.

## Configuration

Added `RefreshThrottle` to limit successive GET requests.
Added `RemoveDeletedItemsFromCache` to optionally keep deleted entities in the cache.
Removed `ExpiryTime` in conjuction with changes to property getters.

## Libraries

The following libraries have been consolidated into the main library:

- *Manatee.Trello.ManateeJson*
- *Manatee.Trello.WebApi*
- *Manatee.Trello.CustomFields*

The configuration seams for these libraries are still available if alternate solutions are desired.

### Additional changes

Custom fields are now writable.