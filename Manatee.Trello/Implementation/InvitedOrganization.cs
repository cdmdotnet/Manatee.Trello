namespace Manatee.Trello.Implementation
{
	internal class InvitedOrganization : Organization
	{
		public InvitedOrganization() {}
		internal InvitedOrganization(TrelloService svc, string id)
			: base(svc, id) {}
	}
}