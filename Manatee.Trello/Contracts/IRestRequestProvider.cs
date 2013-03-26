using System.Collections.Generic;
using Manatee.Trello.Implementation;

namespace Manatee.Trello.Contracts
{
	public interface IRestRequestProvider
	{
		IRestRequest<T> Create<T>()
			where T : ExpiringObject, new();
		IRestRequest<T> Create<T>(string id)
			where T : ExpiringObject, new();
		IRestRequest<T> Create<T>(ExpiringObject obj)
			where T : ExpiringObject, new();
		IRestRequest<T> Create<T>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity = null, string urlExtension = null)
			where T : ExpiringObject, new();
		IRestCollectionRequest<T> CreateCollectionRequest<T>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity = null)
			where T : ExpiringObject, new();
	}
}