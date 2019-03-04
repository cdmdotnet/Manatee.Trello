using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Manatee.Trello
{
	public static partial class Emojis
	{
		private static readonly object _lock = new object();
		private static Dictionary<string, Emoji> _lookup;

		internal static Emoji GetByUnicodeId(string unicodeId)
		{
			_EnsureLookup();

			return _lookup.TryGetValue(unicodeId, out var emoji) ? emoji : null;
		}

		private static void _EnsureLookup()
		{
			if (_lookup != null) return;

			lock (_lock)
			{
				if (_lookup != null) return;

				var fields = typeof(Emojis).GetTypeInfo().DeclaredFields
				                           .Where(f => f.IsPublic);

				_lookup = fields.Select(f => (Emoji) f.GetValue(null))
				                .ToDictionary(f => f.Unified, f => f);
			}
		}
	}
}