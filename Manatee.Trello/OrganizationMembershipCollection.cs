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
 
	File Name:		BoardMembershipCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardMembershipCollection
	Purpose:		Represents a collection of board memberships.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyOrganizationMembershipCollection : ReadOnlyCollection<OrganizationMembership>
	{
		internal ReadOnlyOrganizationMembershipCollection(string ownerId)
			: base(ownerId) {}

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Memberships, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonOrganizationMembership>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => TrelloConfiguration.Cache.Find<OrganizationMembership>(c => c.Id == jc.Id) ?? new OrganizationMembership(jc, OwnerId)));
		}
	}

	public class OrganizationMembershipCollection : ReadOnlyOrganizationMembershipCollection
	{
		internal OrganizationMembershipCollection(string ownerId)
			: base(ownerId) {}

		public void Add(Member member, OrganizationMembershipType membership)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonOrganizationMembership>();
			json.Member = member.Json;
			json.MemberType = membership.ConvertEnum();

			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_AddOrUpdateMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
		}
		public void Remove(Member member)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			json.Value = member.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_RemoveMember, new Dictionary<string, object> {{"_id", OwnerId}, {"_memberId", member.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
		}
	}
}