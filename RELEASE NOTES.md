This file is used to list specific public-facing changes for each version.  The notes on the Nuget package are a summary of these.

## 3.0.8

Updated file location for license usage details to local app data for the current user.

Updated power-up implementation:

- `IBoard.PowerUps` is now `IPowerUpCollection` (was `IReadOnlyCollection<IPowerUp>`)
    - Adds `EnablePowerUp()` and `DisablePowerUp()`
- Fixed issues with setting number, string, and drop-down custom fields

## 3.0.7

Bug fix for setting dropdown and text custom fields on cards without values.

Internal updates to collection classes.

## 3.0.6

Updated boards and cards to only cache themselves once the full ID has been downloaded.

## 3.0.5

Fixed further issues with deserialization.

- `IJsonBoard`
- `IJsonList`
- `IJsonOrganizationMembership`
- `IJsonToken`

## 3.0.4

Fixed issue with `IJsonBoardBackground` deserialization.

## 3.0.3

Fixed issue with `DropDownOption` that caused `ArgumentNullException` when attempting to add the entity to the cache.

## 3.0.2

The following are now read-only as these requests are not supported by Trello.

- `Board.IsPinned`
- `Board.IsStarred`
- `CheckList.Card`

`ICache` changed to take `ICacheable` instead of any object to help support better thread safety.

`Webhook` now implements `ICacheable`

## 3.0.1

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

## 3.0.0

### Supported frameworks

Now multi-targets:

- .Net Framework 4.5
- .Net Standard 1.3
- .Net Standard 2.0

### Added asynchronous processing

All methods that perform requests (`Refresh()`, `Delete()`, collection `Add()` methods, etc.) are now async methods and should be awaited.

Request processing no longer occurs in a queue.  Instead, the .Net *async/await* model is used.

### Data access

Getting properties no longer produces requests.  Calling `Refresh()` is required.

Setting properties works as before.

When refreshing entities and collections, more data is downloaded with a single call.  Additionally, all data is used to update any available entities.  This results in fewer required calls.

### Entities

Interfaces have been extracted for all entities to support unit testing.

All properties have been altered to return interfaces rather than concrete types.

### Configuration

Added `RefreshThrottle` to limit successive GET requests.
Added `RemoveDeletedItemsFromCache` to optionally keep deleted entities in the cache.
Removed `ExpiryTime` in conjuction with changes to property getters.

### Libraries

The following libraries have been consolidated into the main library:

- *Manatee.Trello.ManateeJson*
- *Manatee.Trello.WebApi*
- *Manatee.Trello.CustomFields*

The configuration seams for these libraries are still available if alternate solutions are desired.

### Additional changes

Custom fields are now writable.