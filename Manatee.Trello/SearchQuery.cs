using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Searching;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an easy mechanism to build search queries.
	/// </summary>
	public class SearchQuery : ISearchQuery
	{
		private readonly List<ISearchParameter> _parameters = new List<ISearchParameter>();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns></returns>
		public ISearchQuery Text(string text)
		{
			_parameters.Add(new TextSearchParameter(text));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card names.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery TextInName(string text)
		{
			_parameters.Add(new TextInCardNameSearchParameter(text));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card descriptions.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery TextInDescription(string text)
		{
			_parameters.Add(new TextInCardDescriptionSearchParameter(text));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card comments.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery TextInComments(string text)
		{
			_parameters.Add(new TextInCardCommentSearchParameter(text));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to check lists.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery TextInCheckLists(string text)
		{
			_parameters.Add(new TextInCardCheckListSearchParameter(text));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a member search parameter.
		/// </summary>
		/// <param name="member">The member to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery Member(IMember member)
		{
			_parameters.Add(new MemberSearchParameter((Member) member));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a label search parameter.
		/// </summary>
		/// <param name="label">The label to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery Label(ILabel label)
		{
			_parameters.Add(new LabelSearchParameter((Label) label));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a label color search parameter.
		/// </summary>
		/// <param name="labelColor">The label color to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery Label(LabelColor labelColor)
		{
			_parameters.Add(new LabelSearchParameter(labelColor));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only archived items.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery IsArchived()
		{
			_parameters.Add(IsSearchParameter.Archived);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only unarchived items.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery IsOpen()
		{
			_parameters.Add(IsSearchParameter.Open);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only starred items.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery IsStarred()
		{
			_parameters.Add(IsSearchParameter.Starred);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next 24 hours.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery DueWithinDay()
		{
			_parameters.Add(DueSearchParameter.Day);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next week.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery DueWithinWeek()
		{
			_parameters.Add(DueSearchParameter.Week);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next month.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery DueWithinMonth()
		{
			_parameters.Add(DueSearchParameter.Month);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next <paramref name="days"/> hours.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery DueWithinDays(int days)
		{
			_parameters.Add(new DueSearchParameter(days));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items which are overdue.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery Overdue()
		{
			_parameters.Add(DueSearchParameter.Overdue);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery CreatedWithinDay()
		{
			_parameters.Add(CreatedSearchParameter.Day);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery CreatedWithinWeek()
		{
			_parameters.Add(CreatedSearchParameter.Week);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery CreatedWithinMonth()
		{
			_parameters.Add(CreatedSearchParameter.Month);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery CreatedWithinDays(int days)
		{
			_parameters.Add(new CreatedSearchParameter(days));

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery EditedWithinDay()
		{
			_parameters.Add(EditedSearchParameter.Day);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery EditedWithinWeek()
		{
			_parameters.Add(EditedSearchParameter.Week);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery EditedWithinMonth()
		{
			_parameters.Add(EditedSearchParameter.Month);

			return this;
		}
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		public ISearchQuery EditedWithinDays(int days)
		{
			_parameters.Add(new EditedSearchParameter(days));

			return this;
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