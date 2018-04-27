using System;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello.UnitTests
{
	public static class SafeRunner
	{
		private static readonly SemaphoreSlim RestLock = new SemaphoreSlim(1, 1);

		public static async Task Rest(Func<Task> run)
		{
			try
			{
				await RestLock.WaitAsync();

				await run();
			}
			finally
			{
				RestLock.Release();
			}
		}
	}
}