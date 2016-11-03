namespace Manatee.Trello.Internal.Synchronization
{
	internal abstract class LinkedSynchronizationContext<TJson> : SynchronizationContext<TJson>
	{
#if IOS
		private System.Action _synchronizeRequestedInvoker;
		private System.Action _submitRequestedInvoker;

		public event System.Action SynchronizeRequested
		{
			add { _synchronizeRequestedInvoker += value; }
			remove { _synchronizeRequestedInvoker -= value; }
		}
		public event System.Action SubmitRequested
		{
			add { _submitRequestedInvoker += value; }
			remove { _submitRequestedInvoker -= value; }
		}
#else
		public event System.Action SynchronizeRequested;
		public event System.Action SubmitRequested;
#endif

		protected LinkedSynchronizationContext(TrelloAuthorization auth) : base(auth, false) {}

		protected override TJson GetData()
		{
#if IOS
			RaiseEvent(_synchronizeRequestedInvoker);
#else
			RaiseEvent(SynchronizeRequested);
#endif
			return Data;

		}
		public override void SetValue<T>(string property, T value)
		{
			base.SetValue(property, value);
#if IOS
			RaiseEvent(_submitRequestedInvoker);
#else
			RaiseEvent(SubmitRequested);
#endif
		}

		private static void RaiseEvent(System.Action action)
		{
			var handler = action;
			handler?.Invoke();
		}
	}
}