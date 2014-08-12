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
 
	File Name:		GeneralExtensions.cs
	Namespace:		Manatee.Trello.IEntityProvider
	Class Name:		GeneralExtensions
	Purpose:		A set of general extension methods used throughout the
					project.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson
{
	internal static class GeneralExtensions
	{
		public static string ToLowerString<T>(this T item)
		{
			return item.ToString().ToLower();
		}
		public static bool IsNullOrWhiteSpace(this string value)
		{
#if NET35 || NET35C
			return string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim());
#elif NET4 || NET4C || NET45
			return string.IsNullOrWhiteSpace(value);
#endif
		}
		public static string Join(this IEnumerable<string> segments, string separator)
		{
#if NET35 || NET35C
			return string.Join(separator, segments.ToArray());
#elif NET4 || NET4C || NET45
			return string.Join(separator, segments);
#endif
		}
		public static T Deserialize<T>(this JsonObject obj, JsonSerializer serializer, string key)
		{
			return obj.ContainsKey(key) ? serializer.Deserialize<T>(obj[key]) : default(T);
		}
		public static void Serialize<T>(this T obj, JsonObject json, JsonSerializer serializer, string key)
		{
			if (!Equals(obj, default(T)))
				json[key] = serializer.Serialize(obj);
		}
		public static void SerializeId<T>(this T obj, JsonObject json, string key)
			where T : IJsonCacheable
		{
			if (!Equals(obj, default(T)))
				json[key] = obj.Id;
		}
		public static T Combine<T>(this IEnumerable<T> values)
			where T : struct
		{
			return (T) Enum.ToObject(typeof (T), values.Select(value => Convert.ToInt32(value))
			                                           .Aggregate(0, (current, longValue) => current + longValue));
		}
	}
}
