/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		BoardExtensions.cs
	Namespace:		Manatee.Trello.Extensions
	Class Name:		BoardExtensions
	Purpose:		Extension methods for members.

***************************************************************************************/

using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello
{
	/// <summary>
	/// Extension methods for members.
	/// </summary>
	public static class MemberExtensions
	{
		/// <summary>
		/// Gets all open cards for a member.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <returns>A <see cref="ReadOnlyCardCollection"/> containing the member's cards.</returns>
		public static ReadOnlyCardCollection Cards(this Member member)
		{
			return new ReadOnlyCardCollection(EntityRequestType.Member_Read_Cards, () => member.Id, member.Auth);
		}
	}
}
