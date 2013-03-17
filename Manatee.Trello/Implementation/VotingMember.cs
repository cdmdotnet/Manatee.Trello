using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manatee.Trello.Implementation
{
	internal class VotingMember : Member
	{
		public VotingMember() {}
		internal VotingMember(TrelloService svc, string id)
			: base(svc, id) {}
	}
}
