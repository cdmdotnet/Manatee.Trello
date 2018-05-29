---
title: Notification
category: API
order: 196
---

Represents a notification.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Notification

## Constructors

### Notification(string id, TrelloAuthorization auth)

Creates a new [Notification](../Notification#notification) object.

**Parameter:** id

The notification&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided,
[TrelloAuthorization.Default](../TrelloAuthorization#static-trelloauthorization-default--get-) will be used.

## Properties

### static Manatee.Trello.Notification+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### DateTime CreationDate { get; }

Gets the creation date of the notification.

### [IMember](../IMember#imember) Creator { get; }

Gets the member who performed the action which created the notification.

### [INotificationData](../INotificationData#inotificationdata) Data { get; }

Gets any data associated.

### DateTime? Date { get; }

Gets the date and time at which the notification was issued.

### string Id { get; }

Gets the notification&#39;s ID.

### bool? IsUnread { get; set; }

Gets or sets whether the notification has been read.

### [NotificationType](../NotificationType#notificationtype)? Type { get; }

Gets the type of notification.

## Events

### Action&lt;INotification, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the notification is updated.

## Methods

### Task Refresh(CancellationToken ct)

Refreshes the notification data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

#### Filterpriority

2

