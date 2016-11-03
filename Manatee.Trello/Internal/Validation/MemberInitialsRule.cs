using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class MemberInitialsRule : IValidationRule<string>
	{
		private static readonly Regex _regex = new Regex(@"^(\S.{0,2}\S)|\S$");

		public static MemberInitialsRule Instance { get; private set; }

		static MemberInitialsRule()
		{
			Instance = new MemberInitialsRule();
		}
		private MemberInitialsRule() { }

		public string Validate(string oldValue, string newValue)
		{
			var isValid = _regex.IsMatch(newValue);
			return !isValid
					   ? "Value must consist of between one and three characters and cannot begin or end with whitespace."
					   : null;
		}
	}
}