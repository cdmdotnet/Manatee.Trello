using System;
using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides base functionality for a read-only collection.
	/// </summary>
	/// <typeparam name="T">The type of object contained by the collection.</typeparam>
	public interface IReadOnlyCollection<out T> : IEnumerable<T>
	{
		/// <summary>
		/// Indicates the maximum number of items to return.
		/// </summary>
		int? Limit { get; set; }
		/// <summary>
		/// Retrieves the item at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The item.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		T this[int index] { get; }
	}
}