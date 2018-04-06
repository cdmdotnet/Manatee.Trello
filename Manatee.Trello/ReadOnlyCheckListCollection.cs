using System.Collections.Generic;
using System.Linq;
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
		internal ReadOnlyCheckListCollection(Card card, TrelloAuthorization auth)
			: base(() => card.Id, auth) {}

		/// <summary>
		/// Retrieves a check list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list, or null if none found.</returns>
		/// <remarks>
		/// Matches on <see cref="ICheckList.Id"/> and <see cref="ICheckList.Name"/>.  Comparison is case-sensitive.
		/// </remarks>
		public ICheckList this[string key] => GetByKey(key);

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_CheckLists, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonCheckList>>(Auth, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc =>
				{
					var checkList = CachingObjectFactory.GetFromCache<CheckList>(jc, Auth);
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