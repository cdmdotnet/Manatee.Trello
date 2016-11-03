using System;
using System.Threading;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class QueuableRestRequest
	{
		private readonly object _signal;
		public IRestRequest Request { get; }

		public QueuableRestRequest(IRestRequest request, object signal)
		{
			_signal = signal;
			Request = request;
		}

		public virtual void Execute(IRestClient client)
		{
			Request.Response = client.Execute(Request);
			Signal();
		}
		public virtual void CreateNullResponse(Exception e = null)
		{
			Request.Response = new NullRestResponse {Exception = e};
			Signal();
		}

		protected void Signal()
		{
			lock (_signal)
				Monitor.Pulse(_signal);
		}
	}

	internal class QueuableRestRequest<T> : QueuableRestRequest
		where T : class
	{
		public QueuableRestRequest(IRestRequest request, object signal)
			: base(request, signal) {}

		public override void Execute(IRestClient client)
		{
			Request.Response = client.Execute<T>(Request);
			Signal();
		}
		public override void CreateNullResponse(Exception e = null)
		{
			Request.Response = new NullRestResponse<T> {Exception = e};
			Signal();
		}
	}
}