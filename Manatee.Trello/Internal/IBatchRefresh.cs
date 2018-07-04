using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello.Internal
{
	internal interface IBatchRefresh : IRefreshable, ICacheable
	{
		TrelloAuthorization Auth { get; }
		Endpoint GetRefreshEndpoint();
		void Apply(string content);
	}
}