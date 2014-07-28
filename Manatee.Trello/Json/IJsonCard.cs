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
 
	File Name:		IJsonCard.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonCard
	Purpose:		Defines the JSON structure for the Card object.

***************************************************************************************/
using System;
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Card object.
	/// </summary>
	public interface IJsonCard : IJsonCacheable
	{
		/// <summary>
		/// Gets or set the badges displayed on the card cover.
		/// </summary>
		IJsonBadges Badges { get; set; }
		/// <summary>
		/// Gets or sets whether a card has been archived.
		/// </summary>
		bool? Closed { get; set; }
		/// <summary>
		/// Gets or sets the date of last activity for a card.
		/// </summary>
		DateTime? DateLastActivity { get; set; }
		/// <summary>
		/// Gets or sets the card's description.
		/// </summary>
		string Desc { get; set; }
		/// <summary>
		/// Gets or sets the card's due date.
		/// </summary>
		DateTime? Due { get; set; }
		/// <summary>
		/// Gets or sets the ID of the board which contains the card.
		/// </summary>
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the ID of the list which contains the card.
		/// </summary>
		IJsonList List { get; set; }
		/// <summary>
		/// Gets or sets the card's short ID.
		/// </summary>
		int? IdShort { get; set; }
		/// <summary>
		/// Gets or sets the ID of the attachment cover image.
		/// </summary>
		string IdAttachmentCover { get; set; }
		/// <summary>
		/// Gets or sets the labels assigned to this card.
		/// </summary>
		List<IJsonLabel> Labels { get; set; }
		/// <summary>
		/// Gets or sets whether the cover attachment was manually selected
		/// </summary>
		bool? ManualCoverAttachment { get; set; }
		/// <summary>
		/// Gets or sets the card's name
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the card's position.
		/// </summary>
		IJsonPosition Pos { get; set; }
		/// <summary>
		/// Gets or sets the URL for this card.
		/// </summary>
		string Url { get; set; }
		/// <summary>
		/// Gets or sets the short URL for this card.
		/// </summary>
		string ShortUrl { get; set; }
		/// <summary>
		/// Gets or sets whether the current member is subscribed to this card.
		/// </summary>
		bool? Subscribed { get; set; }
	}
}
