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
	/// A read-only collection of organizations.
	/// </summary>
	public class ReadOnlyOrganizationCollection : ReadOnlyCollection<IOrganization>, IReadOnlyOrganizationCollection
	{
		private Dictionary<string, object> _additionalParameters;

		internal ReadOnlyOrganizationCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}

		/// <summary>
		/// Retrieves a organization which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching organization, or null if none found.</returns>
		/// <remarks>
		/// Matches on Organization.Id, Organization.Name, and Organization.DisplayName.  Comparison is case-sensitive.
		/// </remarks>
		public IOrganization this[string key] => GetByKey(key);

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(OrganizationFilter filter)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			_additionalParameters["filter"] = filter.GetDescription();
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			IncorporateLimit(_additionalParameters);

			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Organizations, new Dictionary<string, object> { { "_id", OwnerId } });
			var newData = await JsonRepository.Execute<List<IJsonOrganization>>(Auth, endpoint, ct, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jo =>
				{
					var org = jo.GetFromCache<Organization, IJsonOrganization>(Auth);
					org.Json = jo;
					return org;
				}));
		}

		private IOrganization GetByKey(string key)
		{
			return this.FirstOrDefault(o => key.In(o.Id, o.Name, o.DisplayName));
		}
	}
}