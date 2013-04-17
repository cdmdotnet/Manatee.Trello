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
using Manatee.Trello.Implementation;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// A base class for an object which expires after a given time and automatically
	/// calls a service to refresh its data.
	/// </summary>
	public abstract class ExpiringObject
	{
		private DateTime _expires;
		private ITrelloRest _svc;
		/// <summary>
		/// Indicates that the object has refreshed from the website at least once.
		/// </summary>
		protected bool _isInitialized;

		/// <summary>
		/// Gets a unique identifier  (not necessarily a GUID).
		/// </summary>
		public string Id { get; internal set; }
		/// <summary>
		/// Gets and sets an object which owns this one.
		/// </summary>
		public ExpiringObject Owner { get; internal set; }
		/// <summary>
		/// Gets a collection of parameters to be added to a request which uses this object.
		/// </summary>
		/// <remarks>
		/// Parameters is cleared after each use.
		/// </remarks>
		public Dictionary<string, object> Parameters { get; private set; }
		/// <summary>
		/// Gets whether this object has expired is an needs to be updated.
		/// </summary>
		public bool IsExpired { get { return DateTime.Now >= _expires; } }

		internal ITrelloRest Svc
		{
			get { return _svc; }
			set
			{
				if (_svc == value) return;
				_svc = value;
				PropigateService();
				MarkForUpdate();
			}
		}
		internal abstract string Key { get; }

		internal ExpiringObject()
		{
			Parameters = new Dictionary<string, object>();
			MarkForUpdate();
		}
		internal ExpiringObject(ITrelloRest svc)
			: this()
		{
			Svc = svc;
		}
		internal ExpiringObject(ITrelloRest svc, string id)
			: this(svc)
		{
			Id = id;
		}
		internal ExpiringObject(ITrelloRest svc, ExpiringObject owner)
			: this(svc)
		{
			Owner = owner;
		}

		internal void MarkForUpdate()
		{
			_expires = DateTime.Now.AddSeconds(-1);
		}
		internal void ForceNotExpired()
		{
			_expires = DateTime.Now.AddMinutes(1);
		}

		/// <summary>
		/// Verifies that the object is not expired and updates if necessary.
		/// </summary>
		protected void VerifyNotExpired()
		{
			if ((Svc == null) || !Options.AutoRefresh || !IsExpired) return;
			Get();
			_expires = DateTime.Now + Options.ItemDuration;
		}

		internal abstract void Refresh(ExpiringObject entity);

		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected abstract void PropigateService();
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected abstract void Get();
	}
}
