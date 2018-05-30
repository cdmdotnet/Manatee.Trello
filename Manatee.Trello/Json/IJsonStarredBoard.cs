namespace Manatee.Trello.Json
{
	public interface IJsonStarredBoard : IJsonCacheable
	{
		IJsonBoard Board { get; set; }
		IJsonPosition Position { get; set; }
	}
}