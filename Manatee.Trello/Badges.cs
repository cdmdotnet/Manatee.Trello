/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Badges.cs
	Namespace:		Manatee.Trello
	Class Name:		Badges
	Purpose:		Represents the set of badges shown on the card cover (when viewed
					in a list) on Trello.com.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "badges":{
	//      "votes":0,
	//      "viewingMemberVoted":false,
	//      "subscribed":false,
	//      "fogbugz":"",
	//      "due":null,
	//      "description":true,
	//      "comments":0,
	//      "checkItemsChecked":0,
	//      "checkItems":4,
	//      "attachments":0
	//   },
	///<summary>
	/// Represents the set of badges shown on the card cover.
	///</summary>
	public class Badges : JsonCompatibleExpiringObject
	{
		private int? _attachments;
		private int? _checkItems;
		private int? _checkItemsChecked;
		private int? _comments;
		private DateTime? _dueDate;
		private string _fogBugz;
		private bool? _hasDescription;
		private bool? _isSubscribed;
		private bool? _viewingMemberVoted;
		private int? _votes;

		///<summary>
		/// Indicates the number of attachments.
		///</summary>
		public int? Attachments
		{
			get
			{
				VerifyNotExpired();
				return _attachments;
			}
		}
		/// <summary>
		/// Indicates the number of check items.
		/// </summary>
		public int? CheckItems
		{
			get
			{
				VerifyNotExpired();
				return _checkItems;
			}
		}
		/// <summary>
		/// Indicates the number of check items which have been checked.
		/// </summary>
		public int? CheckItemsChecked
		{
			get
			{
				VerifyNotExpired();
				return _checkItemsChecked;
			}
		}
		/// <summary>
		/// Indicates the number of comments.
		/// </summary>
		public int? Comments
		{
			get
			{
				VerifyNotExpired();
				return _comments;
			}
		}
		/// <summary>
		/// Indicates the due date, if one exists.
		/// </summary>
		public DateTime? DueDate
		{
			get
			{
				VerifyNotExpired();
				return _dueDate;
			}
		}
		/// <summary>
		/// Indicates the FogBugz ID.
		/// </summary>
		public string FogBugz
		{
			get
			{
				VerifyNotExpired();
				return _fogBugz;
			}
		}
		/// <summary>
		/// Indicates whether the card has a description.
		/// </summary>
		public bool? HasDescription
		{
			get
			{
				VerifyNotExpired();
				return _hasDescription;
			}
		}
		/// <summary>
		/// Indicates whether the member is subscribed to the card.
		/// </summary>
		public bool? IsSubscribed
		{
			get
			{
				VerifyNotExpired();
				return _isSubscribed;
			}
		}
		/// <summary>
		/// Indicates whether the member has voted for this card.
		/// </summary>
		public bool? ViewingMemberVoted
		{
			get
			{
				VerifyNotExpired();
				return _viewingMemberVoted;
			}
		}
		/// <summary>
		/// Indicates the number of votes.
		/// </summary>
		public int? Votes
		{
			get
			{
				VerifyNotExpired();
				return _votes;
			}
		}

		internal override string Key { get { return "badges"; } }

		///<summary>
		/// Creates a new instance of the Badges class.
		///</summary>
		public Badges() {}
		internal Badges(ITrelloRest svc, Card owner)
			: base(svc, owner) {}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_attachments = (int?) obj.TryGetNumber("attachments");
			_checkItems = (int?) obj.TryGetNumber("checkItems");
			_checkItemsChecked = (int?) obj.TryGetNumber("checkItemsChecked");
			_comments = (int?) obj.TryGetNumber("comments");
			var due = obj.TryGetString("due");
			_dueDate = string.IsNullOrWhiteSpace(due) ? (DateTime?) null : DateTime.Parse(due);
			_fogBugz = obj.TryGetString("fogBugz");
			_hasDescription = obj.TryGetBoolean("desc");
			_isSubscribed = obj.TryGetBoolean("subscribed");
			_viewingMemberVoted = obj.TryGetBoolean("viewingMemberVoted");
			_votes = (int?) obj.TryGetNumber("votes");
			_isInitialized = true;
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			if (!_isInitialized) VerifyNotExpired();
			var json = new JsonObject
			           	{
			           		{"attachments", _attachments.HasValue ? _attachments.Value : JsonValue.Null},
			           		{"checkItems", _checkItems.HasValue ? _checkItems.Value : JsonValue.Null},
			           		{"checkItemsChecked", _checkItemsChecked.HasValue ? _checkItemsChecked.Value : JsonValue.Null},
			           		{"comments", _comments.HasValue ? _comments.Value : JsonValue.Null},
			           		{"due", _dueDate.HasValue ? _dueDate.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			           		{"fogBugz", _fogBugz},
			           		{"desc", _hasDescription.HasValue ? _hasDescription.Value : JsonValue.Null},
			           		{"subscribed", _isSubscribed.HasValue ? _isSubscribed.Value : JsonValue.Null},
			           		{"viewingMemberVoted", _viewingMemberVoted.HasValue ? _viewingMemberVoted.Value : JsonValue.Null},
			           		{"votes", _votes.HasValue ? _votes.Value : JsonValue.Null},
			           	};
			return json;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var badges = entity as Badges;
			if (badges == null) return;
			_attachments = badges._attachments;
			_checkItems = badges._checkItems;
			_checkItemsChecked = badges._checkItemsChecked;
			_comments = badges._comments;
			_dueDate = badges._dueDate;
			_fogBugz = badges._fogBugz;
			_hasDescription = badges._hasDescription;
			_isSubscribed = badges._isSubscribed;
			_viewingMemberVoted = badges._viewingMemberVoted;
			_votes = badges._votes;
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Get(Svc.RequestProvider.Create<Badges>(new[] { Owner, this }));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce() {}
	}
}