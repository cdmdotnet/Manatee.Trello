/***************************************************************************************

	Copyright 2013 Little Crab Solutions

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
	Purpose:		Represents an attachment to a card on Trello.com.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	public class Attachment : JsonCompatibleExpiringObject, IEquatable<Attachment>
	{
		private int? _bytes;
		private DateTime? _date;
		private string _memberId;
		private Member _member;
		private bool? _isUpload;
		private string _mimeType;
		private string _name;
		private List<AttachmentPreview> _previews;
		private string _url;

		public string Id { get; private set; }
		public int? Bytes
		{
			get
			{
				VerifyNotExpired();
				return _bytes;
			}
		}
		public DateTime? Date
		{
			get
			{
				VerifyNotExpired();
				return _date;
			}
		}
		public Member Member
		{
			get
			{
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberId)) && (Svc != null) ? (_member = Svc.Retrieve<Member>(_memberId)) : _member;
			}
		}
		public bool? IsUpload
		{
			get
			{
				VerifyNotExpired();
				return _isUpload;
			}
		}
		public string MimeType
		{
			get
			{
				VerifyNotExpired();
				return _mimeType;
			}
		}
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
		}
		public IEnumerable<AttachmentPreview> Previews
		{
			get
			{
				VerifyNotExpired();
				return _previews;
			}
		}
		public string Url
		{
			get
			{
				VerifyNotExpired();
				return _url;
			}
		}

		public Attachment() {}
		public Attachment(TrelloService svc, Card owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_bytes = (int?) obj.TryGetNumber("bytes");
			var date = obj.TryGetString("date");
			_date = string.IsNullOrWhiteSpace(date) ? (DateTime?) null : DateTime.Parse(date);
			_memberId = obj.TryGetString("idMember");
			_isUpload = obj.TryGetBoolean("isUpload");
			_mimeType = obj.TryGetString("mimeType");
			_name = obj.TryGetString("name");
			_previews = obj.TryGetArray("previews").FromJson<AttachmentPreview>();
			_url = obj.TryGetString("url");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"bytes", _bytes.HasValue ? _bytes.Value : JsonValue.Null},
			           		{"date", _date.HasValue ? _date.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			           		{"idMember", _memberId},
			           		{"isUpload", _isUpload.HasValue ? _isUpload.Value : JsonValue.Null},
			           		{"mimeType", _mimeType},
			           		{"name", _name},
			           		{"previews", _previews.ToJson()},
			           		{"url", _url},
			           	};
			return json;
		}
		public  bool Equals(Attachment other)
		{
			return Id == other.Id;
		}

		internal override void Refresh(ExpiringObject entity) {}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Get() {}
		protected override void PropigateSerivce()
		{
			if (_member != null) _member.Svc = Svc;
		}
	}
}
