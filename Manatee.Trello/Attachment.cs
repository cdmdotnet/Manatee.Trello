/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Attachment.cs
	Namespace:		Manatee.Trello
	Class Name:		Attachment
	Purpose:		Represents an attachment to a card.

***************************************************************************************/

using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class Attachment
	{
		private readonly Field<int?> _bytes;
		private readonly Field<DateTime?> _date;
		private readonly string _id;
		private readonly Field<bool?> _isUpload;
		private readonly Field<Member> _member;
		private readonly Field<string> _mimeType;
		private readonly Field<string> _name;
		private readonly ReadOnlyAttachmentPreviewCollection _previews;
		private readonly Field<string> _url;
		private readonly AttachmentContext _context;
		private bool _deleted;

		public int? Bytes { get { return _bytes.Value; } }
		public DateTime? Date { get { return _date.Value; } }
		public string Id { get { return _id; } }
		public bool? IsUpload { get { return _isUpload.Value; } }
		public Member Member { get { return _member.Value; } }
		public string MimeType { get { return _mimeType.Value; } }
		public string Name { get { return _name.Value; } }
		public ReadOnlyAttachmentPreviewCollection Previews { get { return _previews; } }
		public string Url { get { return _url.Value; } }

		internal Attachment(IJsonAttachment json, string ownerId)
		{
			_id = json.Id;
			_context = new AttachmentContext(_id, ownerId);

			_bytes = new Field<int?>(_context, () => Bytes);
			_date = new Field<DateTime?>(_context, () => Date);
			_member = new Field<Member>(_context, () => Member);
			_isUpload = new Field<bool?>(_context, () => IsUpload);
			_mimeType = new Field<string>(_context, () => MimeType);
			_name = new Field<string>(_context, () => Name);
			_previews = new ReadOnlyAttachmentPreviewCollection(_context);
			_url = new Field<string>(_context, () => Url);

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
		}

		public void Delete()
		{
			if (_deleted) return;
			_context.Delete();

			_deleted = true;
			TrelloConfiguration.Cache.Remove(this);
		}
	}
}