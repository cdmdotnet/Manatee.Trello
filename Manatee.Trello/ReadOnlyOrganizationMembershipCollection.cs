using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of organization memberships.
	/// </summary>
	public class ReadOnlyOrganizationMembershipCollection : ReadOnlyCollection<IOrganizationMembership>, IReadOnlyOrganizationMembershipCollection
	{
		internal ReadOnlyOrganizationMembershipCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			AdditionalParameters["fields"] = "all";
		}

		/// <summary>
		/// Retrieves a membership which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on membership ID, member ID, member full name, and member username. Comparison is case-sensitive.
		/// </remarks>
		public IOrganizationMembership this[string key] => GetByKey(key);

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(MembershipFilter filter)
		{
			var filters = filter.GetFlags().Cast<MembershipFilter>();
			Filter(filters);
		}

		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		public void Filter(IEnumerable<MembershipFilter> filters)
		{
			var filter = (string)AdditionalParameters["filter"];
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += filters.Select(a => a.GetDescription()).Join(",");
			AdditionalParameters["filter"] = filter;
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Read_Memberships, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonOrganizationMembership>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jom =>
				{
					var membership = TrelloConfiguration.Cache.Find<OrganizationMembership>(jom.Id) ?? new OrganizationMembership(jom, OwnerId, Auth);
					membership.Json = jom;
					return membership;
				}));
		}

		private IOrganizationMembership GetByKey(string key)
		{
			return this.FirstOrDefault(bm => key.In(bm.Id, bm.Member.Id, bm.Member.FullName, bm.Member.UserName));
		}
	}
}