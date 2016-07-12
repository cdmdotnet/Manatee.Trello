/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Member.cs
	Namespace:		Manatee.Trello
	Class Name:		Member
	Purpose:		Represents a member.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a member.
	/// </summary>
	public class Member : ICanWebhook, ICacheable
	{
		private const string AvatarUrlFormat = "https://trello-avatars.s3.amazonaws.com/{0}/170.png";
		private static Me _me;

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
		/// Returns the <see cref="Member"/> associated with the current User Token.
		/// </summary>
		public static Me Me => _me ?? (_me = new Me());

		/// <summary>
		/// Gets the collection of actions performed by the member.
		/// </summary>
		public ReadOnlyActionCollection Actions { get; }
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
		public ReadOnlyBoardCollection Boards { get; }
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
					_context.Synchronize();
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
		/// Gets a string which can be used in comments or descriptions to mention another
		/// user.  The user will receive notification that they've been mentioned.
		/// </summary>
		public string Mention => $"@{UserName}";
		/// <summary>
		/// Gets the collection of organizations to which the member belongs.
		/// </summary>
		public ReadOnlyOrganizationCollection Organizations { get; }
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

#if IOS
		private Action<Member, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the member is updated.
		/// </summary>
		public event Action<Member, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the member is updated.
		/// </summary>
		public event Action<Member, IEnumerable<string>> Updated;
#endif

		/// <summary>
		/// Creates a new instance of the <see cref="Member"/> object.
		/// </summary>
		/// <param name="id">The member's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
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
		public void ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateMember || action.Data.Member == null || action.Data.Member.Id != Id) return;
			_context.Merge(action.Data.Member.Json);
		}
		/// <summary>
		/// Marks the member to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return FullName;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			handler?.Invoke(this, properties);
		}
		private string GetAvatar()
		{
			var hash = _avatarUrl.Value;
			return hash.IsNullOrWhiteSpace() ? null : string.Format(AvatarUrlFormat, hash);
		}
	}
}