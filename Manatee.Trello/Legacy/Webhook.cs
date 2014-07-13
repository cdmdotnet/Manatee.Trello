/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Webhook.cs
	Namespace:		Manatee.Trello
	Class Name:		Webhook
	Purpose:		Details a requested webhook to Trello.com.

***************************************************************************************/

using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Details a requested webhook to Trello.com.
	/// </summary>
	/// <typeparam name="T">The type of entity the webhook monitors.</typeparam>
	public class Webhook<T> : ExpiringObject, IEquatable<Webhook<T>>
		where T : ExpiringObject, ICanWebhook
	{
		private IJsonWebhook _jsonWebhook;
		private ExpiringObject _entity;
		private bool _isDeleted;

		/// <summary>
		/// Gets or sets the URL which receives notification messages.
		/// </summary>
		public string CallbackUrl
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonWebhook.CallbackUrl;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Url(value);
				if (_jsonWebhook.CallbackUrl == value) return;
				_jsonWebhook.CallbackUrl = value;
				Parameters.Add("callbackURL", _jsonWebhook.CallbackUrl ?? string.Empty);
				Upload(EntityRequestType.Webhook_Write_CallbackUrl);
			}
		}
		/// <summary>
		/// Gets or sets the description of the webhook.
		/// </summary>
		public string Description
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonWebhook.Description;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				if (_jsonWebhook.Description == value) return;
				_jsonWebhook.Description = value;
				Parameters.Add("description", _jsonWebhook.Description ?? string.Empty);
				Upload(EntityRequestType.Webhook_Write_Description);
			}
		}
		/// <summary>
		/// Gets or sets the entity which the webhook monitors.
		/// </summary>
		public T Entity
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (T) UpdateById(ref _entity, EntityRequestType.Webhook_Read_Refresh, _jsonWebhook.IdModel);
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Entity(value);
				if (_jsonWebhook.IdModel == value.Id) return;
				_jsonWebhook.IdModel = value.Id;
				Parameters.Add("idModel", _jsonWebhook.IdModel);
				Upload(EntityRequestType.Webhook_Write_Entity);
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public sealed override string Id
		{
			get { return _jsonWebhook.Id; }
			internal set { _jsonWebhook.Id = value; }
		}
		/// <summary>
		/// Gets or sets whether the webhook is active.
		/// </summary>
		public bool? IsActive
		{
			get { return _isDeleted ? null : _jsonWebhook.Active; }
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonWebhook.Active == value) return;
				_jsonWebhook.Active = value;
				Parameters.Add("active", _jsonWebhook.Active.ToLowerString());
				Upload(EntityRequestType.Webhook_Write_Active);
			}
		}

		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonWebhook is InnerJsonWebhook; } }

		/// <summary>
		/// Creates a new instance of the <see cref="Webhook&lt;T&gt;"/> class.
		/// </summary>
		public Webhook()
		{
			_jsonWebhook = new InnerJsonWebhook();
		}

		/// <summary>
		/// Deletes the webhook.
		/// </summary>
		public void Delete()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.Webhook_Write_Delete, Parameters);
			_isDeleted = true;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Webhook<T> other)
		{
			return Equals(Id, other.Id);
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			Parameters["_id"] = Id;
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.Webhook_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonWebhook = (IJsonWebhook) obj;
		}
		internal override bool EqualsJson(object obj)
		{
			var json = obj as IJsonWebhook;
			return (json != null) && (json.Id == _jsonWebhook.Id);
		}

		private void Upload(EntityRequestType requestType)
		{
			Parameters["_id"] = Id;
			EntityRepository.Upload(requestType, Parameters);
		}
	}
}