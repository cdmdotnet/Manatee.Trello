using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides base functionality for a read-only collection.
	/// </summary>
	/// <typeparam name="T">The type of object contained by the collection.</typeparam>
	public abstract class ReadOnlyCollection<T> : IReadOnlyCollection<T>
	{
		private string _ownerId;
		private readonly Func<string> _getOwnerId;

		/// <summary>
		/// Indicates the maximum number of items to return.
		/// </summary>
		public virtual int? Limit { get; set; }

		/// <summary>
		/// Retrieves the item at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The item.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		public T this[int index] => GetByIndex(index);

		internal string OwnerId => _ownerId ?? (_ownerId = _getOwnerId());
		internal List<T> Items { get; }
		internal TrelloAuthorization Auth { get; }
		internal Dictionary<string, object> AdditionalParameters { get; }

		internal event EventHandler Refreshed;

		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyCollection{T}"/> object.
		/// </summary>
		/// <param name="getOwnerId"></param>
		/// <param name="auth"></param>
		protected ReadOnlyCollection(Func<string> getOwnerId, TrelloAuthorization auth)
		{
			_getOwnerId = getOwnerId;
			Auth = auth ?? TrelloAuthorization.Default;

			Items = new List<T>();
			AdditionalParameters = new Dictionary<string, object>();
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			if (Auth == TrelloAuthorization.Null)
#if NET45
				return Task.Run(() => { }, ct);
#else
				return Task.CompletedTask;
#endif

			Refreshed?.Invoke(this, new EventArgs());

			return PerformRefresh(force, ct);
		}

		internal abstract Task PerformRefresh(bool force, CancellationToken ct);

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Adds <see cref="Limit"/> to a list of additional parameters.
		/// </summary>
		protected void IncorporateLimit()
		{
			if (!Limit.HasValue) return;

			AdditionalParameters["limit"] = Limit.Value;
		}

		internal void Update(IEnumerable<T> items)
		{
			Items.Clear();
			Items.AddRange(items);
		}

		private T GetByIndex(int index)
		{
			return this.ElementAt(index);
		}
	}
}