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
	/// A read-only collection of board memberships.
	/// </summary>
	public class ReadOnlyBoardMembershipCollection : ReadOnlyCollection<IBoardMembership>, IReadOnlyBoardMembershipCollection
	{
		internal ReadOnlyBoardMembershipCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		/// <summary>
		/// Retrieves a membership which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching membership, or null if none found.</returns>
		/// <remarks>
		/// Matches on membership ID, member ID, BoardMembership. member full name, and member username. Comparison is case-sensitive.
		/// </remarks>
		public IBoardMembership this[string key] => GetByKey(key);

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Memberships, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonBoardMembership>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jbm =>
				{
					var membership = TrelloConfiguration.Cache.Find<BoardMembership>(jbm.Id) ?? new BoardMembership(jbm, OwnerId, Auth);
					membership.Json = jbm;
					return membership;
				}));
		}

		private IBoardMembership GetByKey(string key)
		{
			return this.FirstOrDefault(bm => key.In(bm.Id, bm.Member.Id, bm.Member.FullName, bm.Member.UserName));
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="membership">The filter value.</param>
		public void Filter(MembershipFilter membership)
		{
			var memberships = membership.GetFlags().Cast<MembershipFilter>();
			Filter(memberships);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="memberships">The filter values.</param>
		public void Filter(IEnumerable<MembershipFilter> memberships)
		{
			var filter = (string) AdditionalParameters["filter"];
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += memberships.Select(a => a.GetDescription()).Join(",");
			AdditionalParameters["filter"] = filter;
		}
	}
}