namespace Manatee.Trello.Json
{
	public interface IJsonCustomDropDownOption : IJsonCacheable, IAcceptId
	{
		IJsonCustomFieldDefinition Field { get; set; }
		string Text { get; set; }
		LabelColor? Color { get; set; }
		IJsonPosition Pos { get; set; }
	}
}