using System;

namespace Manatee.Trello.IntegrationTests
{
	public static class DateTimeExtensions
	{
		public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
		{
			if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
			return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
		}
	}
}