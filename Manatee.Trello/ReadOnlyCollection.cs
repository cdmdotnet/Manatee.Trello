using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides base functionality for a read-only collection.
	/// </summary>
	/// <typeparam name="T">The type of object contained by the collection.</typeparam>
	public abstract class ReadOnlyCollection<T> : IReadOnlyCollection<T>
	{
		private DateTime _lastUpdate;
		private string _ownerId;
		private readonly Func<string> _getOwnerId;

		/// <summary>
		/// Indicates the maximum number of items to return.
		/// </summary>
		public int? Limit { get; set; }
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
			_lastUpdate = DateTime.MinValue;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<T> GetEnumerator()
		{
			if (DateTime.Now >= _lastUpdate.Add(TrelloConfiguration.ExpiryTime))
			{
				Update();
				_lastUpdate = DateTime.Now;
			}
			return Items.GetEnumerator();
		}
		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected abstract void Update();

		/// <summary>
		/// Adds <see cref="Limit"/> to a list of additional parameters.
		/// </summary>
		/// <param name="additionalParameters">The list of additional parameters.</param>
		protected void IncorporateLimit(Dictionary<string, object> additionalParameters)
		{
			if (!Limit.HasValue) return;

			additionalParameters["limit"] = Limit.Value;
		}

		private T GetByIndex(int index)
		{
			return this.ElementAt(index);
		}
	}
}