using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	public class CommentReaction
	{
		public string Id { get; }
		public Member Member { get; }
		public Action Comment { get; }
		public Emoji Emoji { get; }
	}
}
