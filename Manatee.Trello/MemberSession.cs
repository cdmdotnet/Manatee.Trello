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
 
	File Name:		MemberSession.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberSession
	Purpose:		Represents a web session currently active by a member.

***************************************************************************************/
using System;
using System.Net;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	//{
	//   "isCurrent":true,
	//   "isRecent":true,
	//   "id":"51927eb8cbd45eea2300004d",
	//   "dateCreated":"2013-05-14T18:13:12.405Z",
	//   "dateExpires":"2013-06-25T20:57:15.664Z",
	//   "dateLastUsed":"2013-05-29T03:04:23.389Z",
	//   "ipAddress":"71.96.206.6",
	//   "type":"legacy",
	//   "userAgent":"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36"
	//},
	/// <summary>
	/// Represents a web session currently active by a member.
	/// </summary>
	public class MemberSession : ExpiringObject, IEquatable<MemberSession>
	{
		private IJsonMemberSession _jsonMemberSession;
		private IPAddress _ipAddress;

		///<summary>
		/// Gets the date this session was created.
		///</summary>
		public DateTime? DateCreated { get { return (_jsonMemberSession == null) ? null : _jsonMemberSession.DateCreated; } }
		///<summary>
		/// Gets the date this session was created.
		///</summary>
		public DateTime? DateExpires { get { return (_jsonMemberSession == null) ? null : _jsonMemberSession.DateExpires; } }
		///<summary>
		/// Gets the date this session was created.
		///</summary>
		public DateTime? DateLastUsed { get { return (_jsonMemberSession == null) ? null : _jsonMemberSession.DateLastUsed; } }
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonMemberSession != null ? _jsonMemberSession.Id : base.Id; }
			internal set
			{
				if (_jsonMemberSession != null)
					_jsonMemberSession.Id = value;
				base.Id = value;
			}
		}
		/// <summary>
		/// Gets the IP Address associated with the session.
		/// </summary>
		public IPAddress IpAddress
		{
			get
			{
				return (_jsonMemberSession == null)
						? null
						: _ipAddress ?? (_ipAddress = IPAddress.Parse(_jsonMemberSession.IpAddress));
			}
		}
		/// <summary>
		/// Gets whether the session is the currently active session.
		/// </summary>
		public bool? IsCurrent { get { return (_jsonMemberSession == null) ? null : _jsonMemberSession.IsCurrent; } }
		/// <summary>
		/// Gets whether the session has been used recently.
		/// </summary>
		public bool? IsRecent { get { return (_jsonMemberSession == null) ? null : _jsonMemberSession.IsRecent; } }
		/// <summary>
		/// Gets the type of login used to create the session.
		/// </summary>
		public string Type { get { return (_jsonMemberSession == null) ? null : _jsonMemberSession.Type; } }
		/// <summary>
		/// Gets the interface used to create the session.  This is typically browser information.
		/// </summary>
		public string UserAgent { get { return (_jsonMemberSession == null) ? null : _jsonMemberSession.UserAgent; } }

		internal static string TypeKey { get { return "sessions"; } }
		internal static string TypeKey2 { get { return "sessions"; } }
		internal override string Key { get { return TypeKey; } }
		internal override string Key2 { get { return TypeKey2; } }

		/// <summary>
		/// Creates a new instance of the MemberSession class.
		/// </summary>
		public MemberSession()
		{
			_jsonMemberSession = new InnerJsonMemberSession();
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(MemberSession other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Id, Id);
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
			if (!(obj is MemberSession)) return false;
			return Equals((MemberSession)obj);
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
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Refresh() {}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService() {}

		internal override void ApplyJson(object obj)
		{
			if (obj == null)
				throw new EntityNotOnTrelloException<MemberSession>(this);
			_jsonMemberSession = (IJsonMemberSession) obj;
		}
	}
}