/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ActionCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyActionCollection, CommentCollection
	Purpose:		Collection objects for actions.

***************************************************************************************/

using System;
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
	public class ReadOnlyActionCollection : ReadOnlyCollection<Action>
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
		/// Implement to provide data to the collection.
		/// </summary>
		protected override void Update()
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

		internal void AddFilter(IEnumerable<ActionType> actionTypes)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>{{"filter", string.Empty}};
			var filter = _additionalParameters.ContainsKey("filter") ? ((string)_additionalParameters["filter"]) : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += actionTypes.Select(a => a.GetDescription()).Join(",");
			_additionalParameters["filter"] = filter;
		}

		internal void AddFilter(DateTime? start, DateTime? end)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			if (start.HasValue)
				_additionalParameters["since"] = start.Value.ToUniversalTime().ToString("O");
			if (end.HasValue)
				_additionalParameters["before"] = end.Value.ToUniversalTime().ToString("O");
		}
	}

	/// <summary>
	/// A collection of comment actions.
	/// </summary>
	public class CommentCollection : ReadOnlyActionCollection
	{
		internal CommentCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(typeof (Card), getOwnerId, auth)
		{
			AddFilter(new[] {ActionType.CommentCard, ActionType.CopyCommentCard});
		}

		/// <summary>
		/// Posts a new comment to a card.
		/// </summary>
		/// <param name="text">The content of the comment.</param>
		/// <returns>The <see cref="Action"/> associated with the comment.</returns>
		public Action Add(string text)
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, text);
			if (error != null)
				throw new ValidationException<string>(text, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonComment>();
			json.Text = text;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddComment, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);

			return new Action(newData, Auth);
		}
	}
}