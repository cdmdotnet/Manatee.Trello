using System;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello.Internal.Synchronization
{
	internal abstract class LinkedSynchronizationContext<TJson> : SynchronizationContext<TJson>
		where TJson : class
	{
		public Func<CancellationToken, Task> SynchronizeRequested;
		public Func<CancellationToken, Task> SubmitRequested;

		protected LinkedSynchronizationContext(TrelloAuthorization auth) : base(auth, false) {}

		protected override async Task<TJson> GetData(CancellationToken ct)
		{
			await RequestSync(SynchronizeRequested, ct);
			return Data;

		}
		public override async Task SetValue<T>(string property, T value, CancellationToken ct)
		{
			await base.SetValue(property, value, ct);
			await RequestSync(SubmitRequested, ct);
		}

		private static async Task RequestSync(Func<CancellationToken, Task> action, CancellationToken ct)
		{
			var handler = action;
			if (handler != null)
				await handler(ct);
		}
	}
}