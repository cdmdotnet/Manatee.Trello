using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the accepted values for the MinutesBetweenSummaries property on the
	/// MemberPreferences object.
	/// </summary>
	public enum MemberPreferenceSummaryPeriod
	{
		/// <summary>
		/// Indicates that summary emails are disabled.
		/// </summary>
		[Description("disabled")]
		Disabled = -1,
		/// <summary>
		/// Indicates that summary emails should be sent every minute, when notifications
		/// are present.
		/// </summary>
		[Description("oneMinute")]
		OneMinute = 1,
		/// <summary>
		/// Indicates that summary emails should be sent every hour, when notifications
		/// are present.
		/// </summary>
		[Description("oneHour")]
		OneHour = 60
	}
}