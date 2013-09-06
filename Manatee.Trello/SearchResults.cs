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
 
	File Name:		SearchResults.cs
	Namespace:		Manatee.Trello
	Class Name:		SearchResults
	Purpose:		Contains the results of a text-based search within the current
					member's boards and organizations as well as the parameters
					which yielded the results.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Contains the results of a text-based search within the current member's boards
	/// and organizations as well as the parameters which yielded the results.
	/// </summary>
	public class SearchResults : ExpiringObject
	{
		private IEnumerable<Action> _actions;
		private IEnumerable<Board> _boards;
		private IEnumerable<Card> _cards;
		private IEnumerable<Member> _members;
		private IEnumerable<Organization> _organizations;

		/// <summary>
		/// Enumerates the Actions which match the provided query.
		/// </summary>
		public IEnumerable<Action> Actions { get { return _actions; } }
		/// <summary>
		/// Enumerates the Boards which match the provided query.
		/// </summary>
		public IEnumerable<Board> Boards { get { return _boards; } }
		/// <summary>
		/// Enumerates the Cards which match the provided query.
		/// </summary>
		public IEnumerable<Card> Cards { get { return _cards; } }
		/// <summary>
		/// Enumerates the Members which match the provided query.
		/// </summary>
		public IEnumerable<Member> Members { get { return _members; } }
		/// <summary>
		/// Enumerates the Organizations which match the provided query.
		/// </summary>
		public IEnumerable<Organization> Organizations { get { return _organizations; } }
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return false; } }

		internal SearchResults()
		{
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			return false;
		}

		internal override void ApplyJson(object obj)
		{
			var results = (IJsonSearchResults) obj;
			_actions = results.ActionIds.Select(Download<Action>).ToList();
			_boards = results.BoardIds.Select(Download<Board>).ToList();
			_cards = results.CardIds.Select(Download<Card>).ToList();
			_members = results.MemberIds.Select(Download<Member>).ToList();
			_organizations = results.OrganizationIds.Select(Download<Organization>).ToList();
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}

		private T Download<T>(string id)
			where T : ExpiringObject
		{
			return EntityRepository.Download<T>(EntityRequestType.Action_Read_Refresh,
												new Dictionary<string, object> {{"_id", id}});
		}
	}
}