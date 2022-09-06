using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Eventing;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of members.
	/// </summary>
	public class ReadOnlyCollaboratorCollection : ReadOnlyCollection<ICollaborator>,
											IReadOnlyCollaboratorCollection
	{
		/// <summary>
		/// Retrieves a collaborator which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching collaborator, or null if none found.</returns>
		/// <remarks>
		/// Matches on collaborator ID, full name, and username.  Comparison is case-sensitive.
		/// </remarks>
		public ICollaborator this[string key] => GetByKey(key);

		internal ReadOnlyCollaboratorCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(CollaboratorFilter filter)
		{
			var filters = filter.GetFlags().Cast<CollaboratorFilter>();
			Filter(filters);
		}

		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		public void Filter(IEnumerable<CollaboratorFilter> filters)
		{
			var filter = AdditionalParameters.ContainsKey("filter") ? (string) AdditionalParameters["filter"] : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += filters.Select(a => a.GetDescription()).Join(",");
			AdditionalParameters["filter"] = filter;
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var allParameters = new Dictionary<string, object> { { "fields", string.Empty }, { "collaborators", "true" } };
			var endpoint = EndpointFactory.Build(EntityRequestType.OrganizationCollaborator_Read_Refresh, new Dictionary<string, object> { { "_id", OwnerId } });
			var newData = await JsonRepository.Execute<IJsonOrganization>(Auth, endpoint, ct, allParameters);

			Items.Clear();
			Items.AddRange(newData.Collaborators.Select(jo =>
				{
					var member = jo.GetFromCache<Collaborator, IJsonCollaborator>(Auth);
					member.Json = jo;
					return member;
				}));
		}

		private ICollaborator GetByKey(string key)
		{
			return this.FirstOrDefault(m => key.In(m.Id, m.FullName, m.UserName));
		}
	}
}