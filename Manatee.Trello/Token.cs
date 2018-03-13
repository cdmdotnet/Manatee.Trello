﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a user token.
	/// </summary>
	public interface IToken : ICacheable
	{
		/// <summary>
		/// Gets the name of the application associated with the token.
		/// </summary>
		string AppName { get; }

		/// <summary>
		/// Gets the permissions on boards granted by the token.
		/// </summary>
		ITokenPermission BoardPermissions { get; }

		/// <summary>
		/// Gets the creation date of the token.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets the date and time the token was created.
		/// </summary>
		DateTime? DateCreated { get; }

		/// <summary>
		/// Gets the date and time the token expires, if any.
		/// </summary>
		DateTime? DateExpires { get; }

		/// <summary>
		/// Gets the member for which the token was issued.
		/// </summary>
		IMember Member { get; }

		/// <summary>
		/// Gets the permissions on members granted by the token.
		/// </summary>
		ITokenPermission MemberPermissions { get; }

		/// <summary>
		/// Gets the permissions on organizations granted by the token.
		/// </summary>
		ITokenPermission OrganizationPermissions { get; }

		/// <summary>
		/// Deletes the token.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the token from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		void Delete();

		/// <summary>
		/// Marks the token to be refreshed the next time data is accessed.
		/// </summary>
		void Refresh();
	}

	/// <summary>
	/// Represents a user token.
	/// </summary>
	public class Token : IToken
	{
		/// <summary>
		/// Defines fetchable fields for <see cref="Token"/>s.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates that <see cref="Token.Id"/> should be fetched.
			/// </summary>
			[Display(Description="identifier")]
			Id,
			/// <summary>
			/// Indicates that <see cref="Token.Member"/> should be fetched.
			/// </summary>
			[Display(Description="idMember")]
			Member,
			/// <summary>
			/// Indicates that <see cref="Token.DateCreated"/> should be fetched.
			/// </summary>
			[Display(Description="dateCreated")]
			DateCreated,
			/// <summary>
			/// Indicates that <see cref="Token.DateExpires"/> should be fetched.
			/// </summary>
			[Display(Description="dateExpires")]
			DateExpires,
			/// <summary>
			/// Indicates that <see cref="Token.BoardPermissions"/>, <see cref="Token.MemberPermissions"/>, and <see cref="Token.OrganizationPermissions"/> should be fetched.
			/// </summary>
			[Display(Description="permissions")]
			Permissions
		}

		private readonly Field<string> _appName;
		private readonly Field<DateTime?> _dateCreated;
		private readonly Field<DateTime?> _dateExpires;
		private readonly Field<Member> _member;
		private readonly TokenContext _context;

		private string _id;
		private DateTime? _creation;

		/// <summary>
		/// Gets and sets the fields to fetch.
		/// </summary>
		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

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
					_context.Synchronize();
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

		/// <summary>
		/// Creates a new instance of the <see cref="Token"/> object.
		/// </summary>
		/// <param name="id">The token's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <remarks>
		/// The supplied ID can be either the full ID or the token itself.
		/// </remarks>
		public Token(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new TokenContext(id, auth);
			_context.Synchronized += Synchronized;

			_appName = new Field<string>(_context, nameof(AppName));
			BoardPermissions = new TokenPermission(_context.BoardPermissions);
			_dateCreated = new Field<DateTime?>(_context, nameof(DateCreated));
			_dateExpires = new Field<DateTime?>(_context, nameof(DateExpires));
			_member = new Field<Member>(_context, nameof(Member));
			MemberPermissions = new TokenPermission(_context.MemberPermissions);
			OrganizationPermissions = new TokenPermission(_context.OrganizationPermissions);

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
		/// <remarks>
		/// This permanently deletes the token from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the token to be refreshed the next time data is accessed.
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
			return AppName;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
		}
	}
}