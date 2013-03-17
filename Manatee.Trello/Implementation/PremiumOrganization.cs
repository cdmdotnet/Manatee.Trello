namespace Manatee.Trello.Implementation
{
	internal class PremiumOrganization : Organization
	{
		public PremiumOrganization() { }
		internal PremiumOrganization(TrelloService svc, string id)
			: base(svc, id) { }
	}
}