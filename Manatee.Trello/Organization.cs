using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an organization.
	/// </summary>
	public class Organization : IOrganization, IMergeJson<IJsonOrganization>
	{
		/// <summary>
		/// Enumerates the data which can be pulled for organizations (teams).
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the Description property should be populated.
			/// </summary>
			[Display(Description="desc")]
			Description = 1,
			/// <summary>
			/// Indicates the DisplayName property should be populated.
			/// </summary>
			[Display(Description="displayName")]
			DisplayName = 1 << 1,
			/// <summary>
			/// Indicates the LogoHash property should be populated.
			/// </summary>
			[Display(Description="logoHash")]
			LogoHash = 1 << 2,
			/// <summary>
			/// Indicates the Name property should be populated.
			/// </summary>
			[Display(Description="name")]
			Name = 1 << 3,
			/// <summary>
			/// Indicates the Preferences property should be populated.
			/// </summary>
			[Display(Description="prefs")]
			Preferences = 1 << 6,
			/// <summary>
			/// Indicates the Url property should be populated.
			/// </summary>
			[Display(Description="url")]
			Url = 1 << 7,
			/// <summary>
			/// Indicates the Website property should be populated.
			/// </summary>
			[Display(Description="website")]
			Website = 1 << 8,
			/// <summary>
			/// Indicates the actions will be downloaded.
			/// </summary>
			Actions = 1 << 9,
			/// <summary>
			/// Indicates the boards will be downloaded.
			/// </summary>
			Boards = 1 << 10,
			/// <summary>
			/// Indicates the members will be downloaded. Not included by default.
			/// </summary>
			Members = 1 << 11,
			/// <summary>
			/// Indicates the memberships will be downloaded.
			/// </summary>
			Memberships = 1 << 12,
			/// <summary>
			/// Indicates the power-up data will be downloaded.
			/// </summary>
			PowerUpData = 1 << 13
		}

		private readonly Field<string> _description;
		private readonly Field<string> _displayName;
		private readonly Field<bool> _isBusinessClass;
		private readonly Field<string> _name;
		private readonly Field<string> _url;
		private readonly Field<string> _website;
		private readonly OrganizationContext _context;

		private string _id;
		private DateTime? _creation;
		private static Fields _downloadedFields;

		/// <summary>
		/// Specifies which fields should be downloaded.
		/// </summary>
		public static Fields DownloadedFields
		{
			get { return _downloadedFields; }
			set
			{
				_downloadedFields = value;
				OrganizationContext.UpdateParameters();
			}
		}

		/// <summary>
		/// Gets the collection of actions performed on the organization.
		/// </summary>
		public IReadOnlyCollection<IAction> Actions => _context.Actions;
		/// <summary>
		/// Gets the collection of boards owned by the organization.
		/// </summary>
		public IBoardCollection Boards => _context.Boards;
		/// <summary>
		/// Gets the creation date of the organization.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets or sets the organization's description.
		/// </summary>
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		/// <summary>
		/// Gets or sets the organization's display name.
		/// </summary>
		public string DisplayName
		{
			get { return _displayName.Value; }
			set { _displayName.Value = value; }
		}
		/// <summary>
		/// Gets the organization's ID.
		/// </summary>
		public string Id
		{
			get
			{
				if (!_context.HasValidId)
					_context.Synchronize(CancellationToken.None).Wait();
				return _id;
			}
			private set { _id = value; }
		}
		/// <summary>
		/// Gets whether the organization has business class status.
		/// </summary>
		public bool IsBusinessClass => _isBusinessClass.Value;

		/// <summary>
		/// Gets the collection of members who belong to the organization.
		/// </summary>
		public IReadOnlyCollection<IMember> Members => _context.Members;
		/// <summary>
		/// Gets the collection of members and their priveledges on this organization.
		/// </summary>
		public IOrganizationMembershipCollection Memberships => _context.Memberships;
		/// <summary>
		/// Gets the organization's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets specific data regarding power-ups.
		/// </summary>
		public IReadOnlyCollection<IPowerUpData> PowerUpData => _context.PowerUpData;
		/// <summary>
		/// Gets the set of preferences for the organization.
		/// </summary>
		public IOrganizationPreferences Preferences { get; }
		/// <summary>
		/// Gets the organization's URL.
		/// </summary>
		public string Url => _url.Value;
		/// <summary>
		/// Gets or sets the organization's website.
		/// </summary>
		public string Website
		{
			get { return _website.Value; }
			set { _website.Value = value; }
		}

		internal IJsonOrganization Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the organization is updated.
		/// </summary>
		public event Action<IOrganization, IEnumerable<string>> Updated;

		static Organization()
		{
			DownloadedFields = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum() &
				~Fields.Members;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Organization"/> object.
		/// </summary>
		/// <param name="id">The organization's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <remarks>
		/// The supplied ID can be either the full ID or the organization's name.
		/// </remarks>
		public Organization(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new OrganizationContext(id, auth);
			_context.Synchronized += Synchronized;

			_description = new Field<string>(_context, nameof(Description));
			_displayName = new Field<string>(_context, nameof(DisplayName));
			_isBusinessClass = new Field<bool>(_context, nameof(IsBusinessClass));
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(OrganizationNameRule.Instance);
			Preferences = new OrganizationPreferences(_context.OrganizationPreferencesContext);
			_url = new Field<string>(_context, nameof(Url));
			_website = new Field<string>(_context, nameof(Website));
			_website.AddRule(UriRule.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Organization(IJsonOrganization json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		public void ApplyAction(IAction action)
		{
			if (action.Type != ActionType.UpdateOrganization || action.Data.Organization == null || action.Data.Organization.Id != Id)
				return;
			_context.Merge(((Organization) action.Data.Organization).Json);
		}

		/// <summary>
		/// Deletes the organization.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the organization from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		/// <summary>
		/// Refreshes the organization data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
		}

		void IMergeJson<IJsonOrganization>.Merge(IJsonOrganization json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return DisplayName;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}