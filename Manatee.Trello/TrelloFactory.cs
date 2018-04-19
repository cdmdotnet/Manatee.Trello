using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Supports entity creation for dependency-injected applications.
	/// </summary>
	public class TrelloFactory : ITrelloFactory
	{
		/// <summary>
		/// Creates an <see cref="IAction"/>.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="IAction"/></returns>
		public IAction Action(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<Action>(a => a.Id == id) ?? new Action(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IBoard"/>.
		/// </summary>
		/// <param name="id">The board ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="IBoard"/></returns>
		public IBoard Board(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<Board>(a => a.Id == id) ?? new Board(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="ICard"/>.
		/// </summary>
		/// <param name="id">The board ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="ICard"/></returns>
		public ICard Card(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<Card>(a => a.Id == id) ?? new Card(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="ICheckList"/>.
		/// </summary>
		/// <param name="id">The checklist ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="ICheckList"/></returns>
		public ICheckList CheckList(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<CheckList>(a => a.Id == id) ?? new CheckList(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IList"/>.
		/// </summary>
		/// <param name="id">The list ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="IList"/></returns>
		public IList List(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<List>(a => a.Id == id) ?? new List(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IMe"/>.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>An <see cref="IMe"/></returns>
		/// <remarks>
		/// This performs a call to the API to get the member authorized by <see cref="TrelloAuthorization.Default"/>.
		/// </remarks>
		public async Task<IMe> Me(CancellationToken ct = default(CancellationToken))
		{
			var id = await Trello.Me.GetId(ct);
			return TrelloConfiguration.Cache.Find<Me>(a => a.Id == id) ?? new Me(id);
		}

		/// <summary>
		/// Creates an <see cref="IMember"/>.
		/// </summary>
		/// <param name="id">The member ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="IMember"/></returns>
		public IMember Member(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<Member>(a => a.Id == id) ?? new Member(id, auth);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="IMemberSearch"/> object and performs the search.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="limit">(Optional) The result limit.  Can be a value from 1 to 20. The default is 8.</param>
		/// <param name="board">(Optional) A board to which the search should be limited.</param>
		/// <param name="organization">(Optional) An organization to which the search should be limited.</param>
		/// <param name="restrictToOrganization">(Optional) Restricts the search to only organization members.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public IMemberSearch MemberSearch(string query, int? limit = null, IBoard board = null, IOrganization organization = null,
		                                  bool? restrictToOrganization = null, TrelloAuthorization auth = null)
		{
			return new MemberSearch(query, limit, board, organization, restrictToOrganization, auth);
		}

		/// <summary>
		/// Creates an <see cref="INotification"/>.
		/// </summary>
		/// <param name="id">The notification ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="INotification"/></returns>
		public INotification Notification(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<Notification>(a => a.Id == id) ?? new Notification(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IOrganization"/>.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="IOrganization"/></returns>
		public IOrganization Organization(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<Organization>(a => a.Id == id) ?? new Organization(id, auth);
		}

		/// <summary>
		/// Creates a new empty <see cref="ISearchQuery"/>.
		/// </summary>
		/// <returns>An <see cref="ISearchQuery"/></returns>
		public ISearchQuery SearchQuery()
		{
			return new SearchQuery();
		}

		/// <summary>
		/// Creates an <see cref="ISearch"/>.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <param name="limit">(Optional) The maximum number of items to return.</param>
		/// <param name="modelTypes">(Optional) The model types desired.</param>
		/// <param name="context">(Optional) The context (scope) of the search.</param>
		/// <param name="isPartial">(Optional) Allow "starts with" matches.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="ISearch"/></returns>
		public ISearch Search(ISearchQuery query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		                      IEnumerable<IQueryable> context = null, bool isPartial = false, TrelloAuthorization auth = null)
		{
			return new Search(query, limit, modelTypes, context, auth, isPartial);
		}

		/// <summary>
		/// Creates an <see cref="ISearch"/>.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <param name="limit">(Optional) The maximum number of items to return.</param>
		/// <param name="modelTypes">(Optional) The model types desired.</param>
		/// <param name="context">(Optional) The context (scope) of the search.</param>
		/// <param name="isPartial">(Optional) Allow "starts with" matches.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="ISearch"/></returns>
		public ISearch Search(string query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		                      IEnumerable<IQueryable> context = null, bool isPartial = false, TrelloAuthorization auth = null)
		{
			return new Search(query, limit, modelTypes, context, auth, isPartial);
		}

		/// <summary>
		/// Creates an <see cref="IToken"/>.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="IToken"/></returns>
		public IToken Token(string id, TrelloAuthorization auth = null)
		{
			return TrelloConfiguration.Cache.Find<Token>(a => a.Id == id) ?? new Token(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IWebhook{T}"/> and registers a new webhook with Trello.
		/// </summary>
		/// <param name="target">The action ID.</param>
		/// <param name="callBackUrl">A URL that Trello can POST to.</param>
		/// <param name="description">A description.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>An <see cref="IWebhook{T}"/></returns>
		public async Task<IWebhook<T>> Webhook<T>(T target, string callBackUrl, string description = null,
		                                          TrelloAuthorization auth = null,
		                                          CancellationToken ct = default(CancellationToken))
			where T : class, ICanWebhook
		{
			return await Trello.Webhook<T>.Create(target, callBackUrl, description, auth, ct);
		}

		/// <summary>
		/// Creates an <see cref="IWebhook{T}"/> for and existing webhook.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) The authorization.</param>
		/// <returns>An <see cref="IWebhook{T}"/></returns>
		public IWebhook<T> Webhook<T>(string id, TrelloAuthorization auth = null)
			where T : class, ICanWebhook
		{
			return TrelloConfiguration.Cache.Find<Webhook<T>>(a => a.Id == id) ?? new Webhook<T>(id, auth);
		}
	}
}
