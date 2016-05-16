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
 
	File Name:		SearchFor.cs
	Namespace:		Manatee.Trello
	Class Name:		SearchFor
	Purpose:		Provides an easy mechanism to build search queries.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Searching;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an easy mechanism to build search queries.
	/// </summary>
	public class SearchFor
	{
		private readonly List<ISearchParameter> _parameters;

		private SearchFor(ISearchParameter parameter)
		{
			_parameters = new List<ISearchParameter>{parameter};
		}
		internal SearchFor(SearchFor currentSearch, ISearchParameter newParameter)
		{
			_parameters = new List<ISearchParameter>(currentSearch._parameters) {newParameter};
		}

		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a text search parameter.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns></returns>
		public static SearchFor Text(string text)
		{
			return new SearchFor(new TextSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a text search parameter specific to card names.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor TextInName(string text)
		{
			return new SearchFor(new TextInCardNameSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a text search parameter specific to card descriptions.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor TextInDescription(string text)
		{
			return new SearchFor(new TextInCardDescriptionSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a text search parameter specific to card comments.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor TextInComments(string text)
		{
			return new SearchFor(new TextInCardCommentSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a text search parameter specific to check lists.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor TextInCheckLists(string text)
		{
			return new SearchFor(new TextInCardCheckListSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a member search parameter.
		/// </summary>
		/// <param name="member">The member to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor Member(Member member)
		{
			return new SearchFor(new MemberSearchParameter(member));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a label search parameter.
		/// </summary>
		/// <param name="label">The label to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor Label(Label label)
		{
			return new SearchFor(new LabelSearchParameter(label));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a label color search parameter.
		/// </summary>
		/// <param name="labelColor">The label color to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor Label(LabelColor labelColor)
		{
			return new SearchFor(new LabelSearchParameter(labelColor));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only archived items.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor IsArchived()
		{
			return new SearchFor(IsSearchParameter.Archived);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only unarchived items.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor IsOpen()
		{
			return new SearchFor(IsSearchParameter.Open);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only starred items.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor IsStarred()
		{
			return new SearchFor(IsSearchParameter.Starred);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the next 24 hours.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor DueWithinDay()
		{
			return new SearchFor(DueSearchParameter.Day);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the next week.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor DueWithinWeek()
		{
			return new SearchFor(DueSearchParameter.Week);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the next month.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor DueWithinMonth()
		{
			return new SearchFor(DueSearchParameter.Month);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the next <paramref name="days"/> hours.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor DueWithinDays(int days)
		{
			return new SearchFor(new DueSearchParameter(days));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items which are overdue.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor Overdue()
		{
			return new SearchFor(DueSearchParameter.Overdue);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor CreatedWithinDay()
		{
			return new SearchFor(CreatedSearchParameter.Day);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor CreatedWithinWeek()
		{
			return new SearchFor(CreatedSearchParameter.Week);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor CreatedWithinMonth()
		{
			return new SearchFor(CreatedSearchParameter.Month);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor CreatedWithinDays(int days)
		{
			return new SearchFor(new CreatedSearchParameter(days));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor EditedWithinDay()
		{
			return new SearchFor(EditedSearchParameter.Day);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor EditedWithinWeek()
		{
			return new SearchFor(EditedSearchParameter.Week);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor EditedWithinMonth()
		{
			return new SearchFor(EditedSearchParameter.Month);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> specifying a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor EditedWithinDays(int days)
		{
			return new SearchFor(new EditedSearchParameter(days));
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return _parameters.Select(p => p.Query).Join(" ");
		}
	}
}