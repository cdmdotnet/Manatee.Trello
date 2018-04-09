namespace Manatee.Trello.Internal.Validation
{
	internal class NotNullOrWhiteSpaceRule : IValidationRule<string>
	{
		public static NotNullOrWhiteSpaceRule Instance { get; }

		static NotNullOrWhiteSpaceRule()
		{
			Instance = new NotNullOrWhiteSpaceRule();
		}
		private NotNullOrWhiteSpaceRule() { }

		public string Validate(string oldValue, string newValue)
		{
			return newValue.IsNullOrWhiteSpace()
				       ? "Value cannot be null, empty, or whitespace."
				       : null;
		}
	}
}