﻿using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello
{
	/// <summary>
	/// Supports entity creation for dependency-injected applications.
	/// </summary>
	public interface ITrelloFactory
	{
		/// <summary>
		/// Creates an <see cref="IAction"/>.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IAction"/></returns>
		IAction Action(string id, TrelloAuthorization auth = null);
		/// <summary>
		/// Creates an <see cref="IBoard"/>.
		/// </summary>
		/// <param name="id">The board ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IBoard"/></returns>
		IBoard Board(string id, TrelloAuthorization auth = null);
		/// <summary>
		/// Creates an <see cref="ICard"/>.
		/// </summary>
		/// <param name="id">The board ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="ICard"/></returns>
		ICard Card(string id, TrelloAuthorization auth = null);
		/// <summary>
		/// Creates an <see cref="ICheckList"/>.
		/// </summary>
		/// <param name="id">The checklist ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="ICheckList"/></returns>
		ICheckList CheckList(string id, TrelloAuthorization auth = null);
		/// <summary>
		/// Creates an <see cref="IList"/>.
		/// </summary>
		/// <param name="id">The list ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IList"/></returns>
		IList List(string id, TrelloAuthorization auth = null);
		/// <summary>
		/// Creates an <see cref="IMe"/>.
		/// </summary>
		/// <returns>An <see cref="IMe"/></returns>
		IMe Me();
		/// <summary>
		/// Creates an <see cref="IMember"/>.
		/// </summary>
		/// <param name="id">The member ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IMember"/></returns>
		IMember Member(string id, TrelloAuthorization auth = null);
		/// <summary>
		/// Creates an <see cref="INotification"/>.
		/// </summary>
		/// <param name="id">The notification ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="INotification"/></returns>
		INotification Notification(string id, TrelloAuthorization auth = null);
		/// <summary>
		/// Creates an <see cref="IOrganization"/>.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IOrganization"/></returns>
		IOrganization Organization(string id, TrelloAuthorization auth = null);

		/// <summary>
		/// Creates an <see cref="ISearch"/>.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <param name="limit">(Optional) - The maximum number of items to return.</param>
		/// <param name="modelTypes">(Optional) - The model types desired.</param>
		/// <param name="context">(Optional) - The context (scope) of the search.</param>
		/// <param name="isPartial">(Optional) - Allow "starts with" matches.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="ISearch"/></returns>
		ISearch Search(SearchFor query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		               IEnumerable<IQueryable> context = null, bool isPartial = false, TrelloAuthorization auth = null);

		/// <summary>
		/// Creates an <see cref="ISearch"/>.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <param name="limit">(Optional) - The maximum number of items to return.</param>
		/// <param name="modelTypes">(Optional) - The model types desired.</param>
		/// <param name="context">(Optional) - The context (scope) of the search.</param>
		/// <param name="isPartial">(Optional) - Allow "starts with" matches.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="ISearch"/></returns>
		ISearch Search(string query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		               IEnumerable<IQueryable> context = null, bool isPartial = false, TrelloAuthorization auth = null);

		/// <summary>
		/// Creates an <see cref="IToken"/>.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IToken"/></returns>
		IToken Token(string id, TrelloAuthorization auth = null);

		/// <summary>
		/// Creates an <see cref="IWebhook{T}"/> and registers a new webhook with Trello.
		/// </summary>
		/// <param name="target">The action ID.</param>
		/// <param name="callBackUrl">A URL that Trello can POST to.</param>
		/// <param name="description">A description.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IWebhook{T}"/></returns>
		IWebhook<T> Webhook<T>(T target, string callBackUrl, string description = null, TrelloAuthorization auth = null)
			where T : class, ICanWebhook;

		/// <summary>
		/// Creates an <see cref="IWebhook{T}"/> for and existing webhook.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IWebhook{T}"/></returns>
		IWebhook<T> Webhook<T>(string id, TrelloAuthorization auth = null)
			where T : class, ICanWebhook;
	}

	/// <summary>
	/// Supports entity creation for dependency-injected applications.
	/// </summary>
	public class TrelloFactory : ITrelloFactory
	{
		/// <summary>
		/// Creates an <see cref="IAction"/>.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IAction"/></returns>
		public IAction Action(string id, TrelloAuthorization auth = null)
		{
			return new Action(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IBoard"/>.
		/// </summary>
		/// <param name="id">The board ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IBoard"/></returns>
		public IBoard Board(string id, TrelloAuthorization auth = null)
		{
			return new Board(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="ICard"/>.
		/// </summary>
		/// <param name="id">The board ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="ICard"/></returns>
		public ICard Card(string id, TrelloAuthorization auth = null)
		{
			return new Card(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="ICheckList"/>.
		/// </summary>
		/// <param name="id">The checklist ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="ICheckList"/></returns>
		public ICheckList CheckList(string id, TrelloAuthorization auth = null)
		{
			return new CheckList(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IList"/>.
		/// </summary>
		/// <param name="id">The list ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IList"/></returns>
		public IList List(string id, TrelloAuthorization auth = null)
		{
			return new List(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IMe"/>.
		/// </summary>
		/// <returns>An <see cref="IMe"/></returns>
		public IMe Me()
		{
			return Trello.Member.Me;
		}

		/// <summary>
		/// Creates an <see cref="IMember"/>.
		/// </summary>
		/// <param name="id">The member ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IMember"/></returns>
		public IMember Member(string id, TrelloAuthorization auth = null)
		{
			return new Member(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="INotification"/>.
		/// </summary>
		/// <param name="id">The notification ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="INotification"/></returns>
		public INotification Notification(string id, TrelloAuthorization auth = null)
		{
			return new Notification(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IOrganization"/>.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IOrganization"/></returns>
		public IOrganization Organization(string id, TrelloAuthorization auth = null)
		{
			return new Organization(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="ISearch"/>.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <param name="limit">(Optional) - The maximum number of items to return.</param>
		/// <param name="modelTypes">(Optional) - The model types desired.</param>
		/// <param name="context">(Optional) - The context (scope) of the search.</param>
		/// <param name="isPartial">(Optional) - Allow "starts with" matches.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="ISearch"/></returns>
		public ISearch Search(SearchFor query, int? limit = null, SearchModelType modelTypes = SearchModelType.All,
		                      IEnumerable<IQueryable> context = null, bool isPartial = false, TrelloAuthorization auth = null)
		{
			return new Search(query, limit, modelTypes, context, auth, isPartial);
		}

		/// <summary>
		/// Creates an <see cref="ISearch"/>.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <param name="limit">(Optional) - The maximum number of items to return.</param>
		/// <param name="modelTypes">(Optional) - The model types desired.</param>
		/// <param name="context">(Optional) - The context (scope) of the search.</param>
		/// <param name="isPartial">(Optional) - Allow "starts with" matches.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
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
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IToken"/></returns>
		public IToken Token(string id, TrelloAuthorization auth = null)
		{
			return new Token(id, auth);
		}

		/// <summary>
		/// Creates an <see cref="IWebhook{T}"/> and registers a new webhook with Trello.
		/// </summary>
		/// <param name="target">The action ID.</param>
		/// <param name="callBackUrl">A URL that Trello can POST to.</param>
		/// <param name="description">A description.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IWebhook{T}"/></returns>
		public IWebhook<T> Webhook<T>(T target, string callBackUrl, string description = null, TrelloAuthorization auth = null)
			where T : class, ICanWebhook
		{
			return new Webhook<T>(target, callBackUrl, description, auth);
		}

		/// <summary>
		/// Creates an <see cref="IWebhook{T}"/> for and existing webhook.
		/// </summary>
		/// <param name="id">The action ID.</param>
		/// <param name="auth">(Optional) - The authorization.</param>
		/// <returns>An <see cref="IWebhook{T}"/></returns>
		public IWebhook<T> Webhook<T>(string id, TrelloAuthorization auth = null)
			where T : class, ICanWebhook
		{
			return new Webhook<T>(id, auth);
		}
	}
}
