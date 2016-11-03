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
		[JsonDeserialize]
		IJsonBadges Badges { get; set; }
		/// <summary>
		/// Gets or sets whether a card has been archived.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Closed { get; set; }
		/// <summary>
		/// Gets or sets the date of last activity for a card.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateLastActivity { get; set; }
		/// <summary>
		/// Gets or sets the card's description.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Desc { get; set; }
		/// <summary>
		/// Gets or sets the card's due date.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		DateTime? Due { get; set; }
		/// <summary>
		/// Gets or sets the ID of the board which contains the card.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the ID of the list which contains the card.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonList List { get; set; }
		/// <summary>
		/// Gets or sets the card's short ID.
		/// </summary>
		[JsonDeserialize]
		int? IdShort { get; set; }
		/// <summary>
		/// Gets or sets the ID of the attachment cover image.
		/// </summary>
		[JsonDeserialize]
		string IdAttachmentCover { get; set; }
		/// <summary>
		/// Gets or sets the labels assigned to this card.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		List<IJsonLabel> Labels { get; set; }
		/// <summary>
		/// Gets or sets whether the cover attachment was manually selected
		/// </summary>
		[JsonDeserialize]
		bool? ManualCoverAttachment { get; set; }
		/// <summary>
		/// Gets or sets the card's name
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the card's position.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonPosition Pos { get; set; }
		/// <summary>
		/// Gets or sets the URL for this card.
		/// </summary>
		[JsonDeserialize]
		string Url { get; set; }
		/// <summary>
		/// Gets or sets the short URL for this card.
		/// </summary>
		[JsonDeserialize]
		string ShortUrl { get; set; }
		/// <summary>
		/// Gets or sets whether the current member is subscribed to this card.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Subscribed { get; set; }
		/// <summary>
		/// Gets or sets a card to be used as a template during creation.
		/// </summary>
		[JsonSerialize]
		IJsonCard CardSource { get; set; }
		/// <summary>
		/// Gets or sets a URL to be imported during creation.
		/// </summary>
		[JsonSerialize]
		object UrlSource { get; set; }
		/// <summary>
		/// Gets or sets whether the due date should be serialized, even if it is null.
		/// </summary>
		bool ForceDueDate { get; set; }
	}
}
