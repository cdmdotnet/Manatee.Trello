---
title: 3.0.10
type: bug fix
---

## Summary

In line with changes in the Trello API:

- Getting member avatar images have been augmented. Now the client must specify an image size.  The default is 170x170 which was previously the only option.  Now 30x30 and 50x50 are available as well.
    - https://trello.com/c/VX8B4ndj
- Label uses have been removed from the API.
    - https://trello.com/c/qlIE6fkg
- Custom fields can now be configured to show or not show on the front of a card.
    - https://trello.com/c/F3j0G136

## Changes

New members:

- `static Meember.AvatarSize`
- `Member.Fields.AvatarUrl`
- `ICustomFieldDefinition.DisplayInfo`
- `CustomFieldDefinition.DisplayInfo`

New types:

- `AvatarSize`
- `ICustomFieldDisplayInfo`
- `CustomFieldDisplayInfo`

Functional changes:

- `Member.AvatarUrl` now returns sized image assigned by `static Member.AvatarSize`

Obsoleted the following:

- `Member.Fields.AvatarHash`
- `Member.Fields.AvatarSource`
- `Member.Fields.GravatarHash`
- `Member.Fields.UploadedAvatarHash`
- `Member.AvatarSource` (now just returns null)
- `Label.Uses` (now just returns null)
- `IJsonLabel.Uses`
