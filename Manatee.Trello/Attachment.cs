using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	public class Attachment : OwnedEntityBase<Card>
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
			set { _bytes = value; }
		}
		public DateTime? Date
		{
			get
			{
				VerifyNotExpired();
				return _date;
			}
			set { _date = value; }
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
			set { _isUpload = value; }
		}
		public string MimeType
		{
			get
			{
				VerifyNotExpired();
				return _mimeType;
			}
			set { _mimeType = value; }
		}
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
			set { _name = value; }
		}
		public List<AttachmentPreview> Previews
		{
			get
			{
				VerifyNotExpired();
				return _previews;
			}
			set { _previews = value; }
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
			_date = string.IsNullOrWhiteSpace(date) ? (DateTime?)null : DateTime.Parse(date);
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
		public override bool Equals(EquatableExpiringObject other)
		{
			var attachment = other as Attachment;
			if (attachment == null) return false;
			return Id == attachment.Id;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var attachment = entity as Attachment;
			if (attachment == null) return;
			_bytes = attachment._bytes;
			_date = attachment._date;
			_memberId = attachment._memberId;
			_isUpload = attachment._isUpload;
			_mimeType = attachment._mimeType;
			_name = attachment._name;
			_previews = attachment._previews;
			_url = attachment._url;
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<Card, Attachment>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			if (_member != null) _member.Svc = Svc;
		}
	}
}
