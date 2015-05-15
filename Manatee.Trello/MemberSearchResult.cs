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
 
	File Name:		MemberSearchResult.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberSearchResult
	Purpose:		Represents a result from a member search.

***************************************************************************************/

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a result from a member search.
	/// </summary>
	public class MemberSearchResult
	{
		/// <summary>
		/// Gets the returned member.
		/// </summary>
		public Member Member { get; private set; }
		/// <summary>
		/// Gets a value indicating the similarity of the member to the search query.
		/// </summary>
		public int? Similarity { get; private set; }

		internal MemberSearchResult(Member member, int? similarity)
		{
			Member = member;
			Similarity = similarity;
		}
	}
}
