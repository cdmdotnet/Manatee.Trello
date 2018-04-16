using System.Collections.Generic;

namespace Manatee.Trello
{
	public interface ICustomFieldDefinition : ICacheable
	{
		IBoard Board { get; }
		string FieldGroup { get; }
		string Name { get; }
		Position Position { get; }
		CustomFieldType? Type { get; }
		IReadOnlyCollection<DropDownOption> Options { get; }
	}
}