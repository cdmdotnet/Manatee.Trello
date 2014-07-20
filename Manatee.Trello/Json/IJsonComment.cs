namespace Manatee.Trello.Json
{
	public interface IJsonComment : IJsonAction
	{
		string Text { get; set; }
	}
}