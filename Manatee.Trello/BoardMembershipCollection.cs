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
 
	File Name:		BoardMembershipCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyBoardMembershipCollection, BoardMembershipCollection
	Purpose:		Collection objects for board memberships.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of board memberships.
	/// </summary>
	public class ReadOnlyBoardMembershipCollection : ReadOnlyCollection<BoardMembership>
	{
		private Dictionary<string, object> _additionalParameters;

		internal ReadOnlyBoardMembershipCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_additionalParameters = new Dictionary<string, object> {{"fields", "all"}};
		}
		internal ReadOnlyBoardMembershipCollection(ReadOnlyBoardMembershipCollection source, TrelloAuthorization auth)
			: base(() => source.OwnerId, auth)
		{
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Retrieves a membership which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching membership, or null if none found.</returns>
		/// <remarks>
		/// Matches on BoardMembership.Id, BoardMembership.Member.Id,
		/// BoardMembership.Member.Name, and BoardMembership.Usernamee.
		/// Comparison is case-sensitive.
		/// </remarks>
		public BoardMembership this[string key] => GetByKey(key);

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Memberships, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonBoardMembership>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jbm =>
				{
					var membership = TrelloConfiguration.Cache.Find<BoardMembership>(c => c.Id == jbm.Id) ?? new BoardMembership(jbm, OwnerId, Auth);
					membership.Json = jbm;
					return membership;
				}));
		}

		private BoardMembership GetByKey(string key)
		{
			return this.FirstOrDefault(bm => key.In(bm.Id, bm.Member.Id, bm.Member.FullName, bm.Member.UserName));
		}

		internal void AddFilter(IEnumerable<MembershipFilter> actionTypes)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object> {{"filter", string.Empty}};
			var filter = ((string) _additionalParameters["filter"]);
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += actionTypes.Select(a => a.GetDescription()).Join(",");
			_additionalParameters["filter"] = filter;
		}
	}

	/// <summary>
	/// A collection of board memberships.
	/// </summary>
	public class BoardMembershipCollection : ReadOnlyBoardMembershipCollection
	{
		internal BoardMembershipCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) { }

		/// <summary>
		/// Adds a member to a board with specified privileges.
		/// </summary>
		/// <param name="member">The member to add.</param>
		/// <param name="membership">The membership type.</param>
		public BoardMembership Add(Member member, BoardMembershipType membership)
		{
			var error = NotNullRule<Member>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<Member>(member, new[] { error });

			var json = TrelloConfiguration.JsonFactory.Create<IJsonBoardMembership>();
			json.Member = member.Json;
			json.MemberType = membership;

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_AddOrUpdateMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);

			return new BoardMembership(newData, OwnerId, Auth);
		}
		/// <summary>
		/// Removes a member from a board.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		public void Remove(Member member)
		{
			var error = NotNullRule<Member>.Instance.Validate(null, member);
			if (error != null)
				throw new ValidationException<Member>(member, new[] { error });

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_RemoveMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			JsonRepository.Execute(Auth, endpoint);
		}
	}
}