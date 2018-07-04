using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello.Internal.Synchronization
{
	internal abstract class DeletableSynchronizationContext<T> : SynchronizationContext<T> where T : class
	{
		protected bool Deleted { get; set; }

		protected DeletableSynchronizationContext(TrelloAuthorization auth, bool useTimer = true)
			: base(auth, useTimer)
		{
		}

		public virtual async Task Delete(CancellationToken ct)
		{
			if (Deleted) return;
			CanUpdate();

			var endpoint = GetDeleteEndpoint();
			await JsonRepository.Execute(Auth, endpoint, ct);

			Deleted = true;
			RaiseDeleted();
		}

		protected abstract Endpoint GetDeleteEndpoint();

		protected override async Task<T> GetData(CancellationToken ct)
		{
			try
			{
				return await base.GetData(ct);
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				Deleted = true;
				return Data;
			}
		}
		protected override bool CanUpdate()
		{
			return !Deleted;
		}
	}
}
