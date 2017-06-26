using System;
using System.Threading;

namespace Manatee.Trello.Internal
{
	internal static class TimerExtensions
	{
		public static void Stop(this Timer timer)
		{
			timer?.Change(TimeSpan.FromDays(30), TimeSpan.FromDays(30));
		}

		public static void Start(this Timer timer, TimeSpan period)
		{
			timer?.Change(TimeSpan.Zero, period);
		}
	}
}