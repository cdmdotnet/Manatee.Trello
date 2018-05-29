---
title: INotification
category: API
order: 82
---

# INotification

Represents a notification.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- INotification

## Properties

### DateTime CreationDate { get; }

Gets the creation date of the notification.

### [IMember](IMember#imember) Creator { get; }

Gets the member who performed the action which created the notification.

### [INotificationData](INotificationData#inotificationdata) Data { get; }

Gets any data associated with the notification.

### DateTime? Date { get; }

Gets the date and teim at which the notification was issued.

### bool? IsUnread { get; set; }

Gets or sets whether the notification has been read.

### [NotificationType](NotificationType#notificationtype)? Type { get; }

Gets the type of notification.

## Events

### Action&lt;INotification, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the notification is updated.

## Methods

### Task Refresh(CancellationToken ct)

Refreshes the notification data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

