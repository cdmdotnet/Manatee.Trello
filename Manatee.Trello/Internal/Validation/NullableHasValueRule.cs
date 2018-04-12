namespace Manatee.Trello.Internal.Validation
{
	internal class NullableHasValueRule<T> : IValidationRule<T?>
		where T : struct
	{
		public static NullableHasValueRule<T> Instance { get; }

		static NullableHasValueRule()
		{
			Instance = new NullableHasValueRule<T>();
		}
		private NullableHasValueRule() { }

		public string Validate(T? oldValue, T? newValue)
		{
			return !newValue.HasValue
				       ? "Value cannot be null"
				       : null;
		}
	}
}