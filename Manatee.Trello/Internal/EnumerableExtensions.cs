using System;
using System.Collections.Generic;

namespace Manatee.Trello.Internal
{
	internal static class EnumerableExtensions
	{
		public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
			if (action == null) throw new ArgumentNullException(nameof(action));

			foreach (var item in enumerable)
			{
				action(item);
			}
		}
	}
}
