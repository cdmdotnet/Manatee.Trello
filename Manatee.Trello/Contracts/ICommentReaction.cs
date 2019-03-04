using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a reaction to a card comment.
	/// </summary>
	public interface ICommentReaction : ICacheable
	{
		/// <summary>
		/// Gets the comment (<see cref="Action"/>) reacted to.
		/// </summary>
		Action Comment { get; }
		/// <summary>
		/// Gets the emoji used for the reaction.
		/// </summary>
		Emoji Emoji { get; }
		/// <summary>
		/// Gets the member who posted the reaction.
		/// </summary>
		Member Member { get; }

		/// <summary>
		/// Deletes the reaction.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the reaction from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));
	}
}