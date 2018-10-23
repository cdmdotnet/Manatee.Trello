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
	public class ReadOnlyMemberCollection : ReadOnlyCollection<IMember>,
	                                        IReadOnlyMemberCollection,
	                                        IHandle<EntityUpdatedEvent<IJsonMember>>
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

			EventAggregator.Subscribe(this);
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
			var filter = AdditionalParameters.ContainsKey("filter") ? (string) AdditionalParameters["filter"] : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += filters.Select(a => a.GetDescription()).Join(",");
			AdditionalParameters["filter"] = filter;
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var allParameters = AdditionalParameters.Concat(MemberContext.CurrentParameters)
			                                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonMember>>(Auth, endpoint, ct, allParameters);

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

		void IHandle<EntityUpdatedEvent<IJsonMember>>.Handle(EntityUpdatedEvent<IJsonMember> message)
		{
			IMember member;
			switch (_updateRequestType)
			{
				case EntityRequestType.Board_Read_Members:
					if (!message.Properties.Contains(nameof(Board.Members))) return;
					member = Items.FirstOrDefault(b => b.Id == message.Data.Id);
					var boardIds = message.Data.Boards.Select(m => m.Id).ToList();
					if (!boardIds.Contains(OwnerId) && member != null)
						Items.Remove(member);
					else if (boardIds.Contains(OwnerId) && member == null)
						Items.Add(message.Data.GetFromCache<Member>(Auth));
					break;
				case EntityRequestType.Card_Read_Members:
					if (!message.Properties.Contains(nameof(Card.Members))) return;
					member = Items.FirstOrDefault(b => b.Id == message.Data.Id);
					var cardIds = message.Data.Cards.Select(m => m.Id).ToList();
					if (!cardIds.Contains(OwnerId) && member != null)
						Items.Remove(member);
					else if (cardIds.Contains(OwnerId) && member == null)
						Items.Add(message.Data.GetFromCache<Member>(Auth));
					break;
				case EntityRequestType.Organization_Read_Members:
					if (!message.Properties.Contains(nameof(Organization.Members))) return;
					member = Items.FirstOrDefault(b => b.Id == message.Data.Id);
					var orgIds = message.Data.Organizations.Select(m => m.Id).ToList();
					if (!orgIds.Contains(OwnerId) && member != null)
						Items.Remove(member);
					else if (orgIds.Contains(OwnerId) && member == null)
						Items.Add(message.Data.GetFromCache<Member>(Auth));
					break;
			}
		}
	}
}