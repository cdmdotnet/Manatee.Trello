using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a member.
	/// </summary>
	public class Member : IMember
	{
		/// <summary>
		/// Enumerates the data which can be pulled for members.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the AvatarHash property should be populated.
			/// </summary>
			[Display(Description="avatarHash")]
			AvatarHash = 1,
			/// <summary>
			/// Indicates the AvatarSource property should be populated.
			/// </summary>
			[Display(Description="avatarSource")]
			AvatarSource = 1 << 1,
			/// <summary>
			/// Indicates the Bio property should be populated.
			/// </summary>
			[Display(Description="bio")]
			Bio = 1 << 2,
			/// <summary>
			/// Indicates the IsConfirmed property should be populated.
			/// </summary>
			[Display(Description="confirmed")]
			IsConfirmed = 1 << 3,
			/// <summary>
			/// Indicates the Email property should be populated.
			/// </summary>
			[Display(Description="email")]
			Email = 1 << 4,
			/// <summary>
			/// Indicates the FullName property should be populated.
			/// </summary>
			[Display(Description="fullName")]
			FullName = 1 << 5,
			/// <summary>
			/// Indicates the GravatarHash property should be populated.
			/// </summary>
			[Display(Description="gravatarHash")]
			GravatarHash = 1 << 6,
			/// <summary>
			/// Indicates the Initials property should be populated.
			/// </summary>
			[Display(Description="intials")]
			Initials = 1 << 7,
			/// <summary>
			/// Indicates the LoginTypes property should be populated.
			/// </summary>
			[Display(Description="loginTypes")]
			LoginTypes = 1 << 8,
			/// <summary>
			/// Indicates the MemberType property should be populated.
			/// </summary>
			[Display(Description="memberType")]
			MemberType = 1 << 9,
			/// <summary>
			/// Indicates the OneTimeMessagesDismissed property should be populated.
			/// </summary>
			[Display(Description="oneTimeMessagesReceived")]
			OneTimeMessagesDismissed = 1 << 10,
			/// <summary>
			/// Indicates the Preferencess property should be populated.
			/// </summary>
			[Display(Description="prefs")]
			Preferencess = 1 << 11,
			/// <summary>
			/// Indicates the Similarity property should be populated.
			/// </summary>
			[Display(Description="similarity")]
			Similarity = 1 << 12,
			/// <summary>
			/// Indicates the Status property should be populated.
			/// </summary>
			[Display(Description="status")]
			Status = 1 << 13,
			/// <summary>
			/// Indicates the Trophies property should be populated.
			/// </summary>
			[Display(Description="trophies")]
			Trophies = 1 << 14,
			/// <summary>
			/// Indicates the UploadedAvatarHash property should be populated.
			/// </summary>
			[Display(Description="uploadedAvatarHash")]
			UploadedAvatarHash = 1 << 15,
			/// <summary>
			/// Indicates the Url property should be populated.
			/// </summary>
			[Display(Description="url")]
			Url = 1 << 16,
			/// <summary>
			/// Indicates the Username property should be populated.
			/// </summary>
			[Display(Description="username")]
			Username = 1 << 17
		}

		private const string _avatarUrlFormat = "https://trello-avatars.s3.amazonaws.com/{0}/170.png";

		private readonly Field<AvatarSource?> _avatarSource;
		private readonly Field<string> _avatarUrl;
		private readonly Field<string> _bio;
		private readonly Field<string> _fullName;
		private readonly Field<string> _initials;
		private readonly Field<bool?> _isConfirmed;
		private readonly Field<MemberStatus?> _status;
		private readonly Field<IEnumerable<string>> _trophies;
		private readonly Field<string> _url;
		private readonly Field<string> _userName;
		internal readonly MemberContext _context;

		private string _id;
		private DateTime? _creation;

		/// <summary>
		/// Specifies which fields should be downloaded.
		/// </summary>
		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

		/// <summary>
		/// Gets the collection of actions performed by the member.
		/// </summary>
		public IReadOnlyCollection<IAction> Actions { get; }
		/// <summary>
		/// Gets the source type for the member's avatar.
		/// </summary>
		public AvatarSource? AvatarSource
		{
			get { return _avatarSource.Value; }
			internal set { _avatarSource.Value = value; }
		}
		/// <summary>
		/// Gets the URL to the member's avatar.
		/// </summary>
		public string AvatarUrl => GetAvatar();
		/// <summary>
		/// Gets the member's bio.
		/// </summary>
		public string Bio
		{
			get { return _bio.Value; }
			internal set { _bio.Value = value; }
		}
		/// <summary>
		/// Gets the collection of boards owned by the member.
		/// </summary>
		public IReadOnlyCollection<IBoard> Boards { get; }
		/// <summary>
		/// Gets the creation date of the member.
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
		/// Gets the member's full name.
		/// </summary>
		public string FullName
		{
			get { return _fullName.Value; }
			internal set { _fullName.Value = value; }
		}
		/// <summary>
		/// Gets the member's ID.
		/// </summary>
		public string Id
		{
			get
			{
				if (!_context.HasValidId)
					_context.Synchronize().Wait();
				return _id;
			}
			private set { _id = value; }
		}
		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		public string Initials
		{
			get { return _initials.Value; }
			internal set { _initials.Value = value; }
		}
		/// <summary>
		/// Gets whether the member has actually join or has merely been invited (ghost).
		/// </summary>
		public bool? IsConfirmed => _isConfirmed.Value;
		/// <summary>
		/// Gets a string which can be used in comments or descriptions to mention another user.  The user will receive notification that they've been mentioned.
		/// </summary>
		public string Mention => $"@{UserName}";
		/// <summary>
		/// Gets the collection of organizations to which the member belongs.
		/// </summary>
		public IReadOnlyCollection<IOrganization> Organizations { get; }
		/// <summary>
		/// Gets the member's online status.
		/// </summary>
		public MemberStatus? Status => _status.Value;
		/// <summary>
		/// Gets the collection of trophies earned by the member.
		/// </summary>
		public IEnumerable<string> Trophies => _trophies.Value;
		/// <summary>
		/// Gets the member's URL.
		/// </summary>
		public string Url => _url.Value;
		/// <summary>
		/// Gets the member's username.
		/// </summary>
		public string UserName
		{
			get { return _userName.Value; }
			internal set { _userName.Value = value; }
		}

		internal IJsonMember Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}
		internal TrelloAuthorization Auth { get; }

		/// <summary>
		/// Raised when data on the member is updated.
		/// </summary>
		public event Action<IMember, IEnumerable<string>> Updated;

		/// <summary>
		/// Creates a new instance of the <see cref="Member"/> object.
		/// </summary>
		/// <param name="id">The member's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <remarks>
		/// The supplied ID can be either the full ID or the username.
		/// </remarks>
		public Member(string id, TrelloAuthorization auth = null)
			: this(id, false, auth) {}
		internal Member(string id, bool isMe, TrelloAuthorization auth)
		{
			Auth = auth;
			Id = id;
			_context = new MemberContext(id, auth);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(Member), () => Id, auth);
			_avatarSource = new Field<AvatarSource?>(_context, nameof(AvatarSource));
			_avatarSource.AddRule(NullableHasValueRule<AvatarSource>.Instance);
			_avatarSource.AddRule(EnumerationRule<AvatarSource?>.Instance);
			_avatarUrl = new Field<string>(_context, nameof(AvatarUrl));
			_bio = new Field<string>(_context, nameof(Bio));
			Boards = isMe ? new BoardCollection(typeof(Member), () => Id, auth) : new ReadOnlyBoardCollection(typeof(Member), () => Id, auth);
			_fullName = new Field<string>(_context, nameof(FullName));
			_fullName.AddRule(MemberFullNameRule.Instance);
			_initials = new Field<string>(_context, nameof(Initials));
			_initials.AddRule(MemberInitialsRule.Instance);
			_isConfirmed = new Field<bool?>(_context, nameof(IsConfirmed));
			Organizations = isMe ? new OrganizationCollection(() => Id, auth) : new ReadOnlyOrganizationCollection(() => Id, auth);
			_status = new Field<MemberStatus?>(_context, nameof(Status));
			_trophies = new Field<IEnumerable<string>>(_context, nameof(Trophies));
			_url = new Field<string>(_context, nameof(Url));
			_userName = new Field<string>(_context, nameof(UserName));
			_userName.AddRule(UsernameRule.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Member(IJsonMember json, TrelloAuthorization auth)
			: this(json.Id, false, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		public void ApplyAction(IAction action)
		{
			if (action.Type != ActionType.UpdateMember || action.Data.Member == null || action.Data.Member.Id != Id) return;
			_context.Merge(((Member) action.Data.Member).Json);
		}
		/// <summary>
		/// Marks the member to be refreshed the next time data is accessed.
		/// </summary>
		public async Task Refresh()
		{
			await _context.Expire();
		}
		/// <summary>
		/// Returns the <see cref="FullName"/>.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override string ToString()
		{
			return FullName;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
		private string GetAvatar()
		{
			var hash = _avatarUrl.Value;
			return hash.IsNullOrWhiteSpace() ? null : string.Format(_avatarUrlFormat, hash);
		}
	}
}