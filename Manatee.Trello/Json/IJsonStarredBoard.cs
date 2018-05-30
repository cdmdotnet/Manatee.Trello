using Manatee.Trello.Internal;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the StarredBoard object.
	/// </summary>
	public interface IJsonStarredBoard : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the board.
		/// </summary>
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		IJsonPosition Pos { get; set; }
	}
}