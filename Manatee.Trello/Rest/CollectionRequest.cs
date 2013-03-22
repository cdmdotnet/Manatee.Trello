using System.Collections.Generic;
using Manatee.Trello.Implementation;
using RestSharp;

namespace Manatee.Trello.Rest
{
	internal class CollectionRequest<T> : Request<T>
		where T : ExpiringObject
	{
		public CollectionRequest(IEnumerable<ExpiringObject> tokens, ExpiringObject entity = null)
			: base(tokens, entity) {}
	}
}