using System;
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Attachment object.
	/// </summary>
	public interface IJsonAttachment : IJsonCacheable
	{
		///<summary>
		/// Gets or sets the size of the attachment.
		///</summary>
		[JsonDeserialize]
		int? Bytes { get; set; }
		/// <summary>
		/// Gets or sets the date on which the attachment was created.
		/// </summary>
		[JsonDeserialize]
		DateTime? Date { get; set; }
		/// <summary>
		/// Gets or sets the border color of the attachment preview on the card.
		/// </summary>
		[JsonSerialize]
		[JsonDeserialize]
		string EdgeColor { get; set; }
		///<summary>
		/// Gets or sets the ID of the member who created the attachment.
		///</summary>
		[JsonDeserialize]
		IJsonMember Member { get; set; }
		///<summary>
		/// ?
		///</summary>
		[JsonDeserialize]
		bool? IsUpload { get; set; }
		///<summary>
		/// Gets or sets the type of attachment.
		///</summary>
		[JsonDeserialize]
		string MimeType { get; set; }
		///<summary>
		/// Gets or sets the name of the attachment.
		///</summary>
		[JsonDeserialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the attachment's position.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonPosition Pos { get; set; }
		///<summary>
		/// Gets or sets a collection of previews for the attachment.
		///</summary>
		[JsonDeserialize]
		List<IJsonImagePreview> Previews { get; set; }
		///<summary>
		/// Gets or sets the attachment storage location.
		///</summary>
		[JsonDeserialize]
		string Url { get; set; }
	}
}