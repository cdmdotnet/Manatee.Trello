using System.Threading.Tasks;

namespace Manatee.Trello.Internal.Synchronization
{
	internal abstract class LinkedSynchronizationContext<TJson> : SynchronizationContext<TJson>
		where TJson : class
	{
		public event System.Action SynchronizeRequested;
		public event System.Action SubmitRequested;

		protected LinkedSynchronizationContext(TrelloAuthorization auth) : base(auth, false) {}

		protected override async Task<TJson> GetData()
		{
			RaiseEvent(SynchronizeRequested);
			return Data;

		}
		public override void SetValue<T>(string property, T value)
		{
			base.SetValue(property, value);
			RaiseEvent(SubmitRequested);
		}

		private static void RaiseEvent(System.Action action)
		{
			var handler = action;
			handler?.Invoke();
		}
	}
}