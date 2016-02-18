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
 
	File Name:		MemberSearchContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		MemberSearchContext
	Purpose:		Provides a data context for a member search.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class MemberSearchContext : SynchronizationContext<IJsonMemberSearch>
	{
		static MemberSearchContext()
		{
			_properties = new Dictionary<string, Property<IJsonMemberSearch>>
				{
					{
						"Board", new Property<IJsonMemberSearch, Board>((d, a) => d.Board?.GetFromCache<Board>(a),
						                                                (d, o) => { if (o != null) d.Board = o.Json; })
					},
					{"Limit", new Property<IJsonMemberSearch, int?>((d, a) => d.Limit, (d, o) => d.Limit = o)},
					{
						"Results", new Property<IJsonMemberSearch, IEnumerable<MemberSearchResult>>((d, a) => d.Members?.Select(m => GetResult(m, a)).ToList() ?? Enumerable.Empty<MemberSearchResult>(),
						                                                                            (d, o) => d.Members = o?.Select(a => a.Member.Json).ToList())
					},
					{
						"Organization", new Property<IJsonMemberSearch, Organization>((d, a) => d.Organization?.GetFromCache<Organization>(a),
						                                                              (d, o) => d.Organization = o?.Json)
					},
					{"Query", new Property<IJsonMemberSearch, string>((d, a) => d.Query, (d, o) => { if (!o.IsNullOrWhiteSpace()) d.Query = o; })},
					{"RestrictToOrganization", new Property<IJsonMemberSearch, bool?>((d, a) => d.OnlyOrgMembers, (d, o) => d.OnlyOrgMembers = o)},
				};
		}
		public MemberSearchContext(TrelloAuthorization auth)
			: base(auth) {}

		protected override IJsonMemberSearch GetData()
		{
			// NOTE: Cannot place these parameters in a JSON object because it's a GET operation.
			var parameters = new Dictionary<string, object> {{"query", Data.Query}};
			if (Data.Board != null)
				parameters.Add("idBoard", Data.Board.Id);
			if (Data.Organization != null)
			{
				parameters.Add("idOrganization", Data.Organization.Id);
				parameters.Add("onlyOrgMembers", Data.OnlyOrgMembers);
			}
			if (Data.Limit.HasValue)
				parameters.Add("limit", Data.Limit);
			var endpoint = EndpointFactory.Build(EntityRequestType.Service_Read_SearchMembers);
			var newData = JsonRepository.Execute<IJsonMemberSearch>(Auth, endpoint, parameters);

			return newData;
		}

		private static MemberSearchResult GetResult(IJsonMember json, TrelloAuthorization auth)
		{
			return new MemberSearchResult(json.GetFromCache<Member>(auth), json.Similarity);
		}
	}
}