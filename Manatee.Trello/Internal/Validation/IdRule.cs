using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class IdRule : IValidationRule<string>
	{
		private static readonly Regex _regex = new Regex("^[a-z0-9]{24}$", RegexOptions.IgnoreCase);

		public static IdRule Instance { get; private set; }

		static IdRule()
		{
			Instance = new IdRule();
		}
		private IdRule() {}

		public string Validate(string oldValue, string newValue)
		{
			return _regex.IsMatch(oldValue) ? null : string.Empty;
		}
	}
}
