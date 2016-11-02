namespace Manatee.Trello.Json
{
	public interface IJsonPowerUpData : IJsonCacheable
	{
		string PluginId { get; set; }
		string Value { get; set; }
	}
}
