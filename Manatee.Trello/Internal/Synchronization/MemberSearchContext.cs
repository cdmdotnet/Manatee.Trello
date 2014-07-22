using System.Collections.Generic;
using System.Linq;
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
						"Board", new Property<IJsonMemberSearch, Board>(d => d.Board == null ? null : d.Board.GetFromCache<Board>(),
						                                                (d, o) => { if (o != null) d.Board = o.Json; })
					},
					{"Limit", new Property<IJsonMemberSearch, int?>(d => d.Limit, (d, o) => d.Limit = o)},
					{
						"Results", new Property<IJsonMemberSearch, IEnumerable<MemberSearchResult>>(d => d.Members == null
							                                                                                 ? Enumerable.Empty<MemberSearchResult>()
							                                                                                 : d.Members.Select(GetResult).ToList(),
						                                                                            (d, o) => d.Members = o == null ? null : o.Select(a => a.Member.Json).ToList())
					},
					{
						"Organization", new Property<IJsonMemberSearch, Organization>(d => d.Organization == null
							                                                                   ? null
							                                                                   : d.Organization.GetFromCache<Organization>(),
						                                                              (d, o) => d.Organization = o != null ? o.Json : null)
					},
					{"Query", new Property<IJsonMemberSearch, string>(d => d.Query, (d, o) => { if (!o.IsNullOrWhiteSpace()) d.Query = o; })},
					{"RestrictToOrganization", new Property<IJsonMemberSearch, bool?>(d => d.OnlyOrgMembers, (d, o) => d.OnlyOrgMembers = o)},
				};
		}

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
			var endpoint = EndpointFactory.Build(EntityRequestType.Service_Read_SearchMembers);
			var newData = JsonRepository.Execute<IJsonMemberSearch>(TrelloAuthorization.Default, endpoint, parameters);

			return newData;
		}

		private static MemberSearchResult GetResult(IJsonMember json)
		{
			return new MemberSearchResult(json.GetFromCache<Member>(), json.Similarity);
		}
	}
}