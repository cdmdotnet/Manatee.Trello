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
 
	File Name:		DueSearchParameter.cs
	Namespace:		Manatee.Trello.Internal.Searching
	Class Name:		DueSearchParameter
	Purpose:		

***************************************************************************************/

using System.Globalization;

namespace Manatee.Trello.Internal.Searching
{
	internal class DueSearchParameter : ISearchParameter
	{
		public static readonly DueSearchParameter Day = new DueSearchParameter("day");
		public static readonly DueSearchParameter Week = new DueSearchParameter("week");
		public static readonly DueSearchParameter Month = new DueSearchParameter("month");
		public static readonly DueSearchParameter Overdue = new DueSearchParameter("overdue");

		public string Query { get; }

		public DueSearchParameter(int days)
		{
			Query = "due:" + days.ToString(CultureInfo.InvariantCulture);
		}
		private DueSearchParameter(string named)
		{
			Query = "due:" + named;
		}
	}
}