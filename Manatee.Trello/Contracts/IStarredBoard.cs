using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a member's board star.
	/// </summary>
	public interface IStarredBoard : ICacheable, IRefreshable
	{
		/// <summary>
		/// Gets the board that is starred.
		/// </summary>
		IBoard Board { get; }
		/// <summary>
		/// Gets or sets the position in the member's starred boards list.
		/// </summary>
		Position Position { get; set; }

		/// <summary>
		/// Raised when data on the star is updated.
		/// </summary>
		event Action<IStarredBoard, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the star.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the star from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));
	}
}