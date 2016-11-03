namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Comment object.
	/// </summary>
	public interface IJsonComment : IJsonAction
	{
		/// <summary>
		/// Gets or sets the text content of the comment.
		/// </summary>
		[JsonDeserialize]
		string Text { get; set; }
	}
}