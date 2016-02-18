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
 
	File Name:		IsOpenSearchParameter.cs
	Namespace:		Manatee.Trello.Internal.Searching
	Class Name:		IsOpenSearchParameter
	Purpose:		Searches for items which have not been archived.

***************************************************************************************/

namespace Manatee.Trello.Internal.Searching
{
	internal class IsSearchParameter : ISearchParameter
	{
		public static readonly IsSearchParameter Open = new IsSearchParameter("open");
		public static readonly IsSearchParameter Archived = new IsSearchParameter("archived");
		public static readonly IsSearchParameter Starred = new IsSearchParameter("starred");

		public string Query { get; }

		private IsSearchParameter(string p)
		{
			Query = "is:" + p;
		}
	}
}