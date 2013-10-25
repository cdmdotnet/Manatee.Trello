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
 
	File Name:		ExpiringList.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		ExpiringList<TSource, TContent>
	Purpose:		A collection of entities which automatically updates.

***************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal
{
	internal class ExpiringList<T> : ExpiringObject, IEnumerable<T>
		where T : ExpiringObject, IEquatable<T>, IComparable<T>
	{
		private readonly EntityRequestType _requestType;
		private readonly List<T> _list;

		public string Filter { get; set; }
		public string Fields { get; set; }
		public override bool IsStubbed { get { return Owner == null || Owner.IsStubbed; } }

		public ExpiringList(ExpiringObject owner, EntityRequestType requestType)
		{
			_requestType = requestType;
			Owner = owner;
			_list = new List<T>();
		}
		public IEnumerator<T> GetEnumerator()
		{
			VerifyNotExpired();
			Expires = DateTime.Now;
			return _list.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public override string ToString()
		{
			return _list.ToString();
		}
		public override sealed bool Refresh()
		{
			if (Owner != null)
				Parameters.Add("_id", Owner.Id);
			if (Filter != null)
				Parameters.Add("filter", Filter);
			if (Fields != null)
				Parameters.Add("fields", Fields);
			EntityRepository.RefreshCollection<T>(this, _requestType);
			return true;
		}

		internal override void PropagateDependencies()
		{
			foreach (var item in _list)
			{
				item.Owner = Owner;
			}
		}

		internal void Update(IEnumerable<T> items)
		{
			_list.Clear();
			_list.AddRange(items);
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override void ApplyJson(object obj) {}
	}
}
