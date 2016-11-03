using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the ActionData object.
	/// </summary>
	public interface IJsonActionData
	{
		/// <summary>
		/// Gets or sets an attachment associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonAttachment Attachment { get; set; }
		/// <summary>
		/// Gets or sets a board associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets a board associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard BoardSource { get; set; }
		/// <summary>
		/// Gets or sets a board associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard BoardTarget { get; set; }
		/// <summary>
		/// Gets or sets a card associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonCard Card { get; set; }
		/// <summary>
		/// Gets or sets a card associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonCard CardSource { get; set; }
		/// <summary>
		/// Gets or sets a check item associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonCheckItem CheckItem { get; set; }
		/// <summary>
		/// Gets or sets a check list associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonCheckList CheckList { get; set; }
		/// <summary>
		/// Gets or sets the last date/time that a comment was edited.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateLastEdited { get; set; }
		/// <summary>
		/// Gets or sets a list associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonList List { get; set; }
		/// <summary>
		/// Gets or sets a destination list associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonList ListAfter { get; set; }
		/// <summary>
		/// Gets or sets a source list associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonList ListBefore { get; set; }
		/// <summary>
		/// Gets or sets a member associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonMember Member { get; set; }
		/// <summary>
		/// Gets or sets an organization associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		IJsonOrganization Org { get; set; }
		/// <summary>
		/// Gets or sets any previous data associated with the action.
		/// </summary>
		[JsonDeserialize]
		IJsonActionOldData Old { get; set; }
		/// <summary>
		/// Gets or sets text associated with the action if any.
		/// </summary>
		[JsonDeserialize]
		string Text { get; set; }
		/// <summary>
		/// Gets or sets a custom value associate with the action if any.
		/// </summary>
		[JsonDeserialize]
		string Value { get; set; }
	}
}