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
 
	File Name:		MemberSearchParameter.cs
	Namespace:		Manatee.Trello.Internal.SearchSearching
	Class Name:		MemberSearchParameter
	Purpose:		Searches for items mentioning or attached to a specified member.

***************************************************************************************/

namespace Manatee.Trello.Internal.Searching
{
	internal class MemberSearchParameter : ISearchParameter
	{
		public string Query { get; }

		public MemberSearchParameter(Member member)
		{
			Query = member is Me ? "@me" : member.Mention;
		}
	}
}