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
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	///<summary>
	/// Represents an attachment to a Card.
	///</summary>
	public class Attachment : ExpiringObject, IEquatable<Attachment>, IComparable<Attachment>
	{
		private IJsonAttachment _jsonAttachment;
		private Member _member;
		private List<AttachmentPreview> _previews;
		private bool _isDeleted;

		///<summary>
		/// The size of the attachment.
		///</summary>
		public int? Bytes { get { return _isDeleted ? null : _jsonAttachment.Bytes; } }
		/// <summary>
		/// The date on which the attachment was created.
		/// </summary>
		public DateTime? Date { get { return _isDeleted ? null : _jsonAttachment.Date; } }
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonAttachment.Id; }
			internal set { _jsonAttachment.Id = value; }
		}
		///<summary>
		/// ?
		///</summary>
		public bool? IsUpload { get { return _isDeleted ? null : _jsonAttachment.IsUpload; } }
		///<summary>
		/// The member who created the attachment.
		///</summary>
		public Member Member
		{
			get
			{
				if (_isDeleted) return null;
				return UpdateById(ref _member, EntityRequestType.Member_Read_Refresh, _jsonAttachment.IdMember);
			}
		}
		///<summary>
		/// Indicates the type of attachment.
		///</summary>
		public string MimeType { get { return _isDeleted ? null : _jsonAttachment.MimeType; } }
		///<summary>
		/// The name of the attachment.
		///</summary>
		public string Name { get { return _isDeleted ? null : _jsonAttachment.Name; } }
		///<summary>
		/// Enumerates a collection of previews for the attachment.
		///</summary>
		public IEnumerable<AttachmentPreview> Previews
		{
			get
			{
				if (_isDeleted) return Enumerable.Empty<AttachmentPreview>();
				return _previews ?? Enumerable.Empty<AttachmentPreview>();
			}
		}
		///<summary>
		/// Indicates the attachment storage location.
		///</summary>
		public string Url { get { return _isDeleted ? null : _jsonAttachment.Url; } }
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return false; } }

		/// <summary>
		/// Creates a new instance of the Attachment class.
		/// </summary>
		public Attachment()
		{
			_jsonAttachment = new InnerJsonAttachment();
		}

		/// <summary>
		/// Deletes this attachment.
		/// </summary>
		public void Delete()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			Parameters["_cardId"] = Owner.Id;
			EntityRepository.Upload(EntityRequestType.Attachment_Write_Delete, Parameters);
			_isDeleted = true;
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
			return Id.GetHashCode();
		}
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(Attachment other)
		{
			var diff = Date - other.Date;
			return diff.HasValue ? (int)diff.Value.TotalMilliseconds : 0;
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
		public override bool Refresh()
		{
			if (_isDeleted) return false;
			Parameters["_id"] = Id;
			Parameters["_cardId"] = Owner.Id;
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.Attachment_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonAttachment = (IJsonAttachment)obj;
			_previews = _jsonAttachment.Previews != null
			            	? _jsonAttachment.Previews.Select(p => new AttachmentPreview(p)).ToList()
			            	: null;
		}
		internal override bool EqualsJson(object obj)
		{
			var json = obj as IJsonAttachment;
			return (json != null) && (json.Id == _jsonAttachment.Id);

		}
		internal void ForceDeleted(bool deleted)
		{
			_isDeleted = deleted;
		}
	}
}
