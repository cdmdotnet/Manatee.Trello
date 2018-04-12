using System.Collections.Generic;

namespace Manatee.Trello
{
	public interface ICustomFieldDefinition : ICacheable
	{
		Board Board { get; }
		string FieldGroup { get; }
		string Name { get; }
		Position Position { get; }
		CustomFieldType? Type { get; }
		IEnumerable<DropDownOption> Options { get; }
		string ToString();
	}
}