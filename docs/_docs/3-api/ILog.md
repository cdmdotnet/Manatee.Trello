---
title: ILog
category: API
order: 129
---

Defines methods required to log information, events, and errors generated throughout Manatee.Trello.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ILog

## Methods

### void Debug(string message, Object[] parameters)

Writes a debug level log entry.

**Parameter:** message

The message or message format.

**Parameter:** parameters

A list of parameters.

### void Error(Exception e)

Writes an error level log entry.

**Parameter:** e

The exception that will be or was thrown.

### void Info(string message, Object[] parameters)

Writes an information level log entry.

**Parameter:** message

The message or message format.

**Parameter:** parameters

A list of paramaters.

