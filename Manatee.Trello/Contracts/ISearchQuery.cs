namespace Manatee.Trello
{
	/// <summary>
	/// Builds a search query.
	/// </summary>
	public interface ISearchQuery
	{
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery Text(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card names.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery TextInName(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card descriptions.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery TextInDescription(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card comments.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery TextInComments(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to check lists.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery TextInCheckLists(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a member search parameter.
		/// </summary>
		/// <param name="member">The member to search for.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery Member(IMember member);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a label search parameter.
		/// </summary>
		/// <param name="label">The label to search for.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery Label(ILabel label);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a label color search parameter.
		/// </summary>
		/// <param name="labelColor">The label color to search for.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery Label(LabelColor labelColor);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only archived items.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery IsArchived();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only unarchived items.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery IsOpen();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only starred items.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery IsStarred();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next 24 hours.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery DueWithinDay();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next week.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery DueWithinWeek();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next month.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery DueWithinMonth();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next <paramref name="days"/> hours.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery DueWithinDays(int days);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items which are overdue.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery Overdue();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery CreatedWithinDay();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery CreatedWithinWeek();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery CreatedWithinMonth();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery CreatedWithinDays(int days);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery EditedWithinDay();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery EditedWithinWeek();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery EditedWithinMonth();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>The <see cref="ISearchQuery"/> instance.</returns>
		ISearchQuery EditedWithinDays(int days);
	}
}