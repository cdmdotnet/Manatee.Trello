using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Serialization;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	public class Position : IJsonCompatible
	{
		private PositionValue _value;

		public bool IsValid { get { return _value > PositionValue.Unknown; } }

		public Position(PositionValue value)
		{
			_value = value;
		}
		public Position(int value)
		{
			_value = (PositionValue) value;
		}

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
		public override string ToString()
		{
			return _value.ToLowerString();
		}
		public static implicit operator Position(PositionValue value)
		{
			return new Position(value);
		}
		public static implicit operator Position(int value)
		{
			return new Position(value);
		}
		public static explicit operator PositionValue(Position position)
		{
			return position._value;
		}
		public static implicit operator int(Position position)
		{
			return (int) position._value;
		}
		public static bool operator ==(Position a, Position b)
		{
			if (ReferenceEquals(a, b)) return true;
			if (Equals(a, null) && Equals(b, null)) return true;
			if (Equals(a, null) || Equals(b, null)) return false;
			return a._value == b._value;
		}
		public static bool operator !=(Position a, Position b)
		{
			return !(a == b);
		}
		public bool Equals(Position other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._value, _value);
		}
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Position)) return false;
			return Equals((Position) obj);
		}
		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}
	}
	public enum PositionValue
	{
		Unknown = -2,
		Top = -1,
		Bottom = 0
	}
}
