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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

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
	public class Badges : ExpiringObject
	{
		private IJsonBadges _jsonBadges;

		///<summary>
		/// Indicates the number of attachments.
		///</summary>
		public int? Attachments
		{
			get
			{
				VerifyNotExpired();
				return _jsonBadges.Attachments;
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
				return _jsonBadges.CheckItems;
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
				return _jsonBadges.CheckItemsChecked;
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
				return _jsonBadges.Comments;
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
				return _jsonBadges.Due;
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
				return _jsonBadges.Fogbugz;
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
				return _jsonBadges.Description;
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
				return _jsonBadges.Subscribed;
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
				return _jsonBadges.ViewingMemberVoted;
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
				return _jsonBadges.Votes;
			}
		}

		internal override string Key { get { return "badges"; } }

		/// <summary>
		/// Creates a new instance of the Badges class.
		/// </summary>
		public Badges() {}
		internal Badges(Card owner)
		{
			Owner = owner;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			var request = Api.RequestProvider.Create<IJsonBadges>(endpoint.ToString());
			ApplyJson(Api.Get(request));
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService() {}

		internal override void ApplyJson(object obj)
		{
			_jsonBadges = (IJsonBadges) obj;
		}
	}
}