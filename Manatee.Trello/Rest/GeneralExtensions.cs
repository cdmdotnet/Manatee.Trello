using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manatee.Trello.Rest
{
	internal static class GeneralExtensions
	{
		public static bool In<T>(this T item, IEnumerable<T> items)
		{
			return items.Contains(item);
		}
		public static bool In<T>(this T item, params T[] items)
		{
			return items.Contains(item);
		}
		public static string ToLowerString<T>(this T item)
		{
			return item.ToString().ToLower();
		}
	}
}
