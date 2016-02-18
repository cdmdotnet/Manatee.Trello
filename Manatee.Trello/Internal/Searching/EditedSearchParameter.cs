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
 
	File Name:		EditedSearchParameter.cs
	Namespace:		Manatee.Trello.Internal.Searching
	Class Name:		EditedSearchParameter
	Purpose:		

***************************************************************************************/

using System.Globalization;

namespace Manatee.Trello.Internal.Searching
{
	internal class EditedSearchParameter : ISearchParameter
	{
		public static readonly EditedSearchParameter Day = new EditedSearchParameter("day");
		public static readonly EditedSearchParameter Week = new EditedSearchParameter("week");
		public static readonly EditedSearchParameter Month = new EditedSearchParameter("month");

		public string Query { get; }

		public EditedSearchParameter(int days)
		{
			Query = "edited:" + days.ToString(CultureInfo.InvariantCulture);
		}
		private EditedSearchParameter(string named)
		{
			Query = "edited:" + named;
		}
	}
}