namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the CommentReaction object.
	/// </summary>
	public interface IJsonCommentReaction : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the member that posted the reaction.
		/// </summary>
		IJsonMember Member { get; set; }
		/// <summary>
		/// Gets or sets the comment (action).
		/// </summary>
		IJsonAction Comment { get; set; }
		/// <summary>
		/// Gets or sets the emoji.
		/// </summary>
		Emoji Emoji { get; set; }
	}
}