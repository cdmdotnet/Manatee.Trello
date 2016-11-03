using System.Collections.Generic;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class RequestQueue
	{
		private readonly Queue<QueuableRestRequest> _queue;

		public int Count
		{
			get
			{
				lock(_queue)
					return _queue.Count;
			}
		}

		public RequestQueue()
		{
			_queue = new Queue<QueuableRestRequest>();
		}

		public void Enqueue(IRestRequest request, object signal)
		{
			lock (_queue)
				_queue.Enqueue(new QueuableRestRequest(request, signal));
		}
		public void Enqueue<T>(IRestRequest request, object signal)
			where T : class
		{
			lock (_queue)
				_queue.Enqueue(new QueuableRestRequest<T>(request, signal));
		}
		public QueuableRestRequest Dequeue()
		{
			if (_queue.Count == 0) return null;
			lock (_queue)
				return _queue.Dequeue();
		}
	}
}