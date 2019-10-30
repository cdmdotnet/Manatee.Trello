using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a user token.
	/// </summary>
	public class Token : IToken, IMergeJson<IJsonToken>, IBatchRefresh, IHandleSynchronization
	{
		/// <summary>
		/// Enumerates the data which can be pulled for tokens.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the Id property should be populated.
			/// </summary>
			[Display(Description="identifier")]
			Id = 1,
			/// <summary>
			/// Indicates the Member property should be populated.
			/// </summary>
			[Display(Description="idMember")]
			Member = 1 << 1,
			/// <summary>
			/// Indicates the DateCreated property should be populated.
			/// </summary>
			[Display(Description="dateCreated")]
			DateCreated = 1 << 2,
			/// <summary>
			/// Indicates the DateExpires property should be populated.
			/// </summary>
			[Display(Description="dateExpires")]
			DateExpires = 1 << 3,
			/// <summary>
			/// Indicates the Permissions property should be populated.
			/// </summary>
			[Display(Description="permissions")]
			Permissions = 1 << 4,
			// TODO: (#164) add
			//Webhooks
		}

		private readonly Field<string> _appName;
		private readonly Field<DateTime?> _dateCreated;
		private readonly Field<DateTime?> _dateExpires;
		private readonly Field<Member> _member;
		private readonly TokenContext _context;

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
				TokenContext.UpdateParameters();
			}
		}

		/// <summary>
		/// Gets the name of the application associated with the token.
		/// </summary>
		public string AppName => _appName.Value;
		/// <summary>
		/// Gets the permissions on boards granted by the token.
		/// </summary>
		public ITokenPermission BoardPermissions { get; }
		/// <summary>
		/// Gets the creation date of the token.
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
		/// Gets the date and time the token was created.
		/// </summary>
		public DateTime? DateCreated => _dateCreated.Value;
		/// <summary>
		/// Gets the date and time the token expires, if any.
		/// </summary>
		public DateTime? DateExpires => _dateExpires.Value;
		/// <summary>
		/// Gets the token's ID.
		/// </summary>
		public string Id
		{
			get
			{
				if (!_context.HasValidId)
					_context.Synchronize(true, CancellationToken.None).Wait();
				return _id;
			}
			private set { _id = value; }
		}
		/// <summary>
		/// Gets the member for which the token was issued.
		/// </summary>
		public IMember Member => _member.Value;
		/// <summary>
		/// Gets the permissions on members granted by the token.
		/// </summary>
		public ITokenPermission MemberPermissions { get; }
		/// <summary>
		/// Gets the permissions on organizations granted by the token.
		/// </summary>
		public ITokenPermission OrganizationPermissions { get; }

		TrelloAuthorization IBatchRefresh.Auth => _context.Auth;

		static Token()
		{
			DownloadedFields = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Token"/> object.
		/// </summary>
		/// <param name="id">The token's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <remarks>
		/// The supplied ID can be either the full ID or the token itself.
		/// </remarks>
		public Token(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new TokenContext(id, auth);
			_context.Synchronized.Add(this);

			_appName = new Field<string>(_context, nameof(AppName));
			BoardPermissions = new TokenPermission(_context.BoardPermissions);
			_dateCreated = new Field<DateTime?>(_context, nameof(DateCreated));
			_dateExpires = new Field<DateTime?>(_context, nameof(DateExpires));
			_member = new Field<Member>(_context, nameof(Member));
			MemberPermissions = new TokenPermission(_context.MemberPermissions);
			OrganizationPermissions = new TokenPermission(_context.OrganizationPermissions);

			if (auth != TrelloAuthorization.Null)
				TrelloConfiguration.Cache.Add(this);
		}
		internal Token(IJsonToken json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Deletes the token.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the token from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default)
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		/// <summary>
		/// Refreshes the token data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default)
		{
			return _context.Synchronize(force, ct);
		}

		void IMergeJson<IJsonToken>.Merge(IJsonToken json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		Endpoint IBatchRefresh.GetRefreshEndpoint()
		{
			return _context.GetRefreshEndpoint();
		}

		void IBatchRefresh.Apply(string content)
		{
			var json = TrelloConfiguration.Deserializer.Deserialize<IJsonToken>(content);
			_context.Merge(json);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return AppName;
		}

		void IHandleSynchronization.HandleSynchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
		}
	}
}