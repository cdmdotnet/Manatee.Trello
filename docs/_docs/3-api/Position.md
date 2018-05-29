---
title: Position
category: API
order: 154
---

# Position

Represents the position of a checklist in a card, a card in a list, or list in a board

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- Position

## Constructors

### Position(double value)

Creates a new instance of the [Position](Position#position) class.

**Parameter:** value

A positive integer.

## Properties

### static [Position](Position#position) Bottom { get; }

Represents the bottom position.

### static [Position](Position#position) Top { get; }

Represents the top position.

### static [Position](Position#position) Unknown { get; }

Represents an invalid position.

### bool IsValid { get; }

Gets whether the position is valid.

### double Value { get; }

Gets the internal numeric position value.

## Methods

### static [Position](Position#position) Between(Position a, Position b)

Creates a new [Position](Position#position) object between two others.

**Parameter:** a

A [Position](Position#position).

**Parameter:** b

Another [Position](Position#position).

**Returns:** The new [Position](Position#position).

### static bool op_Equality(Position a, Position b)

Compares two [Position](Position#position) objects by examining their content.

**Parameter:** a

A [Position](Position#position) object.

**Parameter:** b

A [Position](Position#position) object.

**Returns:** True if equivalent, false otherwise.

### static bool op_GreaterThan(Position a, Position b)

Compares two [Position](Position#position) values for linear order.

**Parameter:** a

A [Position](Position#position) object.

**Parameter:** b

A [Position](Position#position) object.

**Returns:** True if the first operand is greater than the second, false otherwise.

### static bool op_GreaterThanOrEqual(Position a, Position b)

Compares two [Position](Position#position) values for linear order.

**Parameter:** a

A [Position](Position#position) object.

**Parameter:** b

A [Position](Position#position) object.

**Returns:** True if the first operand is greater than or equal to the second, false otherwise.

### static bool op_Inequality(Position a, Position b)

Compares two [Position](Position#position) objects by examining their content.

**Parameter:** a

A [Position](Position#position) object.

**Parameter:** b

A [Position](Position#position) object.

**Returns:** False if equivalent, true otherwise.

### static bool op_LessThan(Position a, Position b)

Compares two [Position](Position#position) values for linear order.

**Parameter:** a

A [Position](Position#position) object.

**Parameter:** b

A [Position](Position#position) object.

**Returns:** True if the first operand is less than the second, false otherwise.

### static bool op_LessThanOrEqual(Position a, Position b)

Compares two [Position](Position#position) values for linear order.

**Parameter:** a

A [Position](Position#position) object.

**Parameter:** b

A [Position](Position#position) object.

**Returns:** True if the first operand is less than or equal to the second, false otherwise.

### int CompareTo(Position other)

Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.

**Parameter:** other

An object to compare with this instance.

**Returns:** A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes *other* in the sort order. Zero This instance occurs in the same position in the sort order as *other*. Greater than zero This instance follows *other* in the sort order.

### int CompareTo(Object obj)

Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.

**Parameter:** obj

An object to compare with this instance.

**Returns:** A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes *obj* in the sort order. Zero This instance occurs in the same position in the sort order as *obj*. Greater than zero This instance follows *obj* in the sort order.

**Exception:** System.ArgumentException

*obj* is not the same type as this instance.

### bool Equals(Position other)

Compares two [Position](Position#position) object by examining their content.

**Parameter:** other

A [Position](Position#position) object.

**Returns:** True if equivalent, false otherwise.

### bool Equals(Object obj)

Determines whether the specified System.Object is equal to the current System.Object.

**Parameter:** obj

The object to compare with the current object.

**Returns:** true if the specified System.Object is equal to the current System.Object; otherwise, false.

#### Filterpriority

2

### bool Equals(Object obj)

Determines whether the specified System.Object is equal to the current System.Object.

**Parameter:** obj

The object to compare with the current object.

**Returns:** true if the specified System.Object is equal to the current System.Object; otherwise, false.

#### Filterpriority

2

### int GetHashCode()

Serves as a hash function for a particular type.

**Returns:** A hash code for the current System.Object.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.

