using Manatee.Trello.Rest;

namespace Manatee.Trello.Contracts
{
	public interface IExpires
	{
		string Id { get; }
		IExpires Owner { get; set; }
		ParameterCollection Parameters { get; }
		bool IsExpired { get; }
		TrelloService Svc { get; set; }

		bool Match(string id);
	}
}