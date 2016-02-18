/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		LinkedSynchronizationContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		LinkedSynchronizationContext
	Purpose:		Provides an internal context which can be synchronized to another
					context object.

***************************************************************************************/
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