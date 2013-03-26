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
using Manatee.Trello.Contracts;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Implementation
{
	/// <summary>
	/// A base class for an object which expires after a given time and automatically
	/// calls a service to refresh its data.
	/// </summary>
	public abstract class ExpiringObject
	{
		private DateTime _expires;
		private TrelloService _svc;

		/// <summary>
		/// Gets a unique identifier  (not necessarily a GUID).
		/// </summary>
		public string Id { get; internal set; }
		/// <summary>
		/// Gets and sets an object which owns this one.
		/// </summary>
		public ExpiringObject Owner { get; set; }
		/// <summary>
		/// Gets a collection of parameters to be added to a request which uses this object.
		/// </summary>
		/// <remarks>
		/// Parameters is cleared after each use.
		/// </remarks>
		public ParameterCollection Parameters { get; private set; }
		/// <summary>
		/// Gets and sets the service which manages this object.
		/// </summary>
		/// <remarks>
		/// This instance is used to refresh the object's data.  Setting this property will propigate
		/// the instance to the objects which it owns.
		/// </remarks>
		public TrelloService Svc
		{
			get { return _svc; }
			set
			{
				if (_svc == value) return;
				_svc = value;
				PropigateSerivce();
				MarkForUpdate();
			}
		}
		/// <summary>
		/// Gets whether this object has expired is an needs to be updated.
		/// </summary>
		public bool IsExpired { get { return DateTime.Now >= _expires; } }

		internal ExpiringObject()
		{
			Parameters = new ParameterCollection();
			MarkForUpdate();
		}
		internal ExpiringObject(TrelloService svc)
			: this()
		{
			Svc = svc;
		}
		internal ExpiringObject(TrelloService svc, string id)
			: this(svc)
		{
			Id = id;
		}
		internal ExpiringObject(TrelloService svc, ExpiringObject owner)
			: this(svc)
		{
			Owner = owner;
		}

		internal void MarkForUpdate()
		{
			_expires = DateTime.Now - TimeSpan.FromSeconds(1);
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

		internal abstract bool Match(string id);
		internal abstract void Refresh(ExpiringObject entity);

		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected abstract void PropigateSerivce();
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected abstract void Get();
	}
}
