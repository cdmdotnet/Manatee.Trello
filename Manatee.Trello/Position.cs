/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Position.cs
	Namespace:		Manatee.Trello
	Class Name:		Position
	Purpose:		Represents the position of a checklist in a card, a card
					in a list, or list in a board on Trello.com.

***************************************************************************************/
using System;
using Manatee.Trello.Internal;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the position of a checklist in a card, a card in a list,
	/// or list in a board
	/// </summary>
	public class Position : IComparable, IComparable<Position>
	{
		private const double TopValue = double.NegativeInfinity;
		private const double BottomValue = double.PositiveInfinity;
		private const double UnknownValue = double.NaN;

		private static readonly Position _top = new Position(TopValue);
		private static readonly Position _bottom = new Position(BottomValue);
		private static readonly Position _unknown = new Position(UnknownValue);

		/// <summary>
		/// Represents the top position.
		/// </summary>
		public static Position Top { get { return _top; } }
		/// <summary>
		/// Represents the bottom position.
		/// </summary>
		public static Position Bottom { get { return _bottom; } }
		/// <summary>
		/// Represents an invalid position.
		/// </summary>
		public static Position Unknown { get { return _unknown; } }

		private readonly double _value = UnknownValue;

		/// <summary>
		/// Gets whether the position is valid.
		/// </summary>
		public bool IsValid { get { return !Equals(_value, UnknownValue); } }
		internal double Value { get { return _value; } }

		/// <summary>
		/// Creates a new instance of the Position class.
		/// </summary>
		/// <param name="value">A positive integer.</param>
		public Position(double value)
		{
			_value = value;
		}

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(Position other)
		{
			return _value.CompareTo(other._value);
		}
		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj"/> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj"/>. Greater than zero This instance follows <paramref name="obj"/> in the sort order. 
		/// </returns>
		/// <param name="obj">An object to compare with this instance. </param><exception cref="T:System.ArgumentException"><paramref name="obj"/> is not the same type as this instance. </exception><filterpriority>2</filterpriority>
		public int CompareTo(object obj)
		{
			return _value.CompareTo(obj);
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			if (Equals(_unknown)) return "unknown";
			if (Equals(_top)) return "top";
			if (Equals(_bottom)) return "bottom";
			return _value.ToLowerString();
		}
		/// <summary>
		/// Implicitly casts a PositionValue to a Position.
		/// </summary>
		/// <param name="value">The PositionValue value.</param>
		/// <returns>The Position object.</returns>
		public static implicit operator Position(double value)
		{
			return new Position(value);
		}
		/// <summary>
		/// Implicitly casts an int to a Position.
		/// </summary>
		/// <param name="value">a positive integer.</param>
		/// <returns>The Position object.</returns>
		public static implicit operator Position(int value)
		{
			return new Position(value);
		}
		/// <summary>
		/// Explicitly casts a Position to a PositionValue.
		/// </summary>
		/// <param name="position">The Position object.</param>
		/// <returns>The PositionValue value.</returns>
		public static explicit operator double(Position position)
		{
			return position._value;
		}
		/// <summary>
		/// Explicitly casts a Position to an int.
		/// </summary>
		/// <param name="position">The Position object.</param>
		/// <returns>The int value.</returns>
		public static explicit operator int(Position position)
		{
			return (int) position._value;
		}
		/// <summary>
		/// Compares two Position object by examining their content.
		/// </summary>
		/// <param name="a">A Position object.</param>
		/// <param name="b">A Position object.</param>
		/// <returns>True if equivalent, false otherwise.</returns>
		public static bool operator ==(Position a, Position b)
		{
			if (ReferenceEquals(a, b)) return true;
			if (Equals(a, null) || Equals(b, null)) return false;
			return Equals(a._value, b._value);
		}
		/// <summary>
		/// Compares two Position object by examining their content.
		/// </summary>
		/// <param name="a">A Position object.</param>
		/// <param name="b">A Position object.</param>
		/// <returns>False if equivalent, true otherwise.</returns>
		public static bool operator !=(Position a, Position b)
		{
			return !(a == b);
		}
		/// <summary>
		/// Compares two position values for linear order.
		/// </summary>
		/// <param name="a">A Position object.</param>
		/// <param name="b">A Position object.</param>
		/// <returns>True if the first operand is less than the second, false otherwise.</returns>
		public static bool operator <(Position a, Position b)
		{
			return a._value < b._value;
		}
		/// <summary>
		/// Compares two position values for linear order.
		/// </summary>
		/// <param name="a">A Position object.</param>
		/// <param name="b">A Position object.</param>
		/// <returns>True if the first operand is greater than the second, false otherwise.</returns>
		public static bool operator >(Position a, Position b)
		{
			return a._value > b._value;
		}
		/// <summary>
		/// Compares two position values for linear order.
		/// </summary>
		/// <param name="a">A Position object.</param>
		/// <param name="b">A Position object.</param>
		/// <returns>True if the first operand is less than or equal to the second, false otherwise.</returns>
		public static bool operator <=(Position a, Position b)
		{
			return a._value <= b._value;
		}
		/// <summary>
		/// Compares two position values for linear order.
		/// </summary>
		/// <param name="a">A Position object.</param>
		/// <param name="b">A Position object.</param>
		/// <returns>True if the first operand is greater than or equal to the second, false otherwise.</returns>
		public static bool operator >=(Position a, Position b)
		{
			return a._value >= b._value;
		}
		/// <summary>
		/// Compares two Position object by examining their content.
		/// </summary>
		/// <param name="other">A Position object.</param>
		/// <returns>True if equivalent, false otherwise.</returns>
		public bool Equals(Position other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._value, _value);
		}
		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Position)) return false;
			return Equals((Position) obj);
		}
		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}
	}
}
