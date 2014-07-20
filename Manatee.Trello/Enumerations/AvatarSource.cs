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
 
	File Name:		AvatarSource.cs
	Namespace:		Manatee.Trello
	Class Name:		AvatarSource
	Purpose:		Enumerates the avatar sources used by Trello.

***************************************************************************************/

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