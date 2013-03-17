namespace Manatee.Trello.Implementation
{
	public abstract class OwnedEntityBase<TOwner> : JsonCompatibleEquatableExpiringObject
		where TOwner : EntityBase
	{
		public TOwner Owner { get; protected set; }

		public OwnedEntityBase() {}
		internal OwnedEntityBase(TrelloService svc, TOwner owner)
			: base(svc)
		{
			Owner = owner;
		}
	}
}