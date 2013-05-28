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
 
	File Name:		ExpiringObject.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		ExpiringObject
	Purpose:		A base class for an object which expires after a given time and
					automatically calls a service to refresh its data.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Trello.Internal;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// A base class for an object which expires after a given time and automatically
	/// calls a service to refresh its data.
	/// </summary>
	public abstract class ExpiringObject
	{
		private DateTime _expires;
		private ExpiringObject _owner;
		private ITrelloService _svc;

		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public virtual string Id { get; internal set; }
		/// <summary>
		/// Gets whether this object has expired is an needs to be updated.
		/// </summary>
		public bool IsExpired { get { return DateTime.Now >= _expires; } }

		/// <summary>
		/// Gets a collection of parameters to be added to a request which uses this object.
		/// </summary>
		/// <remarks>
		/// Parameters is cleared after each use.
		/// </remarks>
		protected Dictionary<string, object> Parameters { get; private set; }

		internal ExpiringObject Owner
		{
			get { return _owner; }
			set
			{
				if (_owner == value) return;
				_owner = value;
				Svc = _owner.Svc;
			}
		}
		internal ITrelloService Svc
		{
			get { return _svc; }
			set
			{
				if (_svc == value) return;
				_svc = value;
				if (_svc == null)
					Api = null;
				else
				{
					Api = _svc.Api;
					if (IsCacheableProvider.Default.IsCacheable(GetType()) && (_svc.Cache != null))
						_svc.Cache.Add(this);
				}
				PropigateService();
				MarkForUpdate();
			}
		}
		internal ITrelloRest Api { get; private set; }
		internal abstract string Key { get; }
		internal abstract string Key2 { get; }
		internal virtual string KeyId { get { return Id; } }

		internal ExpiringObject()
		{
			Parameters = new Dictionary<string, object>();
			MarkForUpdate();
		}

		/// <summary>
		/// Explicitly marks the entity as expired, forcing it to update.
		/// </summary>
		public void MarkForUpdate()
		{
			_expires = DateTime.Now.AddSeconds(-1);
		}

		internal void ForceNotExpired()
		{
			_expires = DateTime.Now.AddMinutes(1);
		}
		internal virtual bool Matches(string id)
		{
			return Id == id;
		}
		internal abstract void ApplyJson(object obj);

		/// <summary>
		/// Verifies that the object is not expired and updates if necessary.
		/// </summary>
		protected internal void VerifyNotExpired()
		{
			if ((Svc == null) || !Options.AutoRefresh || !IsExpired) return;
			Refresh();
			_expires = DateTime.Now + Options.ItemDuration;
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected abstract void Refresh();
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected abstract void PropigateService();
	}
}
