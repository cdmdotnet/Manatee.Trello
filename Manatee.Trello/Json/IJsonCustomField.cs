using System;

namespace Manatee.Trello.Json
{
	public interface IJsonCustomField : IJsonCacheable, IAcceptId
	{
		IJsonCustomFieldDefinition Definition { get; set; }
		string Text { get; set; }
		double? Number { get; set; }
		DateTime? Date { get; set; }
		bool? Checked { get; set; }
		IJsonCustomDropDownOption Selected { get; set; }
		CustomFieldType Type { get; set; }
	}
}