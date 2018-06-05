using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides a base implementation for Trello Power-Ups.
	/// </summary>
	public abstract class PowerUpBase : IPowerUp, IMergeJson<IJsonPowerUp>
	{
		private readonly Field<string> _name;
		private readonly Field<bool?> _isPublic;
		private readonly Field<string> _additionalInfo;
		private readonly PowerUpContext _context;

		/// <summary>
		/// Gets a URI that provides JSON-formatted data about the plugin.
		/// </summary>
		public string AdditionalInfo => _additionalInfo.Value;
		/// <summary>
		/// Gets the power-up's ID.
		/// </summary>
		public string Id { get; }
		/// <summary>
		/// Gets or sets whether this power-up is closed.
		/// </summary>
		public bool? IsPublic => _isPublic.Value;
		/// <summary>
		/// Gets the name of the power-up.
		/// </summary>
		public string Name => _name.Value;

		internal IJsonPowerUp Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Initializes a power-up.
		/// </summary>
		protected PowerUpBase(IJsonPowerUp json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new PowerUpContext(json, auth);

			_additionalInfo = new Field<string>(_context, nameof(AdditionalInfo));
			_name = new Field<string>(_context, nameof(Name));
			_isPublic = new Field<bool?>(_context, nameof(IsPublic));
		}
		/// <summary>
		/// Refreshes the power-up data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			return _context.Synchronize(force, ct);
		}

		void IMergeJson<IJsonPowerUp>.Merge(IJsonPowerUp json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		/// <summary>Returns the <see cref="Name"/></summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return Name;
		}
	}
}