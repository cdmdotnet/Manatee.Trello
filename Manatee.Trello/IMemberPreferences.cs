namespace Manatee.Trello
{
	/// <summary>
	/// Represents preferences for a member.
	/// </summary>
	public interface IMemberPreferences
	{
		/// <summary>
		/// Gets or sets whether color-blind mode is enabled.
		/// </summary>
		bool? EnableColorBlindMode { get; set; }

		/// <summary>
		/// Gets or sets the time between email summaries.
		/// </summary>
		int? MinutesBetweenSummaries { get; set; }
	}
}