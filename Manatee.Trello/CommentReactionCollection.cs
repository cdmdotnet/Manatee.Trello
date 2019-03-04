﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of cards.
	/// </summary>
	public class CommentReactionCollection : ReadOnlyCommentReactionCollection, ICommentReactionCollection
	{
		internal CommentReactionCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(typeof (List), getOwnerId, auth) {}

		/// <summary>
		/// Creates a new comment reaction.
		/// </summary>
		/// <param name="emoji">The <see cref="Emoji"/> to add.</param>
		/// <param name="ct">(Optional) A cancellation token.</param>
		/// <returns>The <see cref="ICommentReaction"/> generated by Trello.</returns>
		public async Task<ICommentReaction> Add(Emoji emoji, CancellationToken ct = default(CancellationToken))
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonCommentReaction>();
			json.Emoji = emoji;

			var endpoint = EndpointFactory.Build(EntityRequestType.Action_Write_AddReaction, new Dictionary<string, object> {["_id"] = OwnerId});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			return new CommentReaction(newData, OwnerId, Auth);
		}
	}
}