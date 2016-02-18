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
 
	File Name:		CollectionExtensions.cs
	Namespace:		Manatee.Trello.Extensions
	Class Name:		CollectionExtensions
	Purpose:		Extension methods for various collection types.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Manatee.Trello
{
	/// <summary>
	/// Extension methods for various collection types.
	/// </summary>
	public static class CollectionExtensions
	{
		#region Filter

		/// <summary>
		/// Filters a <see cref="ReadOnlyActionCollection"/> for a given <see cref="ActionType"/>.
		/// </summary>
		/// <param name="actions">The <see cref="ReadOnlyActionCollection"/></param>
		/// <param name="filter">The new <see cref="ActionType"/> by which to filter.  Can be combined using the bitwise OR operator.</param>
		/// <returns>The filtered collection.</returns>
		/// <exception cref="ArgumentException">Thrown when <see cref="ActionType.Unknown"/> is included in the filter.</exception>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyActionCollection Filter(this ReadOnlyActionCollection actions, ActionType filter)
		{
			if (filter == ActionType.Unknown)
				throw new ArgumentException($"Action type '{ActionType.Unknown}' is not recognized by the Trello API.  Please indicate a different filter.", nameof(filter));

			var collection = new ReadOnlyActionCollection(actions, actions.Auth);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyActionCollection"/> for a given <see cref="ActionType"/>s.
		/// </summary>
		/// <param name="actions">The <see cref="ReadOnlyActionCollection"/></param>
		/// <param name="filters">The new <see cref="ActionType"/>s by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		/// <exception cref="ArgumentException">Thrown when <see cref="ActionType.Unknown"/> is included in the filter.</exception>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyActionCollection Filter(this ReadOnlyActionCollection actions, IEnumerable<ActionType> filters)
		{
			if (filters.Any(f => f == ActionType.Unknown))
				throw new ArgumentException($"Action type '{ActionType.Unknown}' is not recognized by the Trello API.  Please remove it from the filters.", nameof(filters));

			var collection = new ReadOnlyActionCollection(actions, actions.Auth);
			collection.AddFilter(filters);
			return collection;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="actions">The <see cref="ReadOnlyActionCollection"/></param>
		/// <param name="start">The desired start date.</param>
		/// <param name="end">The desired end date.</param>
		/// <exception cref="ArgumentException">Thrown when <see cref="ActionType.Unknown"/> is included in the filter.</exception>
		/// <returns>The filtered collection.</returns>
		public static ReadOnlyActionCollection Filter(this ReadOnlyActionCollection actions, DateTime? start = null, DateTime? end = null)
		{
			if (!start.HasValue && !end.HasValue)
				throw new ArgumentException("Must pass a start and/or end date.");

			var collection = new ReadOnlyActionCollection(actions, actions.Auth);
			collection.AddFilter(start, end);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyBoardCollection"/> for a given <see cref="BoardFilter"/>.
		/// </summary>
		/// <param name="boards">The <see cref="ReadOnlyBoardCollection"/></param>
		/// <param name="filter">The new <see cref="BoardFilter"/> by which to filter.  Can be combined using the bitwise OR operator.</param>
		/// <returns>The filtered collection.</returns>
		public static ReadOnlyBoardCollection Filter(this ReadOnlyBoardCollection boards, BoardFilter filter)
		{
			var collection = new ReadOnlyBoardCollection(boards, boards.Auth);
			collection.SetFilter(filter);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyBoardMembershipCollection"/> for a given <see cref="MembershipFilter"/>.
		/// </summary>
		/// <param name="memberships">The <see cref="ReadOnlyBoardMembershipCollection"/></param>
		/// <param name="filter">The new <see cref="MembershipFilter"/> by which to filter.  Can be combined using the bitwise OR operator.</param>
		/// <returns>The filtered collection.</returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyBoardMembershipCollection Filter(this ReadOnlyBoardMembershipCollection memberships, MembershipFilter filter)
		{
			var collection = new ReadOnlyBoardMembershipCollection(memberships, memberships.Auth);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyBoardMembershipCollection"/> for a given <see cref="MembershipFilter"/>s.
		/// </summary>
		/// <param name="memberships">The <see cref="ReadOnlyBoardMembershipCollection"/></param>
		/// <param name="filters">The new <see cref="MembershipFilter"/>s by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyBoardMembershipCollection Filter(this ReadOnlyBoardMembershipCollection memberships, IEnumerable<MembershipFilter> filters)
		{
			var collection = new ReadOnlyBoardMembershipCollection(memberships, memberships.Auth);
			collection.AddFilter(filters);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyCardCollection"/> for a given <see cref="CardFilter"/>.
		/// </summary>
		/// <param name="cards">The <see cref="ReadOnlyCardCollection"/></param>
		/// <param name="filter">The new <see cref="CardFilter"/> by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		public static ReadOnlyCardCollection Filter(this ReadOnlyCardCollection cards, CardFilter filter)
		{
			var collection = new ReadOnlyCardCollection(cards, cards.Auth);
			collection.SetFilter(filter);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyListCollection"/> for a given <see cref="ListFilter"/>.
		/// </summary>
		/// <param name="lists">The <see cref="ReadOnlyListCollection"/></param>
		/// <param name="filter">The new <see cref="ListFilter"/> by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		public static ReadOnlyListCollection Filter(this ReadOnlyListCollection lists, ListFilter filter)
		{
			var collection = new ReadOnlyListCollection(lists, lists.Auth);
			collection.SetFilter(filter);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyMemberCollection"/> for a given <see cref="MemberFilter"/>.
		/// </summary>
		/// <param name="members">The <see cref="ReadOnlyMemberCollection"/></param>
		/// <param name="filter">The new <see cref="MemberFilter"/> by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyMemberCollection Filter(this ReadOnlyMemberCollection members, MemberFilter filter)
		{
			var collection = new ReadOnlyMemberCollection(members, members.Auth);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyMemberCollection"/> for a given <see cref="MemberFilter"/>s.
		/// </summary>
		/// <param name="members">The <see cref="ReadOnlyMemberCollection"/></param>
		/// <param name="filters">The new <see cref="MemberFilter"/>s by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyMemberCollection Filter(this ReadOnlyMemberCollection members, IEnumerable<MemberFilter> filters)
		{
			var collection = new ReadOnlyMemberCollection(members, members.Auth);
			collection.AddFilter(filters);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyNotificationCollection"/> for a given <see cref="NotificationType"/>.
		/// </summary>
		/// <param name="notifications">The <see cref="ReadOnlyNotificationCollection"/></param>
		/// <param name="filter">The new <see cref="NotificationType"/> by which to filter.  Can be combined using the bitwise OR operator.</param>
		/// <returns>The filtered collection.</returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyNotificationCollection Filter(this ReadOnlyNotificationCollection notifications, NotificationType filter)
		{
			var collection = new ReadOnlyNotificationCollection(notifications, notifications.Auth);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyNotificationCollection"/> for a given <see cref="NotificationType"/>s.
		/// </summary>
		/// <param name="notifications">The <see cref="ReadOnlyNotificationCollection"/></param>
		/// <param name="filters">The new <see cref="NotificationType"/>s by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyNotificationCollection Filter(this ReadOnlyNotificationCollection notifications, IEnumerable<NotificationType> filters)
		{
			var collection = new ReadOnlyNotificationCollection(notifications, notifications.Auth);
			collection.AddFilter(filters);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyOrganizationCollection"/> for a given <see cref="BoardFilter"/>.
		/// </summary>
		/// <param name="orgs">The <see cref="ReadOnlyOrganizationCollection"/></param>
		/// <param name="filter">The new <see cref="BoardFilter"/> by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		public static ReadOnlyOrganizationCollection Filter(this ReadOnlyOrganizationCollection orgs, OrganizationFilter filter)
		{
			var collection = new ReadOnlyOrganizationCollection(orgs, orgs.Auth);
			collection.SetFilter(filter);
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyOrganizationMembershipCollection"/> for a given <see cref="MembershipFilter"/>.
		/// </summary>
		/// <param name="memberships">The <see cref="ReadOnlyOrganizationMembershipCollection"/></param>
		/// <param name="filter">The new <see cref="MembershipFilter"/> by which to filter.  Can be combined using the bitwise OR operator.</param>
		/// <returns>The filtered collection.</returns>
		/// <remarks>The new filter parameter will function as an OR parameter.</remarks>
		public static ReadOnlyOrganizationMembershipCollection Filter(this ReadOnlyOrganizationMembershipCollection memberships, MembershipFilter filter)
		{
			var collection = new ReadOnlyOrganizationMembershipCollection(memberships, memberships.Auth);
			collection.AddFilter(new[] {filter});
			return collection;
		}
		/// <summary>
		/// Filters a <see cref="ReadOnlyOrganizationMembershipCollection"/> for a given <see cref="MembershipFilter"/>s.
		/// </summary>
		/// <param name="memberships">The <see cref="ReadOnlyOrganizationMembershipCollection"/></param>
		/// <param name="filters">The new <see cref="MembershipFilter"/>s by which to filter.</param>
		/// <returns>The filtered collection.</returns>
		/// <remarks>The new filter parameters will function as OR parameters.</remarks>
		public static ReadOnlyOrganizationMembershipCollection Filter(this ReadOnlyOrganizationMembershipCollection memberships, IEnumerable<MembershipFilter> filters)
		{
			var collection = new ReadOnlyOrganizationMembershipCollection(memberships, memberships.Auth);
			collection.AddFilter(filters);
			return collection;
		}

		#endregion

		#region Limit

		/// <summary>
		/// Limits a <see cref="ReadOnlyActionCollection"/> to a specified count of items.
		/// </summary>
		/// <param name="actions">The <see cref="ReadOnlyActionCollection"/></param>
		/// <param name="limit">The limit.</param>
		/// <returns>The limited collection.</returns>
		public static ReadOnlyActionCollection Limit(ReadOnlyActionCollection actions, int limit)
		{
			if (limit <= 0)
				throw new ArgumentException("limit");

			return new ReadOnlyActionCollection(actions, actions.Auth) {Limit = limit};
		}

		/// <summary>
		/// Limits a <see cref="ReadOnlyBoardCollection"/> to a specified count of items.
		/// </summary>
		/// <param name="boards">The <see cref="ReadOnlyBoardCollection"/></param>
		/// <param name="limit">The limit.</param>
		/// <returns>The limited collection.</returns>
		public static ReadOnlyBoardCollection Limit(ReadOnlyBoardCollection boards, int limit)
		{
			if (limit <= 0)
				throw new ArgumentException("limit");

			return new ReadOnlyBoardCollection(boards, boards.Auth) {Limit = limit};
		}

		/// <summary>
		/// Limits a <see cref="ReadOnlyCardCollection"/> to a specified count of items.
		/// </summary>
		/// <param name="cards">The <see cref="ReadOnlyCardCollection"/></param>
		/// <param name="limit">The limit.</param>
		/// <returns>The limited collection.</returns>
		public static ReadOnlyCardCollection Limit(ReadOnlyCardCollection cards, int limit)
		{
			if (limit <= 0)
				throw new ArgumentException("limit");

			return new ReadOnlyCardCollection(cards, cards.Auth) {Limit = limit};
		}

		/// <summary>
		/// Limits a <see cref="ReadOnlyListCollection"/> to a specified count of items.
		/// </summary>
		/// <param name="lists">The <see cref="ReadOnlyListCollection"/></param>
		/// <param name="limit">The limit.</param>
		/// <returns>The limited collection.</returns>
		public static ReadOnlyListCollection Limit(ReadOnlyListCollection lists, int limit)
		{
			if (limit <= 0)
				throw new ArgumentException("limit");

			return new ReadOnlyListCollection(lists, lists.Auth) { Limit = limit };
		}

		/// <summary>
		/// Limits a <see cref="ReadOnlyNotificationCollection"/> to a specified count of items.
		/// </summary>
		/// <param name="notifications">The <see cref="ReadOnlyNotificationCollection"/></param>
		/// <param name="limit">The limit.</param>
		/// <returns>The limited collection.</returns>
		public static ReadOnlyNotificationCollection Limit(ReadOnlyNotificationCollection notifications, int limit)
		{
			if (limit <= 0)
				throw new ArgumentException("limit");

			return new ReadOnlyNotificationCollection(notifications, notifications.Auth) {Limit = limit};
		}

		/// <summary>
		/// Limits a <see cref="ReadOnlyOrganizationCollection"/> to a specified count of items.
		/// </summary>
		/// <param name="organizations">The <see cref="ReadOnlyOrganizationCollection"/></param>
		/// <param name="limit">The limit.</param>
		/// <returns>The limited collection.</returns>
		public static ReadOnlyOrganizationCollection Limit(ReadOnlyOrganizationCollection organizations, int limit)
		{
			if (limit <= 0)
				throw new ArgumentException("limit");

			return new ReadOnlyOrganizationCollection(organizations, organizations.Auth) {Limit = limit};
		}

		#endregion
	}
}
