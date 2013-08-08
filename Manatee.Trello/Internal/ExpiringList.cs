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
using System.Linq;
using System.Threading;
using Manatee.Trello.Contracts;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	internal class ExpiringList<T, TJson> : ExpiringObject, IEnumerable<T>
		where T : ExpiringObject, IEquatable<T>, IComparable<T>,  new()
	{
		private readonly List<T> _list;
		private readonly string _key;
		private readonly object _lockObject = new object();

		public string Filter { get; set; }
		public string Fields { get; set; }

		internal override string Key { get { return _key; } }
		internal override string Key2 { get { return _key; } }

		public ExpiringList(ExpiringObject owner, string contentKey)
		{
			Owner = owner;
			_list = new List<T>();
			_key = contentKey;
		}
		public IEnumerator<T> GetEnumerator()
		{
			VerifyNotExpired();
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
			var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			var request = RequestProvider.Create(endpoint.ToString());
			if (Filter != null)
				request.AddParameter("filter", Filter);
			if (Fields != null)
				request.AddParameter("fields", Fields);
			var obj = Api.Get<List<TJson>>(request);
			if (obj == null) return false;
			ApplyJson(obj);
			return true;
		}
		
		protected override void PropigateService()
		{
			foreach (var item in _list)
			{
				if (Owner != null)
					item.Owner = Owner;
				else
					item.Svc = Svc;
			}
		}

		internal override void ApplyJson(object obj)
		{
			_list.Clear();
			List<TJson> jsonList;
			if (obj is IRestResponse)
				jsonList = ((IRestResponse<List<TJson>>)obj).Data;
			else
				jsonList = (List<TJson>) obj;
			var entities = new List<T>();
			var threads = jsonList.Select(j => new Thread(() => AsyncRetrieve(entities, j)) { IsBackground = true }).ToList();
			foreach (var thread in threads)
			{
				thread.Start();
			}
			while (threads.Any(t => t.IsAlive)) {}
			_list.AddRange(entities.OrderBy(e => e).ToList());
			PropigateService();
		}

		private void AsyncRetrieve(ICollection<T> entities, TJson json)
		{
			T entity;
			if (IsCacheableProvider.Default.IsCacheable<T>())
			{
				var jsonCacheable = json as IJsonCacheable;
				entity = Svc.Retrieve<T>(jsonCacheable.Id);
			}
			else
			{
				entity = new T();
				entity.ApplyJson(json);
				if (typeof(T).IsAssignableFrom(typeof(Action)))
				{
					var typedEntity = ActionProvider.Default.Parse(entity as Action) as T;
					if (typedEntity != null)
					{
						typedEntity.ApplyJson(json);
						entity = typedEntity;
					}
				}
				else if (typeof(T).IsAssignableFrom(typeof(Notification)))
				{
					var typedEntity = NotificationProvider.Default.Parse(entity as Notification) as T;
					if (typedEntity != null)
					{
						typedEntity.ApplyJson(json);
						entity = typedEntity;
					}
				}
			}
			lock (_lockObject)
			{
				entities.Add(entity);
			}
		}
	}
}
