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
	/// A read-only collection of comment reactions.
	/// </summary>
	public class ReadOnlyCommentReactionCollection : ReadOnlyCollection<ICommentReaction>,
	                                                 IHandle<EntityDeletedEvent<IJsonCommentReaction>>
	{
		private readonly Dictionary<string, object> _requestParameters;

		internal ReadOnlyCommentReactionCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_requestParameters = new Dictionary<string, object>();

			EventAggregator.Subscribe(this);
		}

		internal ReadOnlyCommentReactionCollection(Func<string> getOwnerId, TrelloAuthorization auth, Dictionary<string, object> requestParameters = null)
			: base(getOwnerId, auth)
		{
			_requestParameters = requestParameters ?? new Dictionary<string, object>();

			EventAggregator.Subscribe(this);
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			IncorporateLimit();

			_requestParameters["_id"] = OwnerId;
			var allParameters = AdditionalParameters.Concat(CardContext.CurrentParameters)
			                                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			var endpoint = EndpointFactory.Build(EntityRequestType.Action_Read_Reactions, _requestParameters);
			var newData = await JsonRepository.Execute<List<IJsonCommentReaction>>(Auth, endpoint, ct, allParameters);

			var previousItems = new List<ICommentReaction>(Items);
			Items.Clear();
			EventAggregator.Unsubscribe(this);
			Items.AddRange(newData.Select(jc =>
				{
					var reaction = jc.GetFromCache<CommentReaction, IJsonCommentReaction>(Auth, true, OwnerId);
					reaction.Json = jc;
					return reaction;
				}));
			EventAggregator.Subscribe(this);
			var removedItems = previousItems.Except(Items, CacheableComparer.Get<ICommentReaction>()).OfType<CommentReaction>().ToList();
			foreach (var item in removedItems)
			{
				item.Json.Comment = null;
			}
		}

		void IHandle<EntityDeletedEvent<IJsonCommentReaction>>.Handle(EntityDeletedEvent<IJsonCommentReaction> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}
