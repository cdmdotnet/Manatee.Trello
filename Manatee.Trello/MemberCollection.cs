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
 
	File Name:		MemberCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyMemberCollection, MemberCollection
	Purpose:		Collection objects for members.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of members.
	/// </summary>
	public class ReadOnlyMemberCollection : ReadOnlyCollection<Member>
	{
		private readonly EntityRequestType _updateRequestType;

		internal ReadOnlyMemberCollection(Type type, string ownerId)
			: base(ownerId)
		{
			if (type == typeof(Board))
				_updateRequestType = EntityRequestType.Board_Read_Members;
			else if (type == typeof(Organization))
				_updateRequestType = EntityRequestType.Organization_Read_Members;
			else
				_updateRequestType = EntityRequestType.Card_Read_Members;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonMember>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => jc.GetFromCache<Member>()));
		}
	}

	/// <summary>
	/// A collection of members.
	/// </summary>
	public class MemberCollection : ReadOnlyMemberCollection
	{
		internal MemberCollection(string ownerId)
			: base(typeof(Card), ownerId) { }

		/// <summary>
		/// Adds a member to the collection.
		/// </summary>
		/// <param name="member">The member to add.</param>
		public void Add(Member member)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.Value = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AssignMember);
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
		}
		/// <summary>
		/// Removes a member from the collection.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		public void Remove(Member member)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.Value = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_RemoveMember);
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
		}
	}
}