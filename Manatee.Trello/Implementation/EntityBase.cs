namespace Manatee.Trello.Implementation
{
	public abstract class EntityBase : JsonCompatibleEquatableExpiringObject
	{
		public string Id { get; protected set; }

		public EntityBase() {}
		internal EntityBase(TrelloService svc, string id)
			: base(svc)
		{
			Id = id;
		}
	}
}
