using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manatee.Trello
{
	public static class Options
	{
		public static readonly TimeSpan DefaultItemDuration;

		public static TimeSpan ItemDuration { get; set; }
		public static bool AutoRefresh { get; set; }

		static Options()
		{
			DefaultItemDuration = TimeSpan.FromSeconds(60);
			ItemDuration = DefaultItemDuration;
			AutoRefresh = true;
		}
	}
}
