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
 
	File Name:		IJsonBadges.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonBadges
	Purpose:		Defines the JSON structure for the Badges object.

***************************************************************************************/
using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Badges object.
	/// </summary>
	public interface IJsonBadges
	{
		/// <summary>
		/// Gets or sets the number of votes.
		/// </summary>
		int? Votes { get; set; }
		/// <summary>
		/// Gets or sets whether the member has voted for this card.
		/// </summary>
		bool? ViewingMemberVoted { get; set; }
		/// <summary>
		/// Gets or sets whether the member is subscribed to the card.
		/// </summary>
		bool? Subscribed { get; set; }
		/// <summary>
		/// Gets or sets the FogBugz ID.
		/// </summary>
		string Fogbugz { get; set; }
		/// <summary>
		/// Gets or sets the due date, if one exists.
		/// </summary>
		DateTime? Due { get; set; }
		/// <summary>
		/// Gets or sets whether the card has a description.
		/// </summary>
		bool? Description { get; set; }
		/// <summary>
		/// Gets or sets the number of comments.
		/// </summary>
		int? Comments { get; set; }
		/// <summary>
		/// Gets or sets the number of check items which have been checked.
		/// </summary>
		int? CheckItemsChecked { get; set; }
		/// <summary>
		/// Gets or sets the number of check items.
		/// </summary>
		int? CheckItems { get; set; }
		///<summary>
		/// Gets or sets the number of attachments.
		///</summary>
		int? Attachments { get; set; }
	}
}