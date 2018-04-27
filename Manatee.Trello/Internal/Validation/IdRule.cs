using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class IdRule : IValidationRule<string>
	{
		private static readonly Regex Regex = new Regex("^[a-z0-9]{24}$", RegexOptions.IgnoreCase);

		public static IdRule Instance { get; }

		static IdRule()
		{
			Instance = new IdRule();
		}
		private IdRule() {}

		public string Validate(string oldValue, string newValue)
		{
			return oldValue != null && Regex.IsMatch(oldValue) ? null : string.Empty;
		}
	}
}
