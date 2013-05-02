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
	internal class ExpiringList<T, TJson> : ExpiringObject, IEnumerable<T>
		where T : ExpiringObject, IEquatable<T>, new()
	{
		private readonly List<T> _list;
		private readonly string _key;

		public string Filter { get; set; }

		internal override string Key { get { return _key; } }

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

		protected override sealed void Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			if (Filter != null)
				request.AddParameter("filter", Filter);
			ApplyJson(Api.Get<List<TJson>>(request));
		}
		protected override void PropigateService()
		{
			foreach (var item in _list)
			{
				item.Svc = Svc;
				item.Api = Api;
			}
		}

		internal override void ApplyJson(object obj)
		{
			_list.Clear();
			var jsonList = (List<TJson>) obj;
			foreach (var json in jsonList)
			{
				var entity = new T();
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
				_list.Add(entity);
			}
			PropigateService();
		}
	}
}
