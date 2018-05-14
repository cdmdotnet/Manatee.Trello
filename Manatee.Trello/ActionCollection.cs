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
	/// A read-only collection of actions.
	/// </summary>
	public class ReadOnlyActionCollection : ReadOnlyCollection<IAction>, IReadOnlyActionCollection
	{
		private static readonly Dictionary<Type, EntityRequestType> RequestTypes;
		private readonly EntityRequestType _updateRequestType;

		static ReadOnlyActionCollection()
		{
			RequestTypes = new Dictionary<Type, EntityRequestType>
				{
					{typeof(Board), EntityRequestType.Board_Read_Actions},
					{typeof(Card), EntityRequestType.Card_Read_Actions},
					{typeof(List), EntityRequestType.List_Read_Actions},
					{typeof(Member), EntityRequestType.Member_Read_Actions},
					{typeof(Organization), EntityRequestType.Organization_Read_Actions},
				};
		}
		internal ReadOnlyActionCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = RequestTypes[type];
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="actionType">The action type.</param>
		public void Filter(ActionType actionType)
		{
			var actionTypes = actionType.GetFlags();
			Filter(actionTypes);
		}

		/// <summary>
		/// Adds a number of filters to the collection.
		/// </summary>
		/// <param name="actionTypes">A collection of action types.</param>
		public void Filter(IEnumerable<ActionType> actionTypes)
		{
			var filter = AdditionalParameters.ContainsKey("filter") ? (string)AdditionalParameters["filter"] : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			var actionType = actionTypes.Aggregate(ActionType.Unknown, (c, a) => c | a);
			filter += actionType.ToString();
			AdditionalParameters["filter"] = filter;
		}

		/// <summary>
		/// Adds a date-based filter to the collection.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="end">The end date.</param>
		public void Filter(DateTime? start, DateTime? end)
		{
			if (start.HasValue)
				AdditionalParameters["since"] = start.Value.ToUniversalTime().ToString("O");
			if (end.HasValue)
				AdditionalParameters["before"] = end.Value.ToUniversalTime().ToString("O");
		}

		/// <summary>
		/// Refreshes the collection.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			IncorporateLimit();

			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonAction>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(ja =>
				{
					var action = ja.GetFromCache<Action, IJsonAction>(Auth);
					action.Json = ja;
					return action;
				}));
		}
	}
}