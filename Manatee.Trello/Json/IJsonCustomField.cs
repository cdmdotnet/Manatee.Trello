using System;

namespace Manatee.Trello.Json
{
	public interface IJsonCustomField : IJsonCacheable
	{
		IJsonCustomFieldDefinition Definition { get; set; }
		string Text { get; set; }
		double? Number { get; set; }
		DateTime? Date { get; set; }
		bool? Checked { get; set; }
	}
}