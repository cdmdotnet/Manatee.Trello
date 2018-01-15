using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the model types to which a user token may grant access.
	/// </summary>
	public enum TokenModelType
	{
		/// <summary>
		/// Assigned when the model type is not recognized.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates the model is one or more Members.
		/// </summary>
		[Display(Description="Member")]
		Member,
		/// <summary>
		/// Indicates the model is one or more Boards.
		/// </summary>
		[Display(Description="Board")]
		Board,
		/// <summary>
		/// Indicates the model is one or more Organizations.
		/// </summary>
		[Display(Description="Organization")]
		Organization
	}
}