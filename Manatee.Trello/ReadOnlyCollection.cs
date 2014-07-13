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
	public abstract class ReadOnlyCollection<T> : IEnumerable<T>
	{
		private readonly List<T> _items;

		private readonly string _ownerId;

		protected string OwnerId { get { return _ownerId; } }
		protected List<T> Items { get { return _items; } }

		protected ReadOnlyCollection(string ownerId)
		{
			_ownerId = ownerId;
			_items = new List<T>();
		}

		public IEnumerator<T> GetEnumerator()
		{
			Update();
			return _items.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		protected abstract void Update();
	}
}