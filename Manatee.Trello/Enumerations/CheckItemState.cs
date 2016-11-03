using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates known values for an item in a checklist.
	/// </summary>
	public enum CheckItemState
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that the checlist item is not checked.
		/// </summary>
		[Description("incomplete")]
		Incomplete,
		/// <summary>
		/// Indicates that the checlist item is checked.
		/// </summary>
		[Description("complete")]
		Complete
	}
}