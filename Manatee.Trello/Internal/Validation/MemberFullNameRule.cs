using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class MemberFullNameRule : IValidationRule<string>
	{
		private static readonly Regex Regex = new Regex(@"^(\S.{2,}\S)|\S$");

		public static MemberFullNameRule Instance { get; private set; }

		static MemberFullNameRule()
		{
			Instance = new MemberFullNameRule();
		}
		private MemberFullNameRule() { }

		public string Validate(string oldValue, string newValue)
		{
			var isValid = Regex.IsMatch(newValue);
			return !isValid
				       ? "Value must consist of at least four characters and cannot begin or end with whitespace."
				       : null;
		}
	}
}