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
 
	File Name:		SearchExtensions.cs
	Namespace:		Manatee.Trello
	Class Name:		SearchExtensions
	Purpose:		Provides extension methods which allow fluent creation of search
					parameters.

***************************************************************************************/

using Manatee.Trello.Internal.Searching;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides extension methods which allow fluent creation of search parameters.
	/// </summary>
	public static class SearchExtensions
	{
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a text search parameter.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndText(this SearchFor search, string text)
		{
			return new SearchFor(search, new TextSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a text search parameter specific to card names.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndTextInName(this SearchFor search, string text)
		{
			return new SearchFor(search, new TextInCardNameSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a text search parameter specific to card descriptions.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndTextInDescription(this SearchFor search, string text)
		{
			return new SearchFor(search, new TextInCardDescriptionSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a text search parameter specific to card comments.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndTextInComments(this SearchFor search, string text)
		{
			return new SearchFor(search, new TextInCardCommentSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a text search parameter specific to check lists.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndTextInCheckLists(this SearchFor search, string text)
		{
			return new SearchFor(search, new TextInCardCheckListSearchParameter(text));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a member search parameter.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="member">The member to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndMember(this SearchFor search, Member member)
		{
			return new SearchFor(search, new MemberSearchParameter(member));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a label search parameter.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="label">The label to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndLabel(this SearchFor search, Label label)
		{
			return new SearchFor(search, new LabelSearchParameter(label));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a label color search parameter.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="labelColor">The label color to search for.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndLabel(this SearchFor search, LabelColor labelColor)
		{
			return new SearchFor(search, new LabelSearchParameter(labelColor));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only archived items.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndIsArchived(this SearchFor search)
		{
			return new SearchFor(search, IsSearchParameter.Archived);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only unarchived items.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndIsOpen(this SearchFor search)
		{
			return new SearchFor(search, IsSearchParameter.Open);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only starred items.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndIsStarred(this SearchFor search)
		{
			return new SearchFor(search, IsSearchParameter.Starred);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the next 24 hours.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndDueWithinDay(this SearchFor search)
		{
			return new SearchFor(search, DueSearchParameter.Day);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the next week.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndDueWithinWeek(this SearchFor search)
		{
			return new SearchFor(search, DueSearchParameter.Week);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the next month.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndDueWithinMonth(this SearchFor search)
		{
			return new SearchFor(search, DueSearchParameter.Month);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the next <paramref name="days"/> hours.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndDueWithinDays(this SearchFor search, int days)
		{
			return new SearchFor(search, new DueSearchParameter(days));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items which are overdue.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndOverdue(this SearchFor search)
		{
			return new SearchFor(search, DueSearchParameter.Overdue);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndCreatedWithinDay(this SearchFor search)
		{
			return new SearchFor(search, CreatedSearchParameter.Day);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndCreatedWithinWeek(this SearchFor search)
		{
			return new SearchFor(search, CreatedSearchParameter.Week);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndCreatedWithinMonth(this SearchFor search)
		{
			return new SearchFor(search, CreatedSearchParameter.Month);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndCreatedWithinDays(this SearchFor search, int days)
		{
			return new SearchFor(search, new CreatedSearchParameter(days));
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndEditedWithinDay(this SearchFor search)
		{
			return new SearchFor(search, EditedSearchParameter.Day);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndEditedWithinWeek(this SearchFor search)
		{
			return new SearchFor(search, EditedSearchParameter.Week);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndEditedWithinMonth(this SearchFor search)
		{
			return new SearchFor(search, EditedSearchParameter.Month);
		}
		/// <summary>
		/// Creates a new <see cref="SearchFor"/> appending a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="search">The current search</param>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="SearchFor"/> parameter list.</returns>
		public static SearchFor AndEditedWithinDays(this SearchFor search, int days)
		{
			return new SearchFor(search, new EditedSearchParameter(days));
		}
	}
}