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
 
	File Name:		MemberExtensions.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberExtensions
	Purpose:		Exposes extension methods for the Member entity.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes extension methods for the Member entity.
	/// </summary>
	public static class MemberExtensions
	{
		/// <summary>
		/// Retrieves all cards assigned to a member, both archived and active.
		/// </summary>
		/// <param name="member">The member</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> AllCards(this Member member)
		{
			if (member.Svc != null)
			{
				var endpoint = EndpointGenerator.Default.Generate(member);
				endpoint.Append(Card.TypeKey);
				var request = member.Svc.RestClientProvider.RequestProvider.Create(endpoint.ToString());
				request.AddParameter("fields", "id");
				request.AddParameter("filter", "all");
				request.AddParameter("actions", "none");
				request.AddParameter("attachments", "false");
				request.AddParameter("badges", "false");
				request.AddParameter("members", "false");
				request.AddParameter("membersVoted", "false");
				request.AddParameter("checkItemStates", "false");
				request.AddParameter("checkLists", "false");
				request.AddParameter("board", "false");
				request.AddParameter("list", "false");
				var response = member.Svc.Api.Get<List<IJsonCard>>(request);
				return response.Select(j => member.Svc.Retrieve<Card>(j.Id));
			}
			return Enumerable.Empty<Card>();
		}
		/// <summary>
		/// Retrieves all active cards assigned to a member.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> ActiveCards(this Member member)
		{
			if (member.Svc != null)
			{
				var endpoint = EndpointGenerator.Default.Generate(member);
				endpoint.Append(Card.TypeKey);
				var request = member.Svc.RestClientProvider.RequestProvider.Create(endpoint.ToString());
				request.AddParameter("fields", "id");
				request.AddParameter("filter", "visible");
				request.AddParameter("actions", "none");
				request.AddParameter("attachments", "false");
				request.AddParameter("badges", "false");
				request.AddParameter("members", "false");
				request.AddParameter("membersVoted", "false");
				request.AddParameter("checkItemStates", "false");
				request.AddParameter("checkLists", "false");
				request.AddParameter("board", "false");
				request.AddParameter("list", "false");
				var response = member.Svc.Api.Get<List<IJsonCard>>(request);
				return response.Select(j => member.Svc.Retrieve<Card>(j.Id));
			}
			return Enumerable.Empty<Card>();
		}
		/// <summary>
		/// Returns only the boards which are owned by a member.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <returns>A collection of boards.</returns>
		public static IEnumerable<Board> PersonalBoards(this Member member)
		{
			return member.Boards.Where(b => b.Organization == null);
		}
		/// <summary>
		/// Retrieves all active cards assigned to a member with due dates within a specified time span of DateTime.Now.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <param name="timeSpan">The time span.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsDueSoon(this Member member, TimeSpan timeSpan)
		{
			return member.ActiveCards().Where(c => DateTime.Now > c.DueDate - timeSpan);
		}
		/// <summary>
		/// Retrieves all active cards assigned to a member with names or descriptions which match a specified Regex.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <param name="regex">The Regex.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description matching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsMatching(this Member member, Regex regex)
		{
			return member.ActiveCards().Where(c => regex.IsMatch(c.Description) || regex.IsMatch(c.Name));
		}
		/// <summary>
		/// Retrieves all active cards assigned to a member with names or descriptions which contain a specified string.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <param name="search">The string.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description searching does not account for Markdown syntax.</remarks>
		[Obsolete("Use TrelloService.Search(... context: [member])")]
		public static IEnumerable<Card> CardsContaining(this Member member, string search)
		{
			return member.ActiveCards().Where(c => c.Description.Contains(search) || c.Name.Contains(search));
		}
	}
}