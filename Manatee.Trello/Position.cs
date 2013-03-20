using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Serialization;

namespace Manatee.Trello
{
	public class Position : IJsonCompatible
	{
		private PositionValue _value;

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
			return _value.ToString().ToLower();
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
	}
	public enum PositionValue
	{
		Unknown = -2,
		Top = -1,
		Bottom = 0
	}
}
