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
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public virtual string Id { get; internal set; }
		/// <summary>
		/// Gets whether this object has expired is an needs to be updated.
		/// </summary>
		public bool IsExpired { get { return DateTime.Now >= Expires; } }
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public abstract bool IsStubbed { get; }

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

		/// <summary>
		/// Gets or sets when this entity expires.
		/// </summary>
		protected DateTime Expires { get; set; }

		internal ExpiringObject()
		{
			Parameters = new Dictionary<string, object>();
			Expires = DateTime.Now.AddSeconds(-1);
		}

		/// <summary>
		/// Explicitly marks the entity as expired, forcing it to update.
		/// </summary>
		public void MarkForUpdate()
		{
			Expires = DateTime.Now.AddSeconds(-1);
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public abstract bool Refresh();

		internal void ForceNotExpired()
		{
			Expires = DateTime.Now.AddMinutes(1);
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
			if (!IsExpired || IsStubbed || !Refresh()) return;
			if (Id != null)
				Log.Info("{0} with ID {{{1}}} will expire at {2}.", GetType().CSharpName(), Id, Expires);
			else if (Owner != null)
				Log.Info("{0} owned by {1} with ID {{{2}}} will expire at {3}.", GetType().CSharpName(), Owner.GetType().CSharpName(), Owner.Id, Expires);
			else
				Log.Info("A {0} will expire at {1}.", GetType().CSharpName(), Expires);
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
		/// <summary>
		/// Adds the default parameters for the type.
		/// </summary>
		protected void AddDefaultParameters()
		{
			foreach (var parameter in RestParameterRepository.GetParameters(GetType()))
			{
				Parameters[parameter.Key] = parameter.Value;
			}
		}
	}
}
