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
 
	File Name:		CollectionExtensions.cs
	Namespace:		Manatee.Trello.Extensions
	Class Name:		CollectionExtensions
	Purpose:		Extension methods for various collection types.

***************************************************************************************/

using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// Extension methods for various collection types.
	/// </summary>
	public static class CollectionExtensions
	{
		/// <summary>
		/// Filters a <see cref="ReadOnlyActionCollection"/> for a given <see cref="ActionType"/>.
		/// </summary>
		/// <param name="actions">The <see cref="ReadOnlyActionCollection"/></param>
		/// <param name="filter">The new <see cref="ActionType"/> by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyActionCollection Filter(this ReadOnlyActionCollection actions, ActionType filter)
		{
			var collection = new ReadOnlyActionCollection(actions);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyActionCollection"/> for a given <see cref="ActionType"/>s.
		/// </summary>
		/// <param name="actions">The <see cref="ReadOnlyActionCollection"/></param>
		/// <param name="filters">The new <see cref="ActionType"/>s by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyActionCollection Filter(this ReadOnlyActionCollection actions, IEnumerable<ActionType> filters)
		{
			var collection = new ReadOnlyActionCollection(actions);
			collection.AddFilter(filters);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyBoardCollection"/> for a given <see cref="BoardFilter"/>.
		/// </summary>
		/// <param name="boards">The <see cref="ReadOnlyBoardCollection"/></param>
		/// <param name="filter">The new <see cref="BoardFilter"/> by which to filter.</param>
		/// <returns></returns>
		public static ReadOnlyBoardCollection Filter(this ReadOnlyBoardCollection boards, BoardFilter filter)
		{
			var collection = new ReadOnlyBoardCollection(boards);
			collection.SetFilter(filter);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyBoardMembershipCollection"/> for a given <see cref="MembershipFilter"/>.
		/// </summary>
		/// <param name="memberships">The <see cref="ReadOnlyBoardMembershipCollection"/></param>
		/// <param name="filter">The new <see cref="MembershipFilter"/> by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyBoardMembershipCollection Filter(this ReadOnlyBoardMembershipCollection memberships, MembershipFilter filter)
		{
			var collection = new ReadOnlyBoardMembershipCollection(memberships);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyBoardMembershipCollection"/> for a given <see cref="MembershipFilter"/>s.
		/// </summary>
		/// <param name="memberships">The <see cref="ReadOnlyBoardMembershipCollection"/></param>
		/// <param name="filters">The new <see cref="MembershipFilter"/>s by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyBoardMembershipCollection Filter(this ReadOnlyBoardMembershipCollection memberships, IEnumerable<MembershipFilter> filters)
		{
			var collection = new ReadOnlyBoardMembershipCollection(memberships);
			collection.AddFilter(filters);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyCardCollection"/> for a given <see cref="CardFilter"/>.
		/// </summary>
		/// <param name="cards">The <see cref="ReadOnlyCardCollection"/></param>
		/// <param name="filter">The new <see cref="CardFilter"/> by which to filter.</param>
		/// <returns></returns>
		public static ReadOnlyCardCollection Filter(this ReadOnlyCardCollection cards, CardFilter filter)
		{
			var collection = new ReadOnlyCardCollection(cards);
			collection.SetFilter(filter);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyListCollection"/> for a given <see cref="ListFilter"/>.
		/// </summary>
		/// <param name="lists">The <see cref="ReadOnlyListCollection"/></param>
		/// <param name="filter">The new <see cref="ListFilter"/> by which to filter.</param>
		/// <returns></returns>
		public static ReadOnlyListCollection Filter(this ReadOnlyListCollection lists, ListFilter filter)
		{
			var collection = new ReadOnlyListCollection(lists);
			collection.SetFilter(filter);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyMemberCollection"/> for a given <see cref="MemberFilter"/>.
		/// </summary>
		/// <param name="members">The <see cref="ReadOnlyMemberCollection"/></param>
		/// <param name="filter">The new <see cref="MemberFilter"/> by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyMemberCollection Filter(this ReadOnlyMemberCollection members, MemberFilter filter)
		{
			var collection = new ReadOnlyMemberCollection(members);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyMemberCollection"/> for a given <see cref="MemberFilter"/>s.
		/// </summary>
		/// <param name="members">The <see cref="ReadOnlyMemberCollection"/></param>
		/// <param name="filters">The new <see cref="MemberFilter"/>s by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyMemberCollection Filter(this ReadOnlyMemberCollection members, IEnumerable<MemberFilter> filters)
		{
			var collection = new ReadOnlyMemberCollection(members);
			collection.AddFilter(filters);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyNotificationCollection"/> for a given <see cref="NotificationType"/>.
		/// </summary>
		/// <param name="notifications">The <see cref="ReadOnlyNotificationCollection"/></param>
		/// <param name="filter">The new <see cref="NotificationType"/> by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyNotificationCollection Filter(this ReadOnlyNotificationCollection notifications, NotificationType filter)
		{
			var collection = new ReadOnlyNotificationCollection(notifications);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyNotificationCollection"/> for a given <see cref="NotificationType"/>s.
		/// </summary>
		/// <param name="notifications">The <see cref="ReadOnlyNotificationCollection"/></param>
		/// <param name="filters">The new <see cref="NotificationType"/>s by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyNotificationCollection Filter(this ReadOnlyNotificationCollection notifications, IEnumerable<NotificationType> filters)
		{
			var collection = new ReadOnlyNotificationCollection(notifications);
			collection.AddFilter(filters);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyOrganizationCollection"/> for a given <see cref="BoardFilter"/>.
		/// </summary>
		/// <param name="orgs">The <see cref="ReadOnlyOrganizationCollection"/></param>
		/// <param name="filter">The new <see cref="BoardFilter"/> by which to filter.</param>
		/// <returns></returns>
		public static ReadOnlyOrganizationCollection Filter(this ReadOnlyOrganizationCollection orgs, OrganizationFilter filter)
		{
			var collection = new ReadOnlyOrganizationCollection(orgs);
			collection.SetFilter(filter);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyOrganizationMembershipCollection"/> for a given <see cref="MembershipFilter"/>.
		/// </summary>
		/// <param name="memberships">The <see cref="ReadOnlyOrganizationMembershipCollection"/></param>
		/// <param name="filter">The new <see cref="MembershipFilter"/> by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyOrganizationMembershipCollection Filter(this ReadOnlyOrganizationMembershipCollection memberships, MembershipFilter filter)
		{
			var collection = new ReadOnlyOrganizationMembershipCollection(memberships);
			collection.AddFilter(new[] { filter });
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyOrganizationMembershipCollection"/> for a given <see cref="MembershipFilter"/>s.
		/// </summary>
		/// <param name="memberships">The <see cref="ReadOnlyOrganizationMembershipCollection"/></param>
		/// <param name="filters">The new <see cref="MembershipFilter"/>s by which to filter.</param>
		/// <returns></returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyOrganizationMembershipCollection Filter(this ReadOnlyOrganizationMembershipCollection memberships, IEnumerable<MembershipFilter> filters)
		{
			var collection = new ReadOnlyOrganizationMembershipCollection(memberships);
			collection.AddFilter(filters);
			return collection;
		}
	}
}
