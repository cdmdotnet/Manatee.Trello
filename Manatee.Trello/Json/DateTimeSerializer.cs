using System;
using System.Globalization;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json
{
	internal class DateTimeSerializer : Manatee.Json.Serialization.ISerializer
	{
		public bool ShouldMaintainReferences => false;

		public bool Handles(SerializationContextBase context)
		{
			return context.InferredType == typeof(DateTime);
		}

		public JsonValue Serialize(SerializationContext context)
		{
			var date = (DateTime) context.Source;

			var dateString = date.ToUniversalTime()
			                     .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

			return dateString;
		}

		public object Deserialize(DeserializationContext context)
		{
			var dateString = context.LocalValue.String;
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var date))
			{
				var localDate = date.ToLocalTime();

				return localDate;
			}

			return DateTime.MinValue;
		}
	}
}