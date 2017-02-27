using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Comment object.
	/// </summary>
	[Obsolete("This JSON type is no longer needed.  It will be deleted with the next major version.")]
	public interface IJsonComment : IJsonAction
	{
		/// <summary>
		/// Gets or sets the text content of the comment.
		/// </summary>
		[JsonDeserialize]
		string Text { get; set; }
	}
}