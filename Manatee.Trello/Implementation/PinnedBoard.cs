namespace Manatee.Trello.Implementation
{
	internal class PinnedBoard : Board
	{
		public PinnedBoard() { }
		internal PinnedBoard(TrelloService svc, string id)
			: base(svc, id) { }
	}
}