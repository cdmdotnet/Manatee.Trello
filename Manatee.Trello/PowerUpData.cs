using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the data associated with a plugin.
	/// </summary>
	public class PowerUpData : IPowerUpData, IMergeJson<IJsonPowerUpData>
	{
		private readonly Field<string> _pluginId;
		private readonly Field<string> _value;
		private readonly PowerUpDataContext _context;

		/// <summary>
		/// Gets the ID associated with this particular data instance.
		/// </summary>
		public string Id { get; }
		/// <summary>
		/// Gets the ID for the power-up with which this data is associated.
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
			
			if (auth != TrelloAuthorization.Null) {
				TrelloConfiguration.Cache.Add(this);
			}
		}
		/// <summary>
		/// Refreshes the power-up data... data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default)
		{
			return _context.Synchronize(force, ct);
		}

		void IMergeJson<IJsonPowerUpData>.Merge(IJsonPowerUpData json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}
	}
}
