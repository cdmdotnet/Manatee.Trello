using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Manatee.Trello.Internal
{
	internal static class GeneralExtensions
	{
		private class Description
		{
			public object Value { get; set; }
			public string String { get; set; }
		}

		private static readonly Dictionary<Type, List<Description>> Descriptions = new Dictionary<Type, List<Description>>();
		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
		private static readonly DateTime TrelloMinDate = DateTime.MinValue.ToUniversalTime().AddHours(12);

		public static string ToLowerString<T>(this T item)
		{
			return item.ToString().ToLower();
		}
		public static void Replace<T>(this List<T> list, T replace, T with)
		{
			var i = list.IndexOf(replace);
			if (i == -1) return;
			list[i] = with;
		}
		public static bool BeginsWith(this string str, string beginning)
		{
			return (beginning.Length > str.Length) || (str.Substring(0, beginning.Length) == beginning);
		}
		public static bool IsNullOrWhiteSpace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}
		public static string Join(this IEnumerable<string> segments, string separator)
		{
			return string.Join(separator, segments);
		}
		public static string GetDescription<T>(this T enumerationValue)
			where T : struct
		{
			var type = enumerationValue.GetType();
			if (!type.GetTypeInfo().IsEnum)
				throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));

			EnsureDescriptions<T>();

			var attributes = typeof(T).GetTypeInfo().GetCustomAttributes(typeof(FlagsAttribute), false);
			return !attributes.Any()
				? Descriptions[typeof(T)].First(d => Equals(d.Value, enumerationValue)).String
				: BuildFlagsValues(enumerationValue, ",");
		}

		public static bool IsNotFoundError(this TrelloInteractionException e)
		{
			return e.InnerException != null && e.InnerException.Message.ToLower().Contains("not found");
		}
		public static string FlagsEnumToCommaList<T>(this T value)
		{
			return value.ToLowerString().Replace(" ", string.Empty);
		}
		public static bool In<T>(this T obj, params T[] values)
		{
			return values.Contains(obj);
		}
		public static DateTime ExtractCreationDate(this string id)
		{
			if (id.IsNullOrWhiteSpace())
				throw new InvalidOperationException("Cannot extract creation date until ID is downloaded.");
			var asHex = id.Substring(0, 8);
			var timeStamp = int.Parse(asHex, NumberStyles.HexNumber);
			return UnixEpoch.AddSeconds(timeStamp);
		}
		public static DateTime Encode(this DateTime date)
		{
			return date <= TrelloMinDate ? TrelloMinDate : date;
		}
		public static DateTime Decode(this DateTime date)
		{
			return date == TrelloMinDate ? DateTime.MinValue : date;
		}

		private static void EnsureDescriptions<T>()
		{
			var type = typeof(T);
			if (Descriptions.ContainsKey(type)) return;
			lock (Descriptions)
			{
				if (Descriptions.ContainsKey(type)) return;
				var names = Enum.GetValues(type).Cast<T>();
				var descriptions = names.Select(n => new Description {Value = n, String = GetDescription<T>(n.ToString())}).ToList();
				Descriptions.Add(type, descriptions);
			}
		}
		private static string GetDescription<T>(string name)
		{
			var type = typeof(T);
			var memInfo = type.GetTypeInfo().GetDeclaredField(name);
			var attributes = memInfo?.GetCustomAttributes(typeof(DisplayAttribute), false).ToList();
			return attributes != null && attributes.Any() ? ((DisplayAttribute)attributes[0]).Description : name;
		}
		private static string BuildFlagsValues<T>(T obj, string separator)
		{
			var descriptions = Descriptions[typeof(T)];
			var value = Convert.ToInt64(obj);
			var index = descriptions.Count - 1;
			var names = new List<string>();
			while (value > 0 && index >= 0)
			{
				var compare = Convert.ToInt64(descriptions[index].Value);
				if (value >= compare)
				{
					names.Insert(0, descriptions[index].String);
					value -= compare;
				}
				index--;
			}
			return names.Distinct().Join(separator);
		}
		public static IEnumerable<Enum> GetFlags(this Enum input)
		{
			foreach (Enum value in Enum.GetValues(input.GetType()))
				if (input.HasFlag(value))
					yield return value;
		}
	}
}
