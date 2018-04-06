using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	public interface IJsonCustomFieldDefinition : IJsonCacheable
	{
		IJsonBoard Board { get; set; }
		string FieldGroup { get; set; }
		string Name { get; set; }
		IJsonPosition Pos { get; set; }
		CustomFieldType? Type { get; set; }
		List<IJsonCustomDropDownOption> Options { get; set; }
	}
}