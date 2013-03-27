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
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Serialization;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the position of a checklist in a card, a card in a list,
	/// or list in a board
	/// </summary>
	public class Position : IJsonCompatible
	{
		private PositionValue _value;

		/// <summary>
		/// Gets whether the position is valid.
		/// </summary>
		public bool IsValid { get { return _value > PositionValue.Unknown; } }
		internal PositionValue Value { get { return _value; } }

		/// <summary>
		/// Creates a new instance of the Position class.
		/// </summary>
		/// <param name="value">A positive integer, Top, or Bottom.</param>
		public Position(PositionValue value)
		{
			_value = value;
		}
		/// <summary>
		/// Creates a new instance of the Position class.
		/// </summary>
		/// <param name="value">A positive integer.</param>
		public Position(int value)
		{
			_value = (PositionValue) value;
		}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public void FromJson(JsonValue json)
		{
			if (json == null) return;
			switch (json.Type)
			{
				case JsonValueType.Number:
					_value = (PositionValue) json.Number;
					break;
				case JsonValueType.String:
					switch (json.String.ToLower())
					{
						case "top":
							_value = PositionValue.Top;
							break;
						case "bottom":
							_value = PositionValue.Bottom;
							break;
						default:
							_value = PositionValue.Unknown;
							break;
					}
					break;
				default:
					_value = PositionValue.Unknown;
					break;
			}
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public JsonValue ToJson()
		{
			JsonValue json;
			switch (_value)
			{
				case PositionValue.Top:
					json = "top";
					break;
				case PositionValue.Bottom:
					json = "bottom";
					break;
				default:
					json = _value > 0 ? (int) _value : JsonValue.Null;
					break;
			}
			return json;
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
			return _value.ToLowerString();
		}
		/// <summary>
		/// Implicitly casts a PositionValue to a Position.
		/// </summary>
		/// <param name="value">The PositionValue value.</param>
		/// <returns>The Position object.</returns>
		public static implicit operator Position(PositionValue value)
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
		public static explicit operator PositionValue(Position position)
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
			if (Equals(a, null) && Equals(b, null)) return true;
			if (Equals(a, null) || Equals(b, null)) return false;
			return a._value == b._value;
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
