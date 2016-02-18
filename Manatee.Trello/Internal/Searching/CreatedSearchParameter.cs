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
 
	File Name:		CreatedSearchParameter.cs
	Namespace:		Manatee.Trello.Internal.Searching
	Class Name:		CreatedSearchParameter
	Purpose:		

***************************************************************************************/

using System.Globalization;

namespace Manatee.Trello.Internal.Searching
{
	internal class CreatedSearchParameter : ISearchParameter
	{
		public static readonly CreatedSearchParameter Day = new CreatedSearchParameter("day");
		public static readonly CreatedSearchParameter Week = new CreatedSearchParameter("week");
		public static readonly CreatedSearchParameter Month = new CreatedSearchParameter("month");

		public string Query { get; }

		public CreatedSearchParameter(int days)
		{
			Query = "created:" + days.ToString(CultureInfo.InvariantCulture);
		}
		private CreatedSearchParameter(string named)
		{
			Query = "created:" + named;
		}
	}
}