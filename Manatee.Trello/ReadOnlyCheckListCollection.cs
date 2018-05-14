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
	/// A read-only collection of checklists.
	/// </summary>
	public class ReadOnlyCheckListCollection : ReadOnlyCollection<ICheckList>, IReadOnlyCheckListCollection
	{
		internal ReadOnlyCheckListCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}

		/// <summary>
		/// Retrieves a check list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list, or null if none found.</returns>
		/// <remarks>
		/// Matches on checklist ID and name.  Comparison is case-sensitive.
		/// </remarks>
		public ICheckList this[string key] => GetByKey(key);

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_CheckLists, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonCheckList>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jc =>
				{
					var checkList = jc.GetFromCache<CheckList, IJsonCheckList>(Auth);
					checkList.Json = jc;
					return checkList;
				}));
		}

		private ICheckList GetByKey(string key)
		{
			return this.FirstOrDefault(cl => key.In(cl.Id, cl.Name));
		}
	}
}