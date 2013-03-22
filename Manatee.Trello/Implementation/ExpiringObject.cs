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
using Manatee.Trello.Rest;
using RestSharp;

namespace Manatee.Trello.Implementation
{
	public abstract class ExpiringObject
	{
		private DateTime _expires;
		private TrelloService _svc;

		public string Id { get; internal set; }
		public ExpiringObject Owner { get; internal set; }

		internal ParameterCollection Parameters { get; private set; }

		internal TrelloService Svc
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
		internal bool IsExpired { get { return DateTime.Now >= _expires; } }

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

		protected void VerifyNotExpired()
		{
			if ((Svc == null) || !Options.AutoRefresh || !IsExpired) return;
			Get();
			_expires = DateTime.Now + Options.ItemDuration;
		}

		internal abstract void Refresh(ExpiringObject entity);
		internal abstract bool Match(string id);
		protected abstract void PropigateSerivce();
		protected abstract void Get();
	}
}
