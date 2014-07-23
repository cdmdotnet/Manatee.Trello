/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		IJsonMember.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonMember
	Purpose:		Defines the JSON structure for the Member object.

***************************************************************************************/
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Member object.
	/// </summary>
	public interface IJsonMember : IJsonCacheable
	{
		// TODO: Implement missing member properties.
		/// <summary>
		/// Gets or sets the member's avatar hash.
		/// </summary>
		string AvatarHash { get; set; }
		/// <summary>
		/// Gets or sets the bio of the member.
		/// </summary>
		string Bio { get; set; }
		/// <summary>
		/// Gets the member's full name.
		/// </summary>
		string FullName { get; set; }
		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		string Initials { get; set; }
		/// <summary>
		/// Gets or sets the type of member.
		/// </summary>
		string MemberType { get; set; }
		/// <summary>
		/// Gets or sets the member's activity status.
		/// </summary>
		MemberStatus Status { get; set; }
		/// <summary>
		/// Gets or sets the URL to the member's profile.
		/// </summary>
		string Url { get; set; }
		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		string Username { get; set; }
		/// <summary>
		/// Gets or sets the source URL for the member's avatar.
		/// </summary>
		AvatarSource AvatarSource { get; set; }
		/// <summary>
		/// Gets or sets whether the member is confirmed.
		/// </summary>
		bool? Confirmed { get; set; }
		/// <summary>
		/// Gets or sets the member's registered email address.
		/// </summary>
		string Email { get; set; }
		/// <summary>
		/// Gets or sets the member's Gravatar hash.
		/// </summary>
		string GravatarHash { get; set; }
		/// <summary>
		/// Gets or sets the login types for the member.
		/// </summary>
		List<string> LoginTypes { get; set; }
		/// <summary>
		/// Gets or sets the trophies obtained by the member.
		/// </summary>
		List<string> Trophies { get; set; }
		/// <summary>
		/// Gets or sets the user's uploaded avatar hash.
		/// </summary>
		string UploadedAvatarHash { get; set; }
		/// <summary>
		/// Gets or sets the types of message which are dismissed for the member.
		/// </summary>
		List<string> OneTimeMessagesDismissed { get; set; }
		/// <summary>
		/// Gets or sets the similarity of the member to a search query.
		/// </summary>
		int? Similarity { get; set; }
	}
}