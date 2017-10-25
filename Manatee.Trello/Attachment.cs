﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an attachment to a card.
	/// </summary>
	public class Attachment : ICacheable
	{
		[Flags]
		public enum Fields
		{
			[Description("bytes")]
			Bytes = 1,
			[Description("date")]
			Date = 1 << 1,
			[Description("isUpload")]
			IsUpload = 1 << 2,
			[Description("idMember")]
			Member = 1 << 3,
			[Description("mimeType")]
			MimeType = 1 << 4,
			[Description("name")]
			Name = 1 << 5,
			[Description("previews")]
			Previews = 1 << 6,
			[Description("url")]
			Url = 1 << 7
		}

		private readonly Field<int?> _bytes;
		private readonly Field<DateTime?> _date;
		private readonly Field<bool?> _isUpload;
		private readonly Field<Member> _member;
		private readonly Field<string> _mimeType;
		private readonly Field<string> _name;
		private readonly Field<string> _url;
		private readonly AttachmentContext _context;
		private DateTime? _creation;

		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

		/// <summary>
		/// Gets the size of the attachment in bytes.
		/// </summary>
		public int? Bytes => _bytes.Value;
		/// <summary>
		/// Gets the creation date of the attachment.
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
		/// Gets the date and time the attachment was added to a card.
		/// </summary>
		public DateTime? Date => _date.Value;
		/// <summary>
		/// Gets the attachment's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets whether the attachment was uploaded data or attached by URI.
		/// </summary>
		public bool? IsUpload => _isUpload.Value;
		/// <summary>
		/// Gets the <see cref="Member"/> who added the attachment.
		/// </summary>
		public Member Member => _member.Value;
		/// <summary>
		/// Gets the MIME type of the attachment.
		/// </summary>
		public string MimeType => _mimeType.Value;
		/// <summary>
		/// Gets the name of the attachment.
		/// </summary>
		public string Name => _name.Value;
		/// <summary>
		/// Gets the collection of previews generated by Trello.
		/// </summary>
		public ReadOnlyAttachmentPreviewCollection Previews { get; }
		/// <summary>
		/// Gets the URI of the attachment.
		/// </summary>
		public string Url => _url.Value;

		internal IJsonAttachment Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

#if IOS
		private Action<Attachment, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		public event Action<Attachment, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		public event Action<Attachment, IEnumerable<string>> Updated;
#endif

		internal Attachment(IJsonAttachment json, string ownerId, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new AttachmentContext(Id, ownerId, auth);
			_context.Synchronized += Synchronized;

			_bytes = new Field<int?>(_context, nameof(Bytes));
			_date = new Field<DateTime?>(_context, nameof(Date));
			_member = new Field<Member>(_context, nameof(Member));
			_isUpload = new Field<bool?>(_context, nameof(IsUpload));
			_mimeType = new Field<string>(_context, nameof(MimeType));
			_name = new Field<string>(_context, nameof(Name));
			Previews = new ReadOnlyAttachmentPreviewCollection(_context, auth);
			_url = new Field<string>(_context, nameof(Url));

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
		}

		/// <summary>
		/// Deletes the attachment.
		/// </summary>
		/// <remarks>
		/// This cannot be undone.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
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
			return Name;
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
	}
}