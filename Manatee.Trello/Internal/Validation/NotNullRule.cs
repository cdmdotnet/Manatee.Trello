namespace Manatee.Trello.Internal.Validation
{
	internal class NotNullRule<T> : IValidationRule<T>
		where T : class
	{
		public static NotNullRule<T> Instance { get; private set; }

		static NotNullRule()
		{
			Instance = new NotNullRule<T>();
		}
		private NotNullRule() { }

		public string Validate(T oldValue, T newValue)
		{
			return newValue == null
				       ? "Value cannot be null"
				       : null;
		}
	}
}