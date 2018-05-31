---
title: IJsonNotification
category: API
order: 114
---

Defines the JSON structure for the Notification object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonNotification

## Properties

### [IJsonNotificationData](../IJsonNotificationData#ijsonnotificationdata) Data { get; set; }

Gets or sets the data associated with the notification. Contents depend upon the notification&#39;s type.

### DateTime? Date { get; set; }

Gets or sets the date on which the notification was created.

### [IJsonMember](../IJsonMember#ijsonmember) MemberCreator { get; set; }

Gets or sets the ID of the member whose action spawned the notification.

### [NotificationType](../NotificationType#notificationtype)? Type { get; set; }

Gets or sets the notification&#39;s type.

### bool? Unread { get; set; }

Gets or sets whether the notification has been read.

