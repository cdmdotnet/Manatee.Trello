using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello.Internal
{
	internal interface IMergeJson<in T>
	{
		void Merge(T json, bool overwrite);
	}

	internal interface IRefreshEndpointSupplier : IRefreshable
	{
		TrelloAuthorization Auth { get; }
		Endpoint GetRefreshEndpoint();
	}
}