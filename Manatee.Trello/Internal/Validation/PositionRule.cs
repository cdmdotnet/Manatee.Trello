namespace Manatee.Trello.Internal.Validation
{
	internal class PositionRule : IValidationRule<Position>
	{
		public static PositionRule Instance { get; }

		static PositionRule()
		{
			Instance = new PositionRule();
		}
		private PositionRule() { }

		public string Validate(Position oldValue, Position newValue)
		{
			return newValue == null || !newValue.IsValid
					   ? "Value must be non-null and positive."
					   : null;
		}
	}
}