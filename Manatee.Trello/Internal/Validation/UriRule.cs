using System;

namespace Manatee.Trello.Internal.Validation
{
	internal class UriRule : IValidationRule<string>
	{
		public static UriRule Instance { get; }

		static UriRule()
		{
			Instance = new UriRule();
		}
		private UriRule() { }

		public string Validate(string oldValue, string newValue)
		{
			return !(newValue.BeginsWith("http://") || newValue.BeginsWith("https://")) || !Uri.IsWellFormedUriString(newValue, UriKind.Absolute)
					   ? "Value must begin with \"http://\" or \"https://\" and be a valid URI."
					   : null;
		}
	}
}