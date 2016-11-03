namespace Manatee.Trello.Internal.Validation
{
	internal interface IValidationRule<in T>
	{
		string Validate(T oldValue, T newValue);
	}
}