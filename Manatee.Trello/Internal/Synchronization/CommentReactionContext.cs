using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CommentReactionContext : DeletableSynchronizationContext<IJsonCommentReaction>
	{
		private readonly string _ownerId;

		static CommentReactionContext()
		{
			Properties = new Dictionary<string, Property<IJsonCommentReaction>>
				{
					{
						nameof(CommentReaction.Emoji),
						new Property<IJsonCommentReaction, Action>((d, a) => d.Comment?.GetFromCache<Action, IJsonAction>(a),
						                                           (d, o) => d.Comment = o?.Json)
					},
					{
						nameof(CommentReaction.Emoji),
						new Property<IJsonCommentReaction, Emoji>((d, a) => d.Emoji, (d, o) => d.Emoji = o)
					},
					{
						nameof(CommentReaction.Id),
						new Property<IJsonCommentReaction, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(CommentReaction.Emoji),
						new Property<IJsonCommentReaction, Member>((d, a) => d.Member?.GetFromCache<Member, IJsonMember>(a),
						                                           (d, o) => d.Member = o?.Json)
					}
				};
		}
		public CommentReactionContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.CommentReaction_Read_Refresh,
			                             new Dictionary<string, object> {{"_actionId", _ownerId}, {"_id", Data.Id}});
		}

		protected override Endpoint GetDeleteEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.CommentReaction_Write_Delete,
			                             new Dictionary<string, object> {{ "_actionId", _ownerId}, {"_id", Data.Id}});
		}
	}
}