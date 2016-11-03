namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the MemberPreferences object.
	/// </summary>
	public interface IJsonMemberPreferences
	{
		/// <summary>
		/// Gets or sets the number of minutes between summary emails.
		/// </summary>
		[JsonDeserialize]
		int? MinutesBetweenSummaries { get; set; }
		/// <summary>
		/// Enables/disables color-blind mode.
		/// </summary>
		[JsonDeserialize]
		bool? ColorBlind { get; set; }
	}
}