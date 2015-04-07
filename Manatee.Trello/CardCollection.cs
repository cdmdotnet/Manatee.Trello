﻿/***************************************************************************************

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
 
	File Name:		CardCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyCardCollection, CardCollection
	Purpose:		Collection objects for cards.

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
	/// A read-only collection of cards.
	/// </summary>
	public class ReadOnlyCardCollection : ReadOnlyCollection<Card>
	{
		private readonly TrelloAuthorization _auth;
		private readonly EntityRequestType _updateRequestType;
		private readonly Dictionary<string, object> _requestParameters;
		private Dictionary<string, object> _additionalParameters; 

		/// <summary>
		/// Retrieves a card which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching card, or null if none found.</returns>
		/// <remarks>
		/// Matches on Card.Id and Card.Name.  Comparison is case-sensitive.
		/// </remarks>
		public Card this[string key] { get { return GetByKey(key); } }

		internal TrelloAuthorization Auth { get { return _auth; } }

		internal ReadOnlyCardCollection(Type type, string ownerId, TrelloAuthorization auth)
			: base(ownerId)
		{
			_auth = auth ?? TrelloAuthorization.Default;
			_updateRequestType = type == typeof (List)
				                     ? EntityRequestType.List_Read_Cards
				                     : EntityRequestType.Board_Read_Cards;
			_requestParameters = new Dictionary<string, object> {{"_id", ownerId}};
		}
		internal ReadOnlyCardCollection(EntityRequestType requestType, string ownerId, TrelloAuthorization auth, Dictionary<string, object> requestParameters = null)
			: base(ownerId)
		{
			_updateRequestType = requestType;
			_auth = auth ?? TrelloAuthorization.Default;
			_requestParameters = requestParameters ?? new Dictionary<string, object>();
			_requestParameters.Add("_id", ownerId);
		}
		internal ReadOnlyCardCollection(ReadOnlyCardCollection source, TrelloAuthorization auth)
			: base(source.OwnerId)
		{
			_auth = auth ?? TrelloAuthorization.Default;
			_updateRequestType = source._updateRequestType;
			_requestParameters = source._requestParameters;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, _requestParameters);
			var newData = JsonRepository.Execute<List<IJsonCard>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jc =>
				{
					var card = jc.GetFromCache<Card>(Auth);
					card.Json = jc;
					return card;
				}));
		}

		internal void SetFilter(CardFilter cardStatus)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			_additionalParameters["filter"] = cardStatus.GetDescription();
		}

		private Card GetByKey(string key)
		{
			return this.FirstOrDefault(c => key.In(c.Id, c.Name));
		}
	}

	/// <summary>
	/// A collection of cards.
	/// </summary>
	public class CardCollection : ReadOnlyCardCollection
	{
		internal CardCollection(string ownerId, TrelloAuthorization auth)
			: base(typeof (List), ownerId, auth) {}

		/// <summary>
		/// Creates a new card.
		/// </summary>
		/// <param name="name">The name of the card to add.</param>
		/// <returns>The <see cref="Card"/> generated by Trello.</returns>
		public Card Add(string name)
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, name);
			if (error != null)
				throw new ValidationException<string>(name, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCard>();
			json.Name = name;

			return CreateCard(json);
		}
		/// <summary>
		/// Creates a new card by copying a card.
		/// </summary>
		/// <param name="source">A card to copy.  Default is null.</param>
		/// <returns>The <see cref="Card"/> generated by Trello.</returns>
		public Card Add(Card source)
		{
			var error = NotNullRule<Card>.Instance.Validate(null, source);
			if (error != null)
				throw new ValidationException<Card>(source, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCard>();
			json.CardSource = source.Json;

			return CreateCard(json);
		}
		/// <summary>
		/// Creates a new card by importing data from a URL.
		/// </summary>
		/// <param name="name">The name of the card to add.</param>
		/// <param name="sourceUrl"></param>
		/// <returns></returns>
		public Card Add(string name, string sourceUrl)
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, name);
			if (error != null)
				throw new ValidationException<string>(name, new[] { error });
			error = UriRule.Instance.Validate(null, sourceUrl);
			if (error != null)
				throw new ValidationException<string>(sourceUrl, new[] { error });

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCard>();
			json.Name = name;
			json.UrlSource = sourceUrl;

			return CreateCard(json);
		}

		private Card CreateCard(IJsonCard json)
		{
			json.List = TrelloConfiguration.JsonFactory.Create<IJsonList>();
			json.List.Id = OwnerId;

			var endpoint = EndpointFactory.Build(EntityRequestType.List_Write_AddCard);
			var newData = JsonRepository.Execute(Auth, endpoint, json);

			return new Card(newData, Auth);
		}
	}
}