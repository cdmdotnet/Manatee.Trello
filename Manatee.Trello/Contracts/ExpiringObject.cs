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

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// A base class for an object which expires after a given time and automatically
	/// calls a service to refresh its data.
	/// </summary>
	public abstract class ExpiringObject
	{
		private DateTime _expires;

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
		internal Dictionary<string, object> Parameters { get; private set; }

		internal ExpiringObject Owner { get; set; }
		internal ILog Log { get; set; }
		internal IValidator Validator { get; set; }
		internal IEntityRepository EntityRepository { get; set; }

		internal ExpiringObject()
		{
			Parameters = new Dictionary<string, object>();
			// BUG: For some reason, the compiler instructs the Parameters list to initialize with data.
			// BUG: Should probably figure this out.
			_expires = DateTime.Now.AddSeconds(-1);
		}

		/// <summary>
		/// Explicitly marks the entity as expired, forcing it to update.
		/// </summary>
		public void MarkForUpdate()
		{
			_expires = DateTime.Now.AddSeconds(-1);
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
		/// <summary>
		/// Updates the dependencies of an entity to that of this object.
		/// </summary>
		/// <param name="entity"></param>
		internal void UpdateDependencies(ExpiringObject entity)
		{
			if (entity == null) return;
			entity.Log = Log;
			entity.Validator = Validator;
			entity.EntityRepository = EntityRepository;
		}
		internal abstract void ApplyJson(object obj);
		internal virtual void PropagateDependencies() { }

		/// <summary>
		/// Verifies that the object is not expired and updates if necessary.
		/// </summary>
		protected internal void VerifyNotExpired()
		{
			if (!IsExpired || !Refresh()) return;
			_expires = DateTime.Now + EntityRepository.EntityDuration;
			if (Id != null)
				Log.Info("{0} with ID {{{1}}} will expire at {2}.", GetType().CSharpName(), Id, _expires);
			else if (Owner != null)
				Log.Info("{0} owned by {1} with ID {{{2}}} will expire at {3}.", GetType().CSharpName(), Owner.GetType().CSharpName(), Owner.Id, _expires);
			else
				Log.Info("A {0} will expire at {2}.", GetType().CSharpName(), _expires);
		}
		/// <summary>
		/// Updates a reference to another object if null by downloading it from trello.com.
		/// </summary>
		/// <typeparam name="T">The ExpiringObject type.</typeparam>
		/// <param name="entity">The entity reference</param>
		/// <param name="requestType">The request type used to download the entity.</param>
		/// <param name="id">The entity id.</param>
		/// <returns></returns>
		protected T UpdateById<T>(ref T entity, EntityRequestType requestType, string id)
			where T : ExpiringObject
		{
			if ((entity == null) || (entity.Id != id))
			{
				entity = EntityRepository.Download<T>(requestType, new Dictionary<string, object> {{"_id", id}});
				UpdateDependencies(entity);
			}
			return entity;
		}
		protected void AddDefaultParameters()
		{
			foreach (var parameter in RestParameterRepository.GetParameters(GetType()))
			{
				Parameters.Add(parameter.Key, parameter.Value);
			}
		}
	}
}
