/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		MemberCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyMemberCollection, MemberCollection
	Purpose:		Collection objects for members.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of members.
	/// </summary>
	public class ReadOnlyMemberCollection : ReadOnlyCollection<Member>
	{
		private readonly EntityRequestType _updateRequestType;
		private Dictionary<string, object> _additionalParameters; 

		/// <summary>
		/// Retrieves a member which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching member, or null if none found.</returns>
		/// <remarks>
		/// Matches on Member.Id, Member.FullName, and Member.Username.  Comparison is case-sensitive.
		/// </remarks>
		public Member this[string key] => GetByKey(key);

		internal ReadOnlyMemberCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = requestType;
			_additionalParameters = new Dictionary<string, object> {{"fields", "all"}};
		}
		internal ReadOnlyMemberCollection(ReadOnlyMemberCollection source, TrelloAuthorization auth)
			: base(() => source.OwnerId, auth)
		{
			_updateRequestType = source._updateRequestType;
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonMember>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jm =>
				{
					var member = jm.GetFromCache<Member>(Auth);
					member.Json = jm;
					return member;
				}));
		}

		internal void AddFilter(IEnumerable<MemberFilter> actionTypes)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object> {{"filter", string.Empty}};
			var filter = _additionalParameters.ContainsKey("filter") ? (string)_additionalParameters["filter"] : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += actionTypes.Select(a => a.GetDescription()).Join(",");
			_additionalParameters["filter"] = filter;
		}

		private Member GetByKey(string key)
		{
			return this.FirstOrDefault(m => key.In(m.Id, m.FullName, m.UserName));
		}
	}

	/// <summary>
	/// A collection of members.
	/// </summary>
	public class MemberCollection : ReadOnlyMemberCollection
	{
		internal MemberCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(requestType, getOwnerId, auth) {}

		/// <summary>
		/// Adds a member to the collection.
		/// </summary>
		/// <param name="member">The member to add.</param>
		public void Add(Member member)
		{
			var error = NotNullRule<Member>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<Member>(member, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.String = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AssignMember, new Dictionary<string, object> {{"_id", OwnerId}});
			JsonRepository.Execute(Auth, endpoint, json);

			Items.Add(member);
		}
		/// <summary>
		/// Removes a member from the collection.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		public void Remove(Member member)
		{
			var error = NotNullRule<Member>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<Member>(member, new[] {error});

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_RemoveMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			JsonRepository.Execute(Auth, endpoint);

			Items.Remove(member);
		}
	}
}