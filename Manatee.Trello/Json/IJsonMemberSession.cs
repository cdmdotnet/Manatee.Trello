using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the MemberSession object.
	/// </summary>
	public interface IJsonMemberSession
	{
		// TODO: Implement member sessions.
		/// <summary>
		/// Gets or sets whether this session is active.
		/// </summary>
		[JsonDeserialize]
		bool? IsCurrent { get; set; }
		/// <summary>
		/// Gets or sets whether this session has been accessed recently.
		/// </summary>
		[JsonDeserialize]
		bool? IsRecent { get; set; }
		/// <summary>
		/// Gets or sets the ID for this session.
		/// </summary>
		[JsonDeserialize]
		string Id { get; set; }
		/// <summary>
		/// Gets or sets the date this session was created.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateCreated { get; set; }
		/// <summary>
		/// Gets or sets the date this session expires.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateExpires { get; set; }
		/// <summary>
		/// Gets or sets the date this session was last used.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateLastUsed { get; set; }
		/// <summary>
		/// Gets or sets the IP address associated with this session.
		/// </summary>
		[JsonDeserialize]
		string IpAddress { get; set; }
		/// <summary>
		/// Gets or sets the type of session.
		/// </summary>
		[JsonDeserialize]
		string Type { get; set; }
		/// <summary>
		/// Gets or sets the user agent associated with this session.
		/// </summary>
		[JsonDeserialize]
		string UserAgent { get; set; }
	}
}