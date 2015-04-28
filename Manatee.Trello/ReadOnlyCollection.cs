/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ReadOnlyCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyCollection
	Purpose:		Provides base functionality for a read-only collection.

***************************************************************************************/

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
	public abstract class ReadOnlyCollection<T> : IEnumerable<T>
	{
		private readonly TrelloAuthorization _auth;
		private readonly List<T> _items;
		private readonly string _ownerId;
		private DateTime _lastUpdate;

		/// <summary>
		/// Retrieves the item at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The item.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		public T this[int index] { get { return GetByIndex(index); } }

		internal string OwnerId { get { return _ownerId; } }
		internal List<T> Items { get { return _items; } }
		internal int? Limit { get; set; }
		internal TrelloAuthorization Auth { get { return _auth; } }

		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyCollection{T}"/> object.
		/// </summary>
		/// <param name="ownerId"></param>
		/// <param name="auth"></param>
		protected ReadOnlyCollection(string ownerId, TrelloAuthorization auth)
		{
			_ownerId = ownerId;
			_auth = auth ?? TrelloAuthorization.Default;

			_items = new List<T>();
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
			return _items.GetEnumerator();
		}
		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
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