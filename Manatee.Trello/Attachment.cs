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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	//{
	//   "id":"51478fa171c9f94a5a0039f2",
	//   "bytes":441087,
	//   "date":"2013-03-18T22:05:21.390Z",
	//   "idMember":"50b693ad6f122b4310000a3c",
	//   "isUpload":true,
	//   "mimeType":null,
	//   "name":"Wjy4CDS.jpg",
	//   "previews":[
	//      {
	//         "width":375,
	//         "height":500,
	//         "url":"https://trello-attachments.s3.amazonaws.com/51478f6469fd3d9341001dae/51478f6ce7d2d11751005681/fa8f48b74f9cdec8d6bed74cfdf1af45/Wjy4CDS.jpg_preview_375x500.png",
	//         "_id":"51478fa671c9f94a5a0039f8"
	//      },
	//      {
	//         "width":276,
	//         "height":160,
	//         "url":"https://trello-attachments.s3.amazonaws.com/51478f6469fd3d9341001dae/51478f6ce7d2d11751005681/589a0fc84f63926c122821149dfda76d/Wjy4CDS.jpg_preview_276x160.png",
	//         "_id":"51478fa671c9f94a5a0039f7"
	//      },
	//      {
	//         "width":70,
	//         "height":50,
	//         "url":"https://trello-attachments.s3.amazonaws.com/51478f6469fd3d9341001dae/51478f6ce7d2d11751005681/138b04fea3f6261d53834f7501b84ad2/Wjy4CDS.jpg_preview_70x50.png",
	//         "_id":"51478fa671c9f94a5a0039f6"
	//      },
	//      {
	//         "width":750,
	//         "height":1000,
	//         "url":"https://trello-attachments.s3.amazonaws.com/51478f6469fd3d9341001dae/51478f6ce7d2d11751005681/fd4e0d45321311da2a7784e68ee0d1b9/Wjy4CDS.jpg_preview_750x1000.png",
	//         "_id":"51478fa671c9f94a5a0039f5"
	//      },
	//      {
	//         "width":552,
	//         "height":320,
	//         "url":"https://trello-attachments.s3.amazonaws.com/51478f6469fd3d9341001dae/51478f6ce7d2d11751005681/f3681a4571e9c7e08605662af4c80982/Wjy4CDS.jpg_preview_552x320.png",
	//         "_id":"51478fa671c9f94a5a0039f4"
	//      },
	//      {
	//         "width":140,
	//         "height":100,
	//         "url":"https://trello-attachments.s3.amazonaws.com/51478f6469fd3d9341001dae/51478f6ce7d2d11751005681/48d4f5113f38b9d86038553b93edeabb/Wjy4CDS.jpg_preview_140x100.png",
	//         "_id":"51478fa671c9f94a5a0039f3"
	//      }
	//   ],
	//   "url":"https://trello-attachments.s3.amazonaws.com/51478f6469fd3d9341001dae/51478f6ce7d2d11751005681/088deb599a9a7b8ced99290d5c2cde90/Wjy4CDS.jpg"
	//}
	///<summary>
	/// Represents an attachment to a Card.
	///</summary>
	public class Attachment : ExpiringObject, IEquatable<Attachment>
	{
		private IJsonAttachment _jsonAttachment;
		private Member _member;
		private List<AttachmentPreview> _previews;

		///<summary>
		/// The size of the attachment.
		///</summary>
		public int? Bytes { get { return (_jsonAttachment == null) ? null : _jsonAttachment.Bytes; } }
		/// <summary>
		/// The date on which the attachment was created.
		/// </summary>
		public DateTime? Date { get { return (_jsonAttachment == null) ? null : _jsonAttachment.Date; } }
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonAttachment != null ? _jsonAttachment.Id : base.Id; }
			internal set
			{
				if (_jsonAttachment != null)
					_jsonAttachment.Id = value;
				base.Id = value;
			}
		}
		///<summary>
		/// ?
		///</summary>
		public bool? IsUpload { get { return (_jsonAttachment == null) ? null : _jsonAttachment.IsUpload; } }
		///<summary>
		/// The member who created the attachment.
		///</summary>
		public Member Member
		{
			get
			{
				if (_jsonAttachment == null) return null;
				return ((_member == null) || (_member.Id != _jsonAttachment.IdMember)) && (Svc != null)
				       	? (_member = Svc.Retrieve<Member>(_jsonAttachment.IdMember))
				       	: _member;
			}
		}
		///<summary>
		/// Indicates the type of attachment.
		///</summary>
		public string MimeType { get { return (_jsonAttachment == null) ? null : _jsonAttachment.MimeType; } }
		///<summary>
		/// The name of the attachment.
		///</summary>
		public string Name { get { return (_jsonAttachment == null) ? null : _jsonAttachment.Name; } }
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
		public string Url { get { return (_jsonAttachment == null) ? null : _jsonAttachment.Url; } }

		internal override string Key { get { return "attachments"; } }

		/// <summary>
		/// Deletes this attachment.
		/// </summary>
		public void Delete()
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonAttachment>(request);
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

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			ApplyJson(Api.Get<IJsonAttachment>(request));
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			if (_member != null) _member.Svc = Svc;
		}

		internal override void ApplyJson(object obj)
		{
			_jsonAttachment = (IJsonAttachment) obj;
			_previews = _jsonAttachment.Previews != null
			            	? _jsonAttachment.Previews.Select(p => new AttachmentPreview(p)).ToList()
			            	: null;
		}
	}
}
