---
title: ICache
category: API
order: 65
---

Defines operations for a cache.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- ICache

## Methods

### void Add(ICacheable obj)

Adds an object to the cache, if it does not already exist.

**Parameter:** obj

The object to add.

### void Clear()

Removes all objects from the cache.

### T Find&lt;T&gt;(string id)

Finds an object of a certain type meeting specified criteria.

**Type Parameter:** T : [ICacheable](../ICacheable#icacheable)

The type of object to find.

**Parameter:** id

The ID to search for.

### void Remove(ICacheable obj)

Removes an object from the cache, if it exists.

**Parameter:** obj

The object to remove.

