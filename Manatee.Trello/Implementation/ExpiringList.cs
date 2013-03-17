using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Manatee.Trello.Implementation
{
	internal class ExpiringList<TSource, TContent> : ExpiringObject, IEntityCollection<TContent>
		where TContent : EquatableExpiringObject, new()
		where TSource : EntityBase
	{
		private readonly List<TContent> _list;
		private readonly TSource _source;

		public TContent this[int index]
		{
			get
			{
				VerifyNotExpired();
				return _list[index];
			}
			set { _list[index] = value; }
		}
		public TContent this[string id]
		{
			get
			{
				VerifyNotExpired();
				return _list.FirstOrDefault(i => i.Match(id));
			}
			set { Replace(id, value); }
		}
		public int Count
		{
			get
			{
				VerifyNotExpired();
				return _list.Count;
			}
		}
		public int Capacity
		{
			get { return _list.Capacity; }
			set { _list.Capacity = value; }
		}
		bool ICollection<TContent>.IsReadOnly
		{
			get
			{
				VerifyNotExpired();
				return ((ICollection<TContent>)_list).IsReadOnly;
			}
		}

		public ExpiringList(TSource source)
		{
			_source = source;
			_list = new List<TContent>();
		}
		public ExpiringList(TrelloService svc, TSource source)
			: base(svc)
		{
			_source = source;
			_list = new List<TContent>();
		}
		public ExpiringList(TrelloService svc, TSource source, IEnumerable<TContent> items)
			: base(svc)
		{
			_source = source;
			_list = new List<TContent>(items);
		}

		public void Add(TContent item)
		{
			VerifyNotExpired();
			_list.Add(item);
		}
		public void AddRange(IEnumerable<TContent> collection)
		{
			VerifyNotExpired();
			_list.AddRange(collection);
		}
		public ReadOnlyCollection<TContent> AsReadOnly()
		{
			VerifyNotExpired();
			return _list.AsReadOnly();
		}
		public int BinarySearch(TContent item, IComparer<TContent> comparer)
		{
			VerifyNotExpired();
			return _list.BinarySearch(item, comparer);
		}
		public int BinarySearch(int index, int count, TContent item, IComparer<TContent> comparer)
		{
			VerifyNotExpired();
			return _list.BinarySearch(index, count, item, comparer);
		}
		public void Clear()
		{
			_list.Clear();
		}
		public bool Contains(TContent item)
		{
			VerifyNotExpired();
			return _list.Contains(item);
		}
		public List<TOutput> ConvertAll<TOutput>(Converter<TContent, TOutput> converter)
		{
			VerifyNotExpired();
			return _list.ConvertAll(converter);
		}
		public void CopyTo(TContent[] array)
		{
			VerifyNotExpired();
			_list.CopyTo(array);
		}
		void ICollection<TContent>.CopyTo(TContent[] array, int arrayIndex)
		{
			VerifyNotExpired();
			((ICollection<TContent>)_list).CopyTo(array, arrayIndex);
		}
		void CopyTo(TContent[] array, int arrayIndex)
		{
			VerifyNotExpired();
			_list.CopyTo(array, arrayIndex);
		}
		public void CopyTo(int index, TContent[] array, int arrayIndex, int count)
		{
			VerifyNotExpired();
			_list.CopyTo(index, array, arrayIndex, count);
		}
		public bool Exists(Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.Exists(match);
		}
		public TContent Find(Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.Find(match);
		}
		public List<TContent> FindAll(Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.FindAll(match);
		}
		public int FindIndex(Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.FindIndex(match);
		}
		public int FindIndex(int startIndex, int count, Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.FindIndex(startIndex, count, match);
		}
		public TContent FindLast(Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.FindLast(match);
		}
		public int FindLastIndex(Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.FindLastIndex(match);
		}
		public int FindLastIndex(int startIndex, int count, Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.FindLastIndex(startIndex, count, match);
		}
		public void ForEach(Action<TContent> action)
		{
			VerifyNotExpired();
			_list.ForEach(action);
		}
		IEnumerator<TContent> IEnumerable<TContent>.GetEnumerator()
		{
			return ((IEnumerable<TContent>)_list).GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_list).GetEnumerator();
		}
		public List<TContent>.Enumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}
		public List<TContent> GetRange(int index, int count)
		{
			VerifyNotExpired();
			return _list.GetRange(index, count);
		}
		public int IndexOf(TContent item)
		{
			VerifyNotExpired();
			return _list.IndexOf(item);
		}
		public int IndexOf(TContent item, int index)
		{
			VerifyNotExpired();
			return _list.IndexOf(item, index);
		}
		public int IndexOf(TContent item, int index, int count)
		{
			VerifyNotExpired();
			return _list.IndexOf(item, index, count);
		}
		public void Insert(int index, TContent item)
		{
			VerifyNotExpired();
			_list.Insert(index, item);
		}
		public void InsertRange(int index, IEnumerable<TContent> collection)
		{
			VerifyNotExpired();
			_list.InsertRange(index, collection);
		}
		public int LastIndexOf(TContent item)
		{
			VerifyNotExpired();
			return _list.LastIndexOf(item);
		}
		public int LastIndexOf(TContent item, int index)
		{
			VerifyNotExpired();
			return _list.LastIndexOf(item, index);
		}
		public int LastIndexOf(TContent item, int index, int count)
		{
			VerifyNotExpired();
			return _list.LastIndexOf(item, index, count);
		}
		public bool Remove(TContent item)
		{
			VerifyNotExpired();
			return _list.Remove(item);
		}
		public int RemoveAll(Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.RemoveAll(match);
		}
		public void RemoveAt(int index)
		{
			VerifyNotExpired();
			_list.RemoveAt(index);
		}
		public void RemoveRange(int index, int count)
		{
			VerifyNotExpired();
			_list.RemoveRange(index, count);
		}
		public void Reverse()
		{
			VerifyNotExpired();
			_list.Reverse();
		}
		public void Reverse(int index, int count)
		{
			VerifyNotExpired();
			_list.Reverse(index, count);
		}
		public void Sort()
		{
			VerifyNotExpired();
			_list.Sort();
		}
		public void Sort(Comparison<TContent> comparison)
		{
			VerifyNotExpired();
			_list.Sort(comparison);
		}
		public void Sort(IComparer<TContent> comparer)
		{
			VerifyNotExpired();
			_list.Sort(comparer);
		}
		public void Sort(int index, int count, IComparer<TContent> comparer)
		{
			VerifyNotExpired();
			_list.Sort(index, count, comparer);
		}
		public TContent[] ToArray()
		{
			VerifyNotExpired();
			return _list.ToArray();
		}
		public void TrimExcess()
		{
			VerifyNotExpired();
			_list.TrimExcess();
		}
		public bool TrueForAll(Predicate<TContent> match)
		{
			VerifyNotExpired();
			return _list.TrueForAll(match);
		}
		public override string ToString()
		{
			return _list.ToString();
		}

		protected override sealed void Refresh()
		{
			_list.Clear();
			var entities = Svc.RetrieveContent<TSource, TContent>(_source.Id);
			if (entities == null) return;
			foreach (var entity in entities)
			{
				entity.Svc = Svc;
			}
			_list.AddRange(entities);
		}
		protected override void PropigateSerivce()
		{
			_list.ForEach(i => i.Svc = Svc);
		}

		private void Replace(string id, TContent value)
		{
			var index = _list.FindIndex(i => i.Match(id));
			if (index == -1) return;
			_list.RemoveAt(index);
			_list.Insert(index, value);
		}
	}
}
