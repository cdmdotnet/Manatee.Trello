/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ICache.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		ICache
	Purpose:		Defines operations for a cache.

***************************************************************************************/
using System;
using System.Collections;

namespace Manatee.Trello.Contracts
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