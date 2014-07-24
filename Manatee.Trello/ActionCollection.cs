/***************************************************************************************

	Copyright 2014 Greg Dennis

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
		internal ReadOnlyActionCollection(Type type, string ownerId)
			: base(ownerId)
		{
			_updateRequestType = _requestTypes[type];
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonAction>>(TrelloAuthorization.Default, endpoint).Where(Filter);

			Items.Clear();
			Items.AddRange(newData.Select(jc => jc.GetFromCache<Action>()));
		}
		/// <summary>
		/// Provides a filter delegate.
		/// </summary>
		/// <param name="action">The subject action.</param>
		/// <returns>True if the action is match, otherwise false.</returns>
		protected virtual bool Filter(IJsonAction action)
		{
			return true;
		}
	}

	/// <summary>
	/// A collection of comment actions.
	/// </summary>
	public class CommentCollection : ReadOnlyActionCollection
	{
		internal CommentCollection(string ownerId)
			: base(typeof (Card), ownerId) {}

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

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddComment);
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new Action(newData);
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Actions, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonAction>>(TrelloAuthorization.Default, endpoint).Where(Filter);

			Items.Clear();
			Items.AddRange(newData.Where(jc => jc.Type == ActionType.CommentCard).Select(jc => jc.GetFromCache<Action>()));
		}
		/// <summary>
		/// Provides a filter delegate.
		/// </summary>
		/// <param name="action">The subject action.</param>
		/// <returns>True if the action is match, otherwise false.</returns>
		protected override bool Filter(IJsonAction action)
		{
			return action.Type == ActionType.CommentCard;
		}
	}
}