/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		CachingEntityRepository.cs
	Namespace:		Manatee.Trello.Internal.DataAccess
	Class Name:		CachingEntityRepository
	Purpose:		Decorates an implementation of IEntityRepository with caching.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.DataAccess
{
	public class CachingEntityRepository : IEntityRepository
	{
		private readonly IEntityRepository _innerRepository;
		private readonly ICache _cache;

		public TimeSpan EntityDuration { get { return _innerRepository.EntityDuration; } }

		public CachingEntityRepository(IEntityRepository innerRepository, ICache cache)
		{
			_innerRepository = innerRepository;
			_cache = cache;
		}

		public void Refresh<T>(T entity, EntityRequestType request)
			where T : ExpiringObject
		{
			_innerRepository.Refresh(entity, request);
		}
		public void RefreshCollecion<T>(ExpiringObject list, EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			_innerRepository.RefreshCollecion<T>(list, request, parameters);
		}
		public T Download<T>(EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject
		{
			T value = null;
			try
			{
				var id = parameters.SingleOrDefault(kvp => kvp.Key.In("_id")).Value.ToString();
				Func<T> query = () => _innerRepository.Download<T>(request, parameters);
				value = _cache.Find(e => e.Matches(id), query);
				return value;
			}
			catch (Exception)
			{
				_cache.Remove(value);
				throw;
			}
		}
		public void Upload(EntityRequestType request, IDictionary<string, object> parameters)
		{
			_innerRepository.Upload(request, parameters);
		}
	}
}