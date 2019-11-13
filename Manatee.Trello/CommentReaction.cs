using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a reaction to a card comment.
	/// </summary>
	public class CommentReaction : ICommentReaction, IMergeJson<IJsonCommentReaction>
	{
		private readonly Field<Action> _comment;
		private readonly Field<Emoji> _emoji;
		private readonly Field<Member> _member;
		private readonly CommentReactionContext _context;

		/// <summary>
		/// Gets the comment (<see cref="Action"/>) reacted to.
		/// </summary>
		public Action Comment => _comment.Value;

		/// <summary>
		/// Gets the emoji used for the reaction.
		/// </summary>
		public Emoji Emoji => _emoji.Value;

		/// <summary>
		/// Gets an ID on which matching can be performed.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Gets the member who posted the reaction.
		/// </summary>
		public Member Member => _member.Value;

		internal IJsonCommentReaction Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal CommentReaction(IJsonCommentReaction json, string ownerId, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new CommentReactionContext(json.Id, ownerId, auth);

			_comment = new Field<Action>(_context, nameof(Comment));
			_emoji = new Field<Emoji>(_context, nameof(Emoji));
			_member = new Field<Member>(_context, nameof(Member));

			_context.Merge(json);
		}

		void IMergeJson<IJsonCommentReaction>.Merge(IJsonCommentReaction json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		/// <summary>
		/// Deletes the reaction.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the reaction from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public Task Delete(CancellationToken ct = default)
		{
			return _context.Delete(ct);
		}
	}
}
