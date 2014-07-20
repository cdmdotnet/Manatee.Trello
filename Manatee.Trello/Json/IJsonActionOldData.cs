namespace Manatee.Trello.Json
{
	public interface IJsonActionOldData
	{
		string Desc { get; set; }
		IJsonList List { get; set; }
		double? Pos { get; set; }
		string Text { get; set; }
		bool? Closed { get; set; }
	}
}