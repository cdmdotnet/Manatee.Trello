using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of members.
	/// </summary>
	public class ReadOnlyMemberCollection : ReadOnlyCollection<IMember>, IReadOnlyMemberCollection
	{
		private readonly EntityRequestType _updateRequestType;

		/// <summary>
		/// Retrieves a member which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching member, or null if none found.</returns>
		/// <remarks>
		/// Matches on member ID, full name, and username.  Comparison is case-sensitive.
		/// </remarks>
		public IMember this[string key] => GetByKey(key);

		internal ReadOnlyMemberCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = requestType;
			AdditionalParameters["fields"] = "all";
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(MemberFilter filter)
		{
			var filters = filter.GetFlags().Cast<MemberFilter>();
			Filter(filters);
		}

		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		public void Filter(IEnumerable<MemberFilter> filters)
		{
			var filter = AdditionalParameters.ContainsKey("filter") ? (string)AdditionalParameters["filter"] : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += filters.Select(a => a.GetDescription()).Join(",");
			AdditionalParameters["filter"] = filter;
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonMember>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jm =>
				{
					var member = jm.GetFromCache<Member, IJsonMember>(Auth);
					member.Json = jm;
					return member;
				}));
		}

		private IMember GetByKey(string key)
		{
			return this.FirstOrDefault(m => key.In(m.Id, m.FullName, m.UserName));
		}
	}
}