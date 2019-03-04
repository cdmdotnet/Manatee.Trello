using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public interface ICommentReaction : ICacheable
	{
		Action Comment { get; }
		Emoji Emoji { get; }
		Member Member { get; }
		Task Delete(CancellationToken ct = default(CancellationToken));
	}

	public class CommentReaction : ICommentReaction, IMergeJson<IJsonCommentReaction>
	{
		private readonly Field<Action> _comment;
		private readonly Field<Emoji> _emoji;
		private readonly Field<Member> _member;
		private readonly CommentReactionContext _context;

		public Action Comment => _comment.Value;
		public Emoji Emoji => _emoji.Value;
		public string Id { get; }
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

		public Task Delete(CancellationToken ct = default(CancellationToken))
		{
			return _context.Delete(ct);
		}
	}
}
