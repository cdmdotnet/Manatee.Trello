using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Member object.
	/// </summary>
	public interface IJsonMember : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the member's avatar hash.
		/// </summary>
		[JsonDeserialize]
		string AvatarHash { get; set; }
		/// <summary>
		/// Gets or sets the bio of the member.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Bio { get; set; }
		/// <summary>
		/// Gets the member's full name.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string FullName { get; set; }
		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Initials { get; set; }
		/// <summary>
		/// Gets or sets the type of member.
		/// </summary>
		// TODO: Implement Member.MemberType.
		[JsonDeserialize]
		string MemberType { get; set; }
		/// <summary>
		/// Gets or sets the member's activity status.
		/// </summary>
		[JsonDeserialize]
		MemberStatus? Status { get; set; }
		/// <summary>
		/// Gets or sets the URL to the member's profile.
		/// </summary>
		[JsonDeserialize]
		string Url { get; set; }
		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Username { get; set; }
		/// <summary>
		/// Gets or sets the source URL for the member's avatar.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		AvatarSource? AvatarSource { get; set; }
		/// <summary>
		/// Gets or sets whether the member is confirmed.
		/// </summary>
		[JsonDeserialize]
		bool? Confirmed { get; set; }
		/// <summary>
		/// Gets or sets the member's registered email address.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Email { get; set; }
		/// <summary>
		/// Gets or sets the member's Gravatar hash.
		/// </summary>
		// TODO: Implement Member.GravatarHash.
		[JsonDeserialize]
		string GravatarHash { get; set; }
		/// <summary>
		/// Gets or sets the login types for the member.
		/// </summary>
		// TODO: Implement Member.LoginTypes.
		[JsonDeserialize]
		List<string> LoginTypes { get; set; }
		/// <summary>
		/// Gets or sets the trophies obtained by the member.
		/// </summary>
		[JsonDeserialize]
		List<string> Trophies { get; set; }
		/// <summary>
		/// Gets or sets the user's uploaded avatar hash.
		/// </summary>
		// TODO: Implement Member.UploadedAvatarHash.
		[JsonDeserialize]
		string UploadedAvatarHash { get; set; }
		/// <summary>
		/// Gets or sets the types of message which are dismissed for the member.
		/// </summary>
		// TODO: Implement Member.OneTimeMessagesDismissed.
		[JsonDeserialize]
		List<string> OneTimeMessagesDismissed { get; set; }
		/// <summary>
		/// Gets or sets the similarity of the member to a search query.
		/// </summary>
		[JsonDeserialize]
		int? Similarity { get; set; }
		/// <summary>
		/// Gets or sets a set of preferences for the member.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		IJsonMemberPreferences Prefs { get; set; }
		/// <summary>
		/// Gets or sets a collection of actions.
		/// </summary>
		[JsonDeserialize]
		List<IJsonAction> Actions { get; set; }
		/// <summary>
		/// Gets or sets a collection of boards.
		/// </summary>
		[JsonDeserialize]
		List<IJsonBoard> Boards { get; set; }
		/// <summary>
		/// Gets or sets a collection of cards.
		/// </summary>
		[JsonDeserialize]
		List<IJsonCard> Cards { get; set; }
		/// <summary>
		/// Gets or sets a collection of notifications.
		/// </summary>
		[JsonDeserialize]
		List<IJsonNotification> Notifications { get; set; }
		/// <summary>
		/// Gets or sets a collection of organizations.
		/// </summary>
		[JsonDeserialize]
		List<IJsonOrganization> Organizations { get; set; }
	}
}