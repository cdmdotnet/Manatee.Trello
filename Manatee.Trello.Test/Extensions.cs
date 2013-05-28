using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Manatee.Trello.Test
{
	static class Extensions
	{
		public static DateTime TruncateToMilliSeconds(this DateTime date)
		{
			return new SqlDateTime(date).Value;
		}
	}
}
