using System;
using System.Collections.Generic;
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
		/// <returns>The <see cref="Action"/> associated with the comment.</returns>
		public IAction Add(string text)
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, text);
			if (error != null)
				throw new ValidationException<string>(text, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonAction>();
			json.Text = text;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddComment, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);

			return new Action(newData, Auth);
		}
	}
}