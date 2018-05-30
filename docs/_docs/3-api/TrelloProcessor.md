---
title: TrelloProcessor
category: API
order: 249
---

Provides options and control for the internal request queue processor.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- TrelloProcessor

## Methods

### static Task Flush()

Signals the processor that the application is shutting down. The processor will perform a &quot;last call&quot; for pending requests.

### static void ProcessNotification(string content, TrelloAuthorization auth)

Processes webhook notification content.

**Parameter:** content

The string content of the notification.

**Parameter:** auth

The Manatee.Trello.TrelloAuthorization under which the notification should be processed

