using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a collaborator
	/// </summary>
	public class Collaborator : ICollaborator, IMergeJson<IJsonCollaborator>, IBatchRefresh, IHandleSynchronization
	{
		/// <summary>
		/// Enumerates the data which can be pulled for collaborators.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the AvatarHash property should be populated.
			/// </summary>
			[Display(Description="avatarHash")]
			[Obsolete("Trello has deprecated this property.")]
			AvatarHash = 1,
			/// <summary>
			/// Indicates the AvatarSource property should be populated.
			/// </summary>
			[Display(Description="avatarSource")]
			[Obsolete("Trello has deprecated this property.")]
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
			/// Indicates the Initials property should be populated.
			/// </summary>
			[Display(Description="initials")]
			Initials = 1 << 7,
			/// <summary>
			/// Indicates the MemberType property should be populated.
			/// </summary>
			[Display(Description="memberType")]
			MemberType = 1 << 9,
			/// <summary>
			/// Indicates the Status property should be populated.
			/// </summary>
			[Display(Description="status")]
			Status = 1 << 13,
			/// <summary>
			/// Indicates the Url property should be populated.
			/// </summary>
			[Display(Description="url")]
			Url = 1 << 16,
			/// <summary>
			/// Indicates the Username property should be populated.
			/// </summary>
			[Display(Description = "username")]
			Username = 1 << 17,
			/// <summary>
			/// Indicates the avatar URL should be downloaded.
			/// </summary>
			[Display(Description = "avatarUrl")]
			AvatarUrl = 1 << 23,
		}

		private readonly Field<string> _bio;
		private readonly Field<string> _fullName;
		private readonly Field<string> _initials;
		private readonly Field<MemberStatus?> _status;
		private readonly Field<string> _url;
		private readonly Field<string> _userName;
		internal readonly CollaboratorContext _context;

		private string _id;
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
				CollaboratorContext.UpdateParameters();
			}
		}
		/// <summary>
		/// Gets the source type for the collaborator's avatar.
		/// </summary>
		[Obsolete("Trello has deprecated this property.")]
		public AvatarSource? AvatarSource => null;

		/// <summary>
		/// Gets the collaborator's bio.
		/// </summary>
		public string Bio
		{
			get { return _bio.Value; }
			internal set { _bio.Value = value; }
		}
		/// <summary>
		/// Gets the collaborator's full name.
		/// </summary>
		public string FullName
		{
			get { return _fullName.Value; }
			internal set { _fullName.Value = value; }
		}
		/// <summary>
		/// Gets the collaborator's ID.
		/// </summary>
		public string Id
		{
			get
			{
				if (!_context.HasValidId)
					Task.Run(async () => { await _context.Synchronize(true, CancellationToken.None); }).Wait();
				return _id;
			}
			private set { _id = value; }
		}
		/// <summary>
		/// Gets or sets the collaborator's initials.
		/// </summary>
		public string Initials
		{
			get { return _initials.Value; }
			internal set { _initials.Value = value; }
		}
		/// <summary>
		/// Gets a string which can be used in comments or descriptions to mention another user.  The user will receive notification that they've been mentioned.
		/// </summary>
		public string Mention => $"@{UserName}";
		/// <summary>
		/// Gets the collaborator's online status.
		/// </summary>
		public MemberStatus? Status => _status.Value;
		/// <summary>
		/// Gets the collaborator's URL.
		/// </summary>
		public string Url => _url.Value;
		/// <summary>
		/// Gets the collaborator's username.
		/// </summary>
		public string UserName
		{
			get { return _userName.Value; }
			internal set { _userName.Value = value; }
		}

		internal IJsonCollaborator Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}
		internal TrelloAuthorization Auth => _context.Auth;
		TrelloAuthorization IBatchRefresh.Auth => _context.Auth;

		/// <summary>
		/// Raised when data on the collaborator is updated.
		/// </summary>
		public event Action<ICollaborator, IEnumerable<string>> Updated;

		static Collaborator()
		{
			DownloadedFields = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Collaborator"/> object.
		/// </summary>
		/// <param name="id">The collaborator's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <remarks>
		/// The supplied ID can be either the full ID or the username.
		/// </remarks>
		public Collaborator(string id, TrelloAuthorization auth = null)
			: this(id, false, auth) {}
		internal Collaborator(string id, bool isMe, TrelloAuthorization auth)
		{
			Id = id;
			_context = new CollaboratorContext(id, isMe, auth);
			_context.Synchronized.Add(this);

			_bio = new Field<string>(_context, nameof(Bio));
			_fullName = new Field<string>(_context, nameof(FullName));
			_fullName.AddRule(MemberFullNameRule.Instance);
			_initials = new Field<string>(_context, nameof(Initials));
			_initials.AddRule(MemberInitialsRule.Instance);
			_status = new Field<MemberStatus?>(_context, nameof(Status));
			_url = new Field<string>(_context, nameof(Url));
			_userName = new Field<string>(_context, nameof(UserName));
			_userName.AddRule(UsernameRule.Instance);

			if (auth != TrelloAuthorization.Null)
				TrelloConfiguration.Cache.Add(this);
		}
		internal Collaborator(IJsonCollaborator json, TrelloAuthorization auth)
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
			var localAction = action as Action;

			if (action.Type != ActionType.UpdateMember || 
			    localAction?.Json?.Data?.Member == null ||
			    localAction.Data.Member.Id != Id) return;

			_context.Merge(localAction.Json.Data.Collaborator);
		}

		/// <summary>
		/// Refreshes the collaborator data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			return _context.Synchronize(force, ct);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return FullName;
		}

		void IMergeJson<IJsonCollaborator>.Merge(IJsonCollaborator json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		Endpoint IBatchRefresh.GetRefreshEndpoint()
		{
			return _context.GetRefreshEndpoint();
		}

		void IBatchRefresh.Apply(string content)
		{
			var json = TrelloConfiguration.Deserializer.Deserialize<IJsonCollaborator>(content);
			_context.Merge(json);
		}

		void IHandleSynchronization.HandleSynchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}