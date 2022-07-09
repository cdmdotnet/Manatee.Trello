using System;
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the collaborator object.
	/// </summary>
	public interface IJsonCollaborator : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets a unique identifier (not necessarily a GUID).
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize(IsRequired = true)]
		new string Id { get; set; }

		/// <summary>
		/// Gets or sets the member's avatar hash.
		/// </summary>
		[JsonDeserialize]
		[Obsolete("Trello has depricated this property.")]
		string AvatarHash { get; set; }
		/// <summary>
		/// Gets or sets the member's avatar hash.
		/// </summary>
		[JsonDeserialize]
		string AvatarUrl { get; set; }
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
		/// Gets or sets a collection of organizations.
		/// </summary>
		[JsonDeserialize]
		List<IJsonOrganization> Organizations { get; set; }
	}
}