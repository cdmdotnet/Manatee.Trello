namespace Manatee.Trello.Json
{
	public interface IJsonPowerUp : IJsonCacheable
	{
		string Name { get; set; }
		bool? Public { get; set; }
		string Url { get; set; }
	}
}
