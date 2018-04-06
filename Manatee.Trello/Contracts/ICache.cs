using System;
using System.Collections;

namespace Manatee.Trello
{
	/// <summary>
	/// Defines operations for a cache.
	/// </summary>
	public interface ICache : IEnumerable
	{
		/// <summary>
		/// Adds an object to the cache, if it does not already exist.
		/// </summary>
		/// <param name="obj">The object to add.</param>
		void Add(object obj);
		/// <summary>
		/// Finds an object of a certain type meeting specified criteria.
		/// </summary>
		/// <typeparam name="T">The type of object to find.</typeparam>
		/// <param name="match">A function which evaluates the matching criteria.</param>
		T Find<T>(Func<T, bool> match);
		/// <summary>
		/// Removes an object from the cache, if it exists.
		/// </summary>
		/// <param name="obj">The object to remove.</param>
		void Remove(object obj);
		/// <summary>
		/// Removes all objects from the cache.
		/// </summary>
		void Clear();
	}
}