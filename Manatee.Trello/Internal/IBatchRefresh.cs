using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello.Internal
{
	internal interface IBatchRefresh : IBatchRefreshable, ICacheable
	{
		TrelloAuthorization Auth { get; }
		Endpoint GetRefreshEndpoint();
		void Apply(string content);
	}
}