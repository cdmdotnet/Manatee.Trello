namespace Manatee.Trello
{
	public interface ICustomField : ICacheable
	{
		ICustomFieldDefinition Definition { get; }
	}

	public interface ICustomField<out T> : ICustomField
	{
		T Value { get; }
	}
}