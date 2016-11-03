using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the avatar sources used by Trello.
	/// </summary>
	public enum AvatarSource
	{
		/// <summary>
		/// Indicates the avatar source is not recognized.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates there is no avatar.
		/// </summary>
		[Description("none")]
		None,
		/// <summary>
		/// Indicates the avatar has been uploaded by the user.
		/// </summary>
		[Description("upload")]
		Upload,
		/// <summary>
		/// Indicates the avatar is supplied by Gravatar.
		/// </summary>
		[Description("gravatar")]
		Gravatar
	}
}