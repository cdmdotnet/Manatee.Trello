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
 
	File Name:		OrganizationMembershipContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		OrganizationMembershipContext
	Purpose:		Provides a data context for an organization membership.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class OrganizationMembershipContext : SynchronizationContext<IJsonOrganizationMembership>
	{
		private readonly string _ownerId;

		static OrganizationMembershipContext()
		{
			_properties = new Dictionary<string, Property<IJsonOrganizationMembership>>
				{
					{"Id", new Property<IJsonOrganizationMembership, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsUnconfirmed", new Property<IJsonOrganizationMembership, bool?>((d, a) => d.Unconfirmed, (d, o) => d.Unconfirmed = o)},
					{
						"Member", new Property<IJsonOrganizationMembership, Member>((d, a) => d.Member.GetFromCache<Member>(a),
						                                                    (d, o) => d.Member = o?.Json)
					},
					{"MemberType", new Property<IJsonOrganizationMembership, OrganizationMembershipType?>((d, a) => d.MemberType, (d, o) => d.MemberType = o)},
				};
		}
		public OrganizationMembershipContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}
		protected override IJsonOrganizationMembership GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationMembership_Read_Refresh, new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonOrganizationMembership>(Auth, endpoint);

			return newData;
		}
		protected override void SubmitData(IJsonOrganizationMembership json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationMembership_Write_Update, new Dictionary<string, object> {{"_organizationId", _ownerId}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
	}
}