namespace Manatee.Trello
{
	public interface ISearchQuery
	{
		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns></returns>
		ISearchQuery Text(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card names.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery TextInName(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card descriptions.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery TextInDescription(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to card comments.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery TextInComments(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a text search parameter specific to check lists.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery TextInCheckLists(string text);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a member search parameter.
		/// </summary>
		/// <param name="member">The member to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery Member(IMember member);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a label search parameter.
		/// </summary>
		/// <param name="label">The label to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery Label(ILabel label);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a label color search parameter.
		/// </summary>
		/// <param name="labelColor">The label color to search for.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery Label(LabelColor labelColor);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only archived items.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery IsArchived();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only unarchived items.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery IsOpen();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only starred items.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery IsStarred();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next 24 hours.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery DueWithinDay();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next week.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery DueWithinWeek();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next month.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery DueWithinMonth();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the next <paramref name="days"/> hours.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery DueWithinDays(int days);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items which are overdue.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery Overdue();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery CreatedWithinDay();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery CreatedWithinWeek();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery CreatedWithinMonth();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery CreatedWithinDays(int days);

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past 24 hours.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery EditedWithinDay();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past week.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery EditedWithinWeek();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past month.
		/// </summary>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery EditedWithinMonth();

		/// <summary>
		/// Creates a new <see cref="ISearchQuery"/> specifying a search parameter to restrict to only items due in the past <paramref name="days"/> days.
		/// </summary>
		/// <param name="days">The number of days.</param>
		/// <returns>A new <see cref="ISearchQuery"/> parameter list.</returns>
		ISearchQuery EditedWithinDays(int days);
	}
}