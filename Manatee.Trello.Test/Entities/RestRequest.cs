using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Internal;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Test.Entities
{
	class RestRequest : IRestRequest
	{
		public RestMethod Method { get; set; }
		public string Resource { get; set; }
		public IDictionary<string, object> Parameters { get; set; }
		public void AddParameter(string name, object value)
		{
			throw new NotImplementedException();
		}
		public void AddBody(object body)
		{
			throw new NotImplementedException();
		}
	}
	class QueuedRestRequest : IQueuedRestRequest
	{
		public IRestRequest Request { get; set; }
		public Type RequestedType { get; set; }
		public IRestResponse Response { get; set; }
		public bool CanContinue { get; set; }
	}
}
