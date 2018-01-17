using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello
{
	public interface ITrelloFactory
	{
		IAction Action(string id, TrelloAuthorization auth = null);
		IBoard Board(string id, TrelloAuthorization auth = null);
		ICard Card(string id, TrelloAuthorization auth = null);
		ICheckList CheckList(string id, TrelloAuthorization auth = null);
		IList List(string id, TrelloAuthorization auth = null);
		IMember Member(string id, TrelloAuthorization auth = null);
		INotification Notification(string id, TrelloAuthorization auth = null);
		IOrganization Organization(string id, TrelloAuthorization auth = null);

		ISearch Search(SearchFor query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		               IEnumerable<IQueryable> context = null, TrelloAuthorization auth = null, bool isPartial = false);

		ISearch Search(string query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		               IEnumerable<IQueryable> context = null, TrelloAuthorization auth = null, bool isPartial = false);

		IToken Token(string id, TrelloAuthorization auth = null);

		IWebhook<T> Webhook<T>(string id, TrelloAuthorization auth = null)
			where T : class, ICanWebhook;
	}

	public class TrelloFactory : ITrelloFactory
	{
		public IAction Action(string id, TrelloAuthorization auth = null)
		{
			return new Action(id, auth);
		}

		public IBoard Board(string id, TrelloAuthorization auth = null)
		{
			return new Board(id, auth);
		}

		public ICard Card(string id, TrelloAuthorization auth = null)
		{
			return new Card(id, auth);
		}

		public ICheckList CheckList(string id, TrelloAuthorization auth = null)
		{
			return new CheckList(id, auth);
		}

		public IList List(string id, TrelloAuthorization auth = null)
		{
			return new List(id, auth);
		}

		public IMember Member(string id, TrelloAuthorization auth = null)
		{
			return new Member(id, auth);
		}

		public INotification Notification(string id, TrelloAuthorization auth = null)
		{
			return new Notification(id, auth);
		}

		public IOrganization Organization(string id, TrelloAuthorization auth = null)
		{
			return new Organization(id, auth);
		}

		public ISearch Search(SearchFor query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		                      IEnumerable<IQueryable> context = null, TrelloAuthorization auth = null, bool isPartial = false)
		{
			return new Search(query, limit, modelTypes, context, auth, isPartial);
		}

		public ISearch Search(string query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		                      IEnumerable<IQueryable> context = null, TrelloAuthorization auth = null, bool isPartial = false)
		{
			return new Search(query, limit, modelTypes, context, auth, isPartial);
		}

		public IToken Token(string id, TrelloAuthorization auth = null)
		{
			return new Token(id, auth);
		}

		public IWebhook<T> Webhook<T>(string id, TrelloAuthorization auth = null)
			where T : class, ICanWebhook
		{
			return new Webhook<T>(id, auth);
		}
	}
}
