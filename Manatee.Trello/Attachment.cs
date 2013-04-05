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
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	///<summary>
	/// Represents an attachment to a Card.
	///</summary>
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

		///<summary>
		/// The size of the attachment.
		///</summary>
		public int? Bytes { get { return _bytes; } }
		/// <summary>
		/// The date on which the attachment was created.
		/// </summary>
		public DateTime? Date { get { return _date; } }
		///<summary>
		/// The member who created the attachment
		///</summary>
		public Member Member
		{
			get
			{
				return ((_member == null) || (_member.Id != _memberId)) && (Svc != null)
						? (_member = Svc.Get(Svc.RequestProvider.Create<Member>(_memberId)))
						: _member;
			}
		}
		///<summary>
		///</summary>
		public bool? IsUpload { get { return _isUpload; } }
		///<summary>
		/// Indicates the type of attachment.
		///</summary>
		public string MimeType { get { return _mimeType; } }
		///<summary>
		/// The name of the attachment.
		///</summary>
		public string Name { get { return _name; } }
		///<summary>
		/// Enumerates a collection of previews for the attachment.
		///</summary>
		public IEnumerable<AttachmentPreview> Previews
		{
			get
			{
				VerifyNotExpired();
				return _previews;
			}
		}
		///<summary>
		/// Indicates the attachment storage location.
		///</summary>
		public string Url { get { return _url; } }

		internal override string Key { get { return "attachments"; } }

		/// <summary>
		/// Creates a new instance of an Attachment.
		/// </summary>
		public Attachment() {}
		internal Attachment(ITrelloRest svc, Card owner)
			: base(svc, owner) {}

		/// <summary>
		/// Deletes this attachment.
		/// </summary>
		public void Delete()
		{
			if (Svc == null) return;
			Svc.Delete(Svc.RequestProvider.Create<Attachment>(new ExpiringObject[] {Owner, this}));
		}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
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
			_previews = obj.TryGetArray("previews").FromJson<AttachmentPreview>().ToList();
			_url = obj.TryGetString("url");
			_isInitialized = true;
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			if (!_isInitialized) VerifyNotExpired();
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
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Attachment other)
		{
			return Id == other.Id;
		}
		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!(obj is Attachment)) return false;
			return Equals((Attachment) obj);
		}
		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal override void Refresh(ExpiringObject entity)
		{
			_isInitialized = true;
		}

		/// <summary>
		/// Get(Svc.RequestProvider.Creates updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			Refresh(null);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce()
		{
			if (_member != null) _member.Svc = Svc;
		}
	}
}
