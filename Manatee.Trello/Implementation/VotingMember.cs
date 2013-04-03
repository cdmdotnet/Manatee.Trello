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
 
	File Name:		VotingMember.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		VotingMember
	Purpose:		Represents a single member who voted for a Card on Trello.com.

***************************************************************************************/
using System;

namespace Manatee.Trello.Implementation
{
	internal class VotingMember : Member, IEquatable<VotingMember>
	{
		internal override string Key { get { return "membersVoted"; } }

		public VotingMember() { }
		internal VotingMember(ITrelloRest svc, string id)
			: base(svc, id) {}

		public bool Equals(VotingMember other)
		{
			return base.Equals(this);
		}
	}
}
