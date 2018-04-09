using System.Linq;
using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class OrganizationNameRule : IValidationRule<string>
	{
		private static readonly Regex Regex = new Regex("^[a-z0-9_]{3,}$");

		public static OrganizationNameRule Instance { get; }

		static OrganizationNameRule()
		{
			Instance = new OrganizationNameRule();
		}
		private OrganizationNameRule() { }

		public string Validate(string oldValue, string newValue)
		{
			var isValid = Regex.IsMatch(newValue);
			if (isValid)
			{
				var search = new Search(newValue, 10, SearchModelType.Organizations);
				isValid &= search.Organizations == null || search.Organizations.All(o => o.Name != newValue);
			}
			return !isValid
				       ? "Value must consist of at least three lowercase letters, number, or underscores and must be unique on Trello."
				       : null;
		}
	}
}