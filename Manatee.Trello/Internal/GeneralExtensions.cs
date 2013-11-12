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
using System.Text;

namespace Manatee.Trello.Internal
{
	internal static class GeneralExtensions
	{
		public static bool In<T>(this T item, params T[] items)
		{
			return items.Contains(item);
		}
		public static string ToLowerString<T>(this T item)
		{
			return item.ToString().ToLower();
		}
		public static string ToParameterString<T>(this T types)
		{
			return types.ToLowerString().Replace(" ", String.Empty);
		}
		public static bool BetweenInclusive(this IComparable value, object low, object high)
		{
			return (value.CompareTo(low) >= 0) && (value.CompareTo(high) <= 0);
		}
		public static string CSharpName(this Type type)
		{
			var sb = new StringBuilder();
			var name = type.Name;
			if (!type.IsGenericType) return name;
			sb.Append(name.Substring(0, name.IndexOf('`')));
			sb.Append("<");
			sb.Append(String.Join(", ", type.GetGenericArguments().Select(t => t.CSharpName())));
			sb.Append(">");
			return sb.ToString();
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
	}
}
