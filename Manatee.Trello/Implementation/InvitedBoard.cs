namespace Manatee.Trello.Implementation
{
	internal class InvitedBoard : Board
	{
		public InvitedBoard() {}
		internal InvitedBoard(TrelloService svc, string id)
			: base(svc, id) {}
	}
}