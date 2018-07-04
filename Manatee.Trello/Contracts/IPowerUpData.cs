namespace Manatee.Trello
{
	/// <summary>
	/// Represents the data associated with a power-up.
	/// </summary>
	public interface IPowerUpData : ICacheable, IRefreshable
	{
		/// <summary>
		/// Gets the ID for the plugin with which this data is associated.
		/// </summary>
		string PluginId { get; }

		/// <summary>
		/// Gets the data as a string.  This data will be JSON-encoded.
		/// </summary>
		string Value { get; }
	}
}