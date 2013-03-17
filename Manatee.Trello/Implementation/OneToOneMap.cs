/*
 *	Source: http://stackoverflow.com/a/268545/878701
 *	Modified as follows:
 *		- refactored to conform to personal coding practices
 *		- added ICollection<KeyValuePair<TFirst, TSecond>> implementation
 *		- added indexers
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Manatee.Trello.Implementation
{
	/// <summary>
	/// This is a dictionary guaranteed to have only one of each value and key. 
	/// It may be searched either by TFirst or by TSecond, giving a unique answer because it is 1 to 1.
	/// </summary>
	/// <typeparam name="TFirst">The type of the "key"</typeparam>
	/// <typeparam name="TSecond">The type of the "value"</typeparam>
	public class OneToOneMap<TFirst, TSecond> : ICollection<KeyValuePair<TFirst, TSecond>>
	{
		private readonly IDictionary<TFirst, TSecond> _firstToSecond = new Dictionary<TFirst, TSecond>();
		private readonly IDictionary<TSecond, TFirst> _secondToFirst = new Dictionary<TSecond, TFirst>();

		#region Indexers

		public TSecond this[TFirst first]
		{
			get { return GetByFirst(first); }
		}
		public TFirst this[TSecond second]
		{
			get { return GetBySecond(second); }
		}

		#endregion

		#region Exception throwing methods

		/// <summary>
		/// Tries to add the pair to the dictionary.
		/// Throws an exception if either element is already in the dictionary
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		public void Add(TFirst first, TSecond second)
		{
			if (_firstToSecond.ContainsKey(first) || _secondToFirst.ContainsKey(second))
				throw new ArgumentException("Duplicate first or second");

			_firstToSecond.Add(first, second);
			_secondToFirst.Add(second, first);
		}
		/// <summary>
		/// Find the TSecond corresponding to the TFirst first
		/// Throws an exception if first is not in the dictionary.
		/// </summary>
		/// <param name="first">the key to search for</param>
		/// <returns>the value corresponding to first</returns>
		public TSecond GetByFirst(TFirst first)
		{
			TSecond second;
			if (!_firstToSecond.TryGetValue(first, out second))
				throw new ArgumentException("first");

			return second;
		}
		/// <summary>
		/// Find the TFirst corresponing to the Second second.
		/// Throws an exception if second is not in the dictionary.
		/// </summary>
		/// <param name="second">the key to search for</param>
		/// <returns>the value corresponding to second</returns>
		public TFirst GetBySecond(TSecond second)
		{
			TFirst first;
			if (!_secondToFirst.TryGetValue(second, out first))
				throw new ArgumentException("second");

			return first;
		}
		/// <summary>
		/// Remove the record containing first.
		/// If first is not in the dictionary, throws an Exception.
		/// </summary>
		/// <param name="first">the key of the record to delete</param>
		public void RemoveByFirst(TFirst first)
		{
			TSecond second;
			if (!_firstToSecond.TryGetValue(first, out second))
				throw new ArgumentException("first");

			_firstToSecond.Remove(first);
			_secondToFirst.Remove(second);
		}
		/// <summary>
		/// Remove the record containing second.
		/// If second is not in the dictionary, throws an Exception.
		/// </summary>
		/// <param name="second">the key of the record to delete</param>
		public void RemoveBySecond(TSecond second)
		{
			TFirst first;
			if (!_secondToFirst.TryGetValue(second, out first))
				throw new ArgumentException("second");

			_secondToFirst.Remove(second);
			_firstToSecond.Remove(first);
		}

		#endregion

		#region Try methods

		/// <summary>
		/// Tries to add the pair to the dictionary.
		/// Returns false if either element is already in the dictionary        
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>true if successfully added, false if either element are already in the dictionary</returns>
		public bool TryAdd(TFirst first, TSecond second)
		{
			if (_firstToSecond.ContainsKey(first) || _secondToFirst.ContainsKey(second))
				return false;

			_firstToSecond.Add(first, second);
			_secondToFirst.Add(second, first);
			return true;
		}
		/// <summary>
		/// Find the TSecond corresponding to the TFirst first.
		/// Returns false if first is not in the dictionary.
		/// </summary>
		/// <param name="first">the key to search for</param>
		/// <param name="second">the corresponding value</param>
		/// <returns>true if first is in the dictionary, false otherwise</returns>
		public bool TryGetByFirst(TFirst first, out TSecond second)
		{
			return _firstToSecond.TryGetValue(first, out second);
		}
		/// <summary>
		/// Find the TFirst corresponding to the TSecond second.
		/// Returns false if second is not in the dictionary.
		/// </summary>
		/// <param name="second">the key to search for</param>
		/// <param name="first">the corresponding value</param>
		/// <returns>true if second is in the dictionary, false otherwise</returns>
		public bool TryGetBySecond(TSecond second, out TFirst first)
		{
			return _secondToFirst.TryGetValue(second, out first);
		}
		/// <summary>
		/// Remove the record containing first, if there is one.
		/// </summary>
		/// <param name="first"></param>
		/// <returns> If first is not in the dictionary, returns false, otherwise true</returns>
		public bool TryRemoveByFirst(TFirst first)
		{
			TSecond second;
			if (!_firstToSecond.TryGetValue(first, out second))
				return false;

			_firstToSecond.Remove(first);
			_secondToFirst.Remove(second);
			return true;
		}
		/// <summary>
		/// Remove the record containing second, if there is one.
		/// </summary>
		/// <param name="second"></param>
		/// <returns> If second is not in the dictionary, returns false, otherwise true</returns>
		public bool TryRemoveBySecond(TSecond second)
		{
			TFirst first;
			if (!_secondToFirst.TryGetValue(second, out first))
				return false;

			_secondToFirst.Remove(second);
			_firstToSecond.Remove(first);
			return true;
		}

		#endregion

		#region ICollection implementation

		/// <summary>
		/// The number of pairs stored in the dictionary
		/// </summary>
		public Int32 Count
		{
			get { return _firstToSecond.Count; }
		}
		public bool IsReadOnly
		{
			get { return _secondToFirst.IsReadOnly; }
		}

		public bool Remove(KeyValuePair<TFirst, TSecond> item)
		{
			return TryRemoveByFirst(item.Key);
		}
		public void Add(KeyValuePair<TFirst, TSecond> item)
		{
			Add(item.Key, item.Value);
		}
		/// <summary>
		/// Removes all items from the dictionary.
		/// </summary>
		public void Clear()
		{
			_firstToSecond.Clear();
			_secondToFirst.Clear();
		}
		public bool Contains(KeyValuePair<TFirst, TSecond> item)
		{
			return _firstToSecond.Contains(item);
		}
		public void CopyTo(KeyValuePair<TFirst, TSecond>[] array, int arrayIndex)
		{
			_firstToSecond.CopyTo(array, arrayIndex);
		}
		public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator()
		{
			return _firstToSecond.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
