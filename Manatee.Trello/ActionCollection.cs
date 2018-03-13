﻿using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of actions.
	/// </summary>
	public interface IReadOnlyActionCollection : IReadOnlyCollection<IAction>
	{
		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="actionType">The action type.</param>
		void Filter(ActionType actionType);
		/// <summary>
		/// Adds a number of filters to the collection.
		/// </summary>
		/// <param name="actionTypes">A collection of action types.</param>
		void Filter(IEnumerable<ActionType> actionTypes);
		/// <summary>
		/// Adds a date-based filter to the collection.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="end">The end date.</param>
		void Filter(DateTime? start, DateTime? end);
	}

	/// <summary>
	/// A read-only collection of actions.
	/// </summary>
	public class ReadOnlyActionCollection : ReadOnlyCollection<IAction>, IReadOnlyActionCollection
	{
		private static readonly Dictionary<Type, EntityRequestType> _requestTypes;
		private readonly EntityRequestType _updateRequestType;
		private Dictionary<string, object> _additionalParameters;

		static ReadOnlyActionCollection()
		{
			_requestTypes = new Dictionary<Type, EntityRequestType>
				{
					{typeof(Board), EntityRequestType.Board_Read_Actions},
					{typeof(Card), EntityRequestType.Card_Read_Actions},
					{typeof(List), EntityRequestType.List_Read_Actions},
					{typeof(Member), EntityRequestType.Member_Read_Actions},
					{typeof(Organization), EntityRequestType.Organization_Read_Actions},
				};
		}
		internal ReadOnlyActionCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = _requestTypes[type];
			_additionalParameters = new Dictionary<string, object>();
		}
		internal ReadOnlyActionCollection(ReadOnlyActionCollection source, TrelloAuthorization auth)
			: base(() => source.OwnerId, auth)
		{
			_updateRequestType = source._updateRequestType;
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="actionType">The action type.</param>
		public void Filter(ActionType actionType)
		{
			var actionTypes = actionType.GetFlags();
			Filter(actionTypes);
		}

		/// <summary>
		/// Adds a number of filters to the collection.
		/// </summary>
		/// <param name="actionTypes">A collection of action types.</param>
		public void Filter(IEnumerable<ActionType> actionTypes)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>{{"filter", string.Empty}};
			var filter = _additionalParameters.ContainsKey("filter") ? (string)_additionalParameters["filter"] : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			var actionType = actionTypes.Aggregate(ActionType.Unknown, (c, a) => c | a);
			filter += actionType.ToString();
			_additionalParameters["filter"] = filter;
		}

		/// <summary>
		/// Adds a date-based filter to the collection.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="end">The end date.</param>
		public void Filter(DateTime? start, DateTime? end)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			if (start.HasValue)
				_additionalParameters["since"] = start.Value.ToUniversalTime().ToString("O");
			if (end.HasValue)
				_additionalParameters["before"] = end.Value.ToUniversalTime().ToString("O");
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			IncorporateLimit(_additionalParameters);

			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonAction>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(ja =>
				{
					var action = ja.GetFromCache<Action>(Auth);
					action.Json = ja;
					return action;
				}));
		}
	}

	/// <summary>
	/// A collection of comment actions.
	/// </summary>
	public interface ICommentCollection : IReadOnlyActionCollection
	{
		/// <summary>
		/// Posts a new comment to a card.
		/// </summary>
		/// <param name="text">The content of the comment.</param>
		/// <returns>The <see cref="Action"/> associated with the comment.</returns>
		IAction Add(string text);
	}

	/// <summary>
	/// A collection of comment actions.
	/// </summary>
	public class CommentCollection : ReadOnlyActionCollection, ICommentCollection
	{
		internal CommentCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(typeof (Card), getOwnerId, auth)
		{
			Filter(ActionType.CommentCard | ActionType.CopyCommentCard);
		}

		/// <summary>
		/// Posts a new comment to a card.
		/// </summary>
		/// <param name="text">The content of the comment.</param>
		/// <returns>The <see cref="Action"/> associated with the comment.</returns>
		public IAction Add(string text)
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, text);
			if (error != null)
				throw new ValidationException<string>(text, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonAction>();
			json.Text = text;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddComment, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);

			return new Action(newData, Auth);
		}
	}
}