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
using Manatee.Trello.Json;

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

		/// <summary>
		/// Represents the top position.
		/// </summary>
		public static Position Top { get; } = new Position(TopValue);
		/// <summary>
		/// Represents the bottom position.
		/// </summary>
		public static Position Bottom { get; } = new Position(BottomValue);
		/// <summary>
		/// Represents an invalid position.
		/// </summary>
		public static Position Unknown { get; } = new Position(UnknownValue);

		/// <summary>
		/// Gets whether the position is valid.
		/// </summary>
		public bool IsValid => Equals(Value, TopValue) || (!Equals(Value, UnknownValue) && !Equals(Value, TopValue) && Value > 0);
		/// <summary>
		/// Gets the internal numeric position value.
		/// </summary>
		public double Value { get; }

		/// <summary>
		/// Creates a new instance of the <see cref="Position"/> class.
		/// </summary>
		/// <param name="value">A positive integer.</param>
		public Position(double value)
		{
			Value = value;
		}

		/// <summary>
		/// Creates a new <see cref="Position"/> object between two others.
		/// </summary>
		/// <param name="a">A <see cref="Position"/>.</param>
		/// <param name="b">Another <see cref="Position"/>.</param>
		/// <returns>The new <see cref="Position"/>.</returns>
		public static Position Between(Position a, Position b)
		{
			return new Position((a.Value + b.Value)/2);
		}

		internal static Position GetPosition(IJsonPosition pos)
		{
			if (pos == null) return null;
			if (pos.Named.IsNullOrWhiteSpace() && pos.Explicit.HasValue)
				return new Position(pos.Explicit.Value);
			switch (pos.Named)
			{
				case "top":
					return Top;
				case "bottom":
					return Bottom;
			}
			return null;
		}
		internal static IJsonPosition GetJson(Position pos)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonPosition>();
			if (pos == null) return json;
			if (Equals(pos, Unknown))
				json.Named = "unknown";
			else if (Equals(pos, Top))
				json.Named = "top";
			else if (Equals(pos, Bottom))
				json.Named = "bottom";
			else
				json.Explicit = pos.Value;
			return json;
		}

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return
		/// value has the following meanings:
		///     Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.
		///     Zero This object is equal to <paramref name="other"/>.
		///     Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(Position other)
		{
			return Value.CompareTo(other.Value);
		}
		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that
		/// indicates whether the current instance precedes, follows, or occurs in the same position in the
		/// sort order as the other object.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return
		/// value has these meanings:
		///     Value Meaning Less than zero This instance precedes <paramref name="obj"/> in the sort order.
		///     Zero This instance occurs in the same position in the sort order as <paramref name="obj"/>.
		///     Greater than zero This instance follows <paramref name="obj"/> in the sort order. 
		/// </returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <exception cref="T:System.ArgumentException">
		/// <paramref name="obj"/> is not the same type as this instance.
		/// </exception>
		/// <filterpriority>2</filterpriority>
		public int CompareTo(object obj)
		{
			return Value.CompareTo(obj);
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
			if (Equals(Unknown)) return "unknown";
			if (Equals(Top)) return "top";
			if (Equals(Bottom)) return "bottom";
			return Value.ToLowerString();
		}
		/// <summary>
		/// Implicitly casts a PositionValue to a <see cref="Position"/>.
		/// </summary>
		/// <param name="value">The PositionValue value.</param>
		/// <returns>The Position object.</returns>
		public static implicit operator Position(double value)
		{
			return new Position(value);
		}
		/// <summary>
		/// Implicitly casts an int to a <see cref="Position"/>.
		/// </summary>
		/// <param name="value">a positive integer.</param>
		/// <returns>The Position object.</returns>
		public static implicit operator Position(int value)
		{
			return new Position(value);
		}
		/// <summary>
		/// Explicitly casts a <see cref="Position"/> to a double.
		/// </summary>
		/// <param name="position">The Position object.</param>
		/// <returns>The PositionValue value.</returns>
		public static explicit operator double(Position position)
		{
			return position.Value;
		}
		/// <summary>
		/// Explicitly casts a <see cref="Position"/> to an int.
		/// </summary>
		/// <param name="position">The Position object.</param>
		/// <returns>The int value.</returns>
		public static explicit operator int(Position position)
		{
			return (int) position.Value;
		}
		/// <summary>
		/// Compares two <see cref="Position"/> objects by examining their content.
		/// </summary>
		/// <param name="a">A <see cref="Position"/> object.</param>
		/// <param name="b">A <see cref="Position"/> object.</param>
		/// <returns>True if equivalent, false otherwise.</returns>
		public static bool operator ==(Position a, Position b)
		{
			if (ReferenceEquals(a, b)) return true;
			if (Equals(a, null) || Equals(b, null)) return false;
			return Equals(a.Value, b.Value);
		}
		/// <summary>
		/// Compares two <see cref="Position"/> objects by examining their content.
		/// </summary>
		/// <param name="a">A <see cref="Position"/> object.</param>
		/// <param name="b">A <see cref="Position"/> object.</param>
		/// <returns>False if equivalent, true otherwise.</returns>
		public static bool operator !=(Position a, Position b)
		{
			return !(a == b);
		}
		/// <summary>
		/// Compares two <see cref="Position"/> values for linear order.
		/// </summary>
		/// <param name="a">A <see cref="Position"/> object.</param>
		/// <param name="b">A <see cref="Position"/> object.</param>
		/// <returns>True if the first operand is less than the second, false otherwise.</returns>
		public static bool operator <(Position a, Position b)
		{
			return a.Value < b.Value;
		}
		/// <summary>
		/// Compares two <see cref="Position"/> values for linear order.
		/// </summary>
		/// <param name="a">A <see cref="Position"/> object.</param>
		/// <param name="b">A <see cref="Position"/> object.</param>
		/// <returns>True if the first operand is greater than the second, false otherwise.</returns>
		public static bool operator >(Position a, Position b)
		{
			return a.Value > b.Value;
		}
		/// <summary>
		/// Compares two <see cref="Position"/> values for linear order.
		/// </summary>
		/// <param name="a">A <see cref="Position"/> object.</param>
		/// <param name="b">A <see cref="Position"/> object.</param>
		/// <returns>True if the first operand is less than or equal to the second, false otherwise.</returns>
		public static bool operator <=(Position a, Position b)
		{
			return a.Value <= b.Value;
		}
		/// <summary>
		/// Compares two <see cref="Position"/> values for linear order.
		/// </summary>
		/// <param name="a">A <see cref="Position"/> object.</param>
		/// <param name="b">A <see cref="Position"/> object.</param>
		/// <returns>True if the first operand is greater than or equal to the second, false otherwise.</returns>
		public static bool operator >=(Position a, Position b)
		{
			return a.Value >= b.Value;
		}
		/// <summary>
		/// Compares two <see cref="Position"/> object by examining their content.
		/// </summary>
		/// <param name="other">A <see cref="Position"/> object.</param>
		/// <returns>True if equivalent, false otherwise.</returns>
		public bool Equals(Position other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Value, Value);
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
			return Value.GetHashCode();
		}
	}
}
