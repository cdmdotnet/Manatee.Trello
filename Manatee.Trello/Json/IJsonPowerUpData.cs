namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the PowerUpData object.
	/// </summary>
	public interface IJsonPowerUpData : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the power-up ID.
		/// </summary>
		string PluginId { get; set; }
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		string Value { get; set; }
	}
}
