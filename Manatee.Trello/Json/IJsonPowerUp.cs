namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the PowerUp object.
	/// </summary>
	public interface IJsonPowerUp : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[JsonDeserialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets whether the power-up is public.
		/// </summary>
		[JsonDeserialize]
		bool? Public { get; set; }
		/// <summary>
		/// Gets or sets the URL for more information about the power-up.
		/// </summary>
		[JsonDeserialize]
		string Url { get; set; }
	}
}
