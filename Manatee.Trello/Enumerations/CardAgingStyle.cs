using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the various styles of aging for the Card Aging power up.
	/// </summary>
	public enum CardAgingStyle
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that cards will age by fading.
		/// </summary>
		[Description("regular")]
		Regular,
		/// <summary>
		/// Indicates that cards will age using a treasure map effect.
		/// </summary>
		[Description("pirate")]
		Pirate
	}
}