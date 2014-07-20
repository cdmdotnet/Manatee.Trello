namespace Manatee.Trello.Internal.Synchronization
{
	internal class LinkedSynchronizationContext<TJson> : SynchronizationContext<TJson>
	{
		public event System.Action SynchronizeRequested;
		public event System.Action SubmitRequested;

		public LinkedSynchronizationContext() : base(false) {}

		protected override TJson GetData()
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
			if (handler != null)
				handler();
		}
	}
}