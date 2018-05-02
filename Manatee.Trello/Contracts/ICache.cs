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
		void Add(ICacheable obj);
		/// <summary>
		/// Finds an object of a certain type meeting specified criteria.
		/// </summary>
		/// <param name="id">The ID to search for.</param>
		/// <typeparam name="T">The type of object to find.</typeparam>
		T Find<T>(string id) where T : class, ICacheable;
		/// <summary>
		/// Removes an object from the cache, if it exists.
		/// </summary>
		/// <param name="obj">The object to remove.</param>
		void Remove(ICacheable obj);
		/// <summary>
		/// Removes all objects from the cache.
		/// </summary>
		void Clear();
	}
}