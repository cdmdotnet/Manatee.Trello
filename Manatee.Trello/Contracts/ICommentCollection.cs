namespace Manatee.Trello
{
	/// <summary>
	/// A collection of comment actions.
	/// </summary>
	public interface ICommentCollection : IReadOnlyActionCollection
	{
		/// <summary>
		/// Posts a new comment to a card.
		/// </summary>
		/// <param name="text">The content of the comment.</param>
		/// <returns>The <see cref="Action"/> associated with the comment.</returns>
		IAction Add(string text);
	}
}