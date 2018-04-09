using System.Linq;
using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class UsernameRule : IValidationRule<string>
	{
		private static readonly Regex Regex = new Regex("^[a-z0-9_]{3,}$");
		
		public static UsernameRule Instance { get; }

		static UsernameRule()
		{
			Instance = new UsernameRule();
		}
		private UsernameRule() { }

		public string Validate(string oldValue, string newValue)
		{
			var isValid = Regex.IsMatch(newValue);
			if (isValid)
			{
				var search = new MemberSearch(newValue);
				isValid &= search.Results == null || search.Results.All(o => o.Member.UserName != newValue);
			}
			return !isValid
					   ? "Value must consist of at least three lowercase letters, number, or underscores and must be unique on Trello."
					   : null;
		}
	}
}