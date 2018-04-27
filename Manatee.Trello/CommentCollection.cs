using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of <see cref="Action"/>s of types <see cref="ActionType.CommentCard"/> and <see cref="ActionType.CopyCommentCard"/>.
	/// </summary>
	public class CommentCollection : ReadOnlyActionCollection, ICommentCollection
	{
		internal CommentCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(typeof (Card), getOwnerId, auth)
		{
			Filter(ActionType.CommentCard | ActionType.CopyCommentCard);
		}

		/// <summary>
		/// Posts a new comment to a card.
		/// </summary>
		/// <param name="text">The content of the comment.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The <see cref="IAction"/> associated with the comment.</returns>
		public async Task<IAction> Add(string text, CancellationToken ct = default(CancellationToken))
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, text);
			if (error != null)
				throw new ValidationException<string>(text, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonAction>();
			json.Text = text;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddComment, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			return new Action(newData, Auth);
		}
	}
}