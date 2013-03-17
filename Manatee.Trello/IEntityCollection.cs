using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	public interface IEntityCollection<T> : IList<T>
		where T : EquatableExpiringObject, new()
	{
		T this[string id] { get; set; }
		//bool All(Func<T, bool> predicate);
		//bool Any();
		//bool Any(Func<T, bool> predicate);
		//IEntityCollection<TResult> Cast<TResult>() where TResult : EquatableExpiringObject, new();
		//IEntityCollection<T> Concat(IEntityCollection<T> other);
		//bool Contains(T value, Func<T, bool> predicate);
		//int Count();
		//int Count(Func<T, bool> predicate);
		//IEntityCollection<T> DefaultIfEmpty();
		//IEntityCollection<T> DefaultIfEmpty(T defaultValue);
		//IEntityCollection<T> Distinct();
		//IEntityCollection<T> Distinct(IEqualityComparer<T> comparer);
		//T ElementAt(int index);
		//T ElementAtOrDefault(int index);
		//IEntityCollection<T> Except(IEntityCollection<T> other);
		//IEntityCollection<T> Except(IEntityCollection<T> other, Func<T, bool> predicate);
		//T First();
		//T First(Func<T, bool> predicate);
		//T FirstOrDefault();
		//T FirstOrDefault(Func<T, bool> predicate);
	}
}
