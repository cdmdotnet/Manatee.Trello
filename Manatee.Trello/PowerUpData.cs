using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public interface IPowerUpData : ICacheable
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

	/// <summary>
	/// Represents the data associated with a plugin.
	/// </summary>
	public class PowerUpData : IPowerUpData
	{
		private readonly Field<string> _pluginId;
		private readonly Field<string> _value;
		private readonly PowerUpDataContext _context;

		/// <summary>
		/// Gets the ID associated with this particular data instance.
		/// </summary>
		public string Id { get; }
		/// <summary>
		/// Gets the ID for the plugin with which this data is associated.
		/// </summary>
		public string PluginId => _pluginId.Value;
		/// <summary>
		/// Gets the data as a string.  This data will be JSON-encoded.
		/// </summary>
		public string Value => _value.Value;

		internal IJsonPowerUpData Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal PowerUpData(IJsonPowerUpData json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new PowerUpDataContext(Id, auth);

			_pluginId = new Field<string>(_context, nameof(PluginId));
			_value = new Field<string>(_context, nameof(Value));
		}
	}
}
