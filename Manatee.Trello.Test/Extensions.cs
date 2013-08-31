using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Manatee.Trello.Test
{
	public static class Extensions
	{
		public static DateTime TruncateToMilliseconds(this DateTime date)
		{
			return new SqlDateTime(date).Value;
		}
	}
}
