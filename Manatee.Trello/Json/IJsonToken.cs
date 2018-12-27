using System;
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Token object.
	/// </summary>
	public interface IJsonToken : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the token itself.
		/// </summary>
		[JsonSpecialSerialization]
		string TokenValue { get; set; }
		/// <summary>
		/// Gets or sets the identifier of the application which requested the token.
		/// </summary>
		[JsonDeserialize]
		string Identifier { get; set; }
		/// <summary>
		/// Gets or sets the ID of the member who issued the token.
		/// </summary>
		[JsonDeserialize]
		IJsonMember Member { get; set; }
		/// <summary>
		/// Gets or sets the date the token was created.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateCreated { get; set; }
		/// <summary>
		/// Gets or sets the date the token will expire, if any.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateExpires { get; set; }
		/// <summary>
		/// Gets or sets the collection of permissions granted by the token.
		/// </summary>
		[JsonDeserialize]
		List<IJsonTokenPermission> Permissions { get; set; }
	}
}