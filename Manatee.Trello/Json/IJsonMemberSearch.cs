using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	public interface IJsonMemberSearch
	{
		List<IJsonMember> Members { get; set; }
		IJsonBoard Board { get; set; }
		int? Limit { get; set; }
		bool? OnlyOrgMembers { get; set; }
		IJsonOrganization Organization { get; set; }
		string Query { get; set; }
	}
}