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

using System.Collections;
using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides base functionality for a read-only collection.
	/// </summary>
	/// <typeparam name="T">The type of object contained by the collection.</typeparam>
	public abstract class ReadOnlyCollection<T> : IEnumerable<T>
	{
		private readonly List<T> _items;
		private readonly string _ownerId;

		internal string OwnerId { get { return _ownerId; } }
		internal List<T> Items { get { return _items; } }

		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyCollection{T}"/> object.
		/// </summary>
		/// <param name="ownerId"></param>
		protected ReadOnlyCollection(string ownerId)
		{
			_ownerId = ownerId;
			_items = new List<T>();
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
			Update();
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
	}
}