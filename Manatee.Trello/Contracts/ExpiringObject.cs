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
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// A base class for an object which expires after a given time and automatically
	/// calls a service to refresh its data.
	/// </summary>
	public abstract class ExpiringObject
	{
		private DateTime _expires;
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

		internal ExpiringObject Owner { get; set; }
		internal ITrelloService Svc
		{
			get { return _svc; }
			set
			{
				if (_svc == value) return;
				_svc = value;
				PropagateService();
				MarkForUpdate();
				if (_svc == null) return;
				if (Id != null)
					Log.Debug("Updated service reference for {0} with ID {{{1}}}.", GetType().CSharpName(), Id);
				else if (Owner != null)
					Log.Debug("Updated service reference for {0} owned by {1} with ID {{{2}}}.", GetType().CSharpName(), Owner.GetType().CSharpName(), Owner.Id);
				else
					Log.Debug("Updated service reference for a {0}.", GetType().CSharpName());
				if (IsCacheableProvider.Default.IsCacheable(GetType()) && (_svc.Configuration.Cache != null))
				{
					Log.Info("Adding {0} with ID {{{1}}} to cache.", GetType().CSharpName(), Id);
					_svc.Configuration.Cache.Add(this);
				}
			}
		}
		internal IJsonRepository JsonRepository { get; set; }
		internal IRestRequestProvider RequestProvider {get{return Svc.Configuration.RestClientProvider.RequestProvider;}}
		internal ILog Log { get { return Svc == null ? null : Svc.Configuration.Log; } }
		internal IValidator Validator { get; set; }
		internal abstract string PrimaryKey { get; }
		internal abstract string SecondaryKey { get; }
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
			if (Svc == null) return;
			if (Id != null)
				Log.Debug("{0} with ID {{{1}}} marked to update.", GetType().CSharpName(), Id);
			else if (Owner != null)
				Log.Debug("{0} owned by {1} with ID {{{2}}} marked to update.", GetType().CSharpName(), Owner.GetType().CSharpName(), Owner.Id);
			else
				Log.Debug("A {0} has been marked to update.", GetType().CSharpName());
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public abstract bool Refresh();

		internal void ForceNotExpired()
		{
			_expires = DateTime.Now.AddMinutes(1);
			if (Svc == null) return;
			if (Id != null)
				Log.Debug("{0} with ID {{{1}}} has been marked as not expired.", GetType().CSharpName(), Id);
			else if (Owner != null)
				Log.Debug("{0} owned by {1} with ID {{{2}}} has been marked as not expired.", GetType().CSharpName(), Owner.GetType().CSharpName(), Owner.Id);
			else
				Log.Debug("A {0} has been marked as not expired.", GetType().CSharpName());
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
			if ((Svc == null) || !Svc.Configuration.AutoRefresh || !IsExpired) return;
			if (!Refresh()) return;
			_expires = DateTime.Now + Svc.Configuration.ItemDuration;
			if (Id != null)
				Log.Info("{0} with ID {{{1}}} will expire at {2}.", GetType().CSharpName(), Id, _expires);
			else if (Owner != null)
				Log.Info("{0} owned by {1} with ID {{{2}}} will expire at {3}.", GetType().CSharpName(), Owner.GetType().CSharpName(), Owner.Id, _expires);
			else
				Log.Info("A {0} will expire at {2}.", GetType().CSharpName(), _expires);
		}
		protected void UpdateService(ExpiringObject entity)
		{
			if (entity == null) return;
			entity.Validator = Validator;
			entity.JsonRepository = JsonRepository;
			entity.Svc = _svc;
		}
		/// <summary>
		/// Propagates the service instance to the object's owned objects.
		/// </summary>
		protected abstract void PropagateService();
	}
}
