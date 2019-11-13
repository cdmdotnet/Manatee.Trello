using System.ComponentModel.DataAnnotations;

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
		/// Indicates that the checklist item is not checked.
		/// </summary>
		[Display(Description="incomplete")]
		Incomplete,
		/// <summary>
		/// Indicates that the checklist item is checked.
		/// </summary>
		[Display(Description="complete")]
		Complete
	}
}