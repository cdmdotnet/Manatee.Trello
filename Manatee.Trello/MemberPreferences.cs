using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
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

	/// <summary>
	/// Represents preferences for a member.
	/// </summary>
	public class MemberPreferences : IMemberPreferences
	{
		private readonly Field<bool?> _enableColorBlindMode;
		private readonly Field<int?> _minutesBetweenSummaries;
		private MemberPreferencesContext _context;

		/// <summary>
		/// Gets or sets whether color-blind mode is enabled.
		/// </summary>
		public bool? EnableColorBlindMode
		{
			get { return _enableColorBlindMode.Value; }
			set { _enableColorBlindMode.Value = value; }
		}
		/// <summary>
		/// Gets or sets the time between email summaries.
		/// </summary>
		public int? MinutesBetweenSummaries
		{
			get { return _minutesBetweenSummaries.Value; }
			set { _minutesBetweenSummaries.Value = value; }
		}

		internal MemberPreferences(MemberPreferencesContext context)
		{
			_context = context;

			_enableColorBlindMode = new Field<bool?>(_context, nameof(EnableColorBlindMode));
			_enableColorBlindMode.AddRule(NullableHasValueRule<bool>.Instance);
			_minutesBetweenSummaries = new Field<int?>(_context, nameof(MinutesBetweenSummaries));
			_minutesBetweenSummaries.AddRule(NullableHasValueRule<int>.Instance);
		}
	}
}