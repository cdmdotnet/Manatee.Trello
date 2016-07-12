/***************************************************************************************

	Copyright 2015 Greg Dennis

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
	Purpose:		Represents a webhook.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides a common base class for the generic Webhook classes.
	/// </summary>
	public abstract class Webhook
	{
		internal Webhook() {}

		/// <summary>
		/// Processes webhook notification content.
		/// </summary>
		/// <param name="content">The string content of the notification.</param>
		/// <param name="auth">The <see cref="TrelloAuthorization"/> under which the notification should be processed</param>
		public static void ProcessNotification(string content, TrelloAuthorization auth = null)
		{
			var notification = TrelloConfiguration.Deserializer.Deserialize<IJsonWebhookNotification>(content);
			var action = new Action(notification.Action, auth);

			foreach (var obj in TrelloConfiguration.Cache.OfType<ICanWebhook>())
			{
				obj.ApplyAction(action);
			}
		}
	}

	/// <summary>
	/// Represents a webhook.
	/// </summary>
	/// <typeparam name="T">The type of object to which the webhook is attached.</typeparam>
	public class Webhook<T> : Webhook
		where T : class, ICanWebhook
	{
		private readonly Field<string> _callBackUrl;
		private readonly Field<string> _description;
		private readonly Field<bool?> _isActive;
		private readonly Field<T> _target;
		private readonly WebhookContext<T> _context;
		private DateTime? _creation;

		/// <summary>
		/// Gets or sets a callback URL for the webhook.
		/// </summary>
		public string CallBackUrl
		{
			get { return _callBackUrl.Value; }
			set { _callBackUrl.Value = value; }
		}
		/// <summary>
		/// Gets the creation date of the webhook.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets or sets a description for the webhook.
		/// </summary>
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		/// <summary>
		/// Gets the webhook's ID>
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets or sets whether the webhook is active.
		/// </summary>
		public bool? IsActive
		{
			get { return _isActive.Value; }
			set { _isActive.Value = value; }
		}
		/// <summary>
		/// Gets or sets the webhook's target.
		/// </summary>
		public T Target
		{
			get { return _target.Value; }
			set { _target.Value = value; }
		}

#if IOS
		private Action<Webhook, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the webhook is updated.
		/// </summary>
		public event Action<Webhook, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the webhook is updated.
		/// </summary>
		public event Action<Webhook, IEnumerable<string>> Updated;
#endif

		/// <summary>
		/// Creates a new instance of the <see cref="Webhook{T}"/> object and registers a webhook with Trello.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="description"></param>
		/// <param name="callBackUrl"></param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Webhook(T target, string callBackUrl, string description = null, TrelloAuthorization auth = null)
		{
			_context = new WebhookContext<T>(auth);
			Id = _context.Create(target, description, callBackUrl);

			_callBackUrl = new Field<string>(_context, nameof(CallBackUrl));
			_callBackUrl.AddRule(UriRule.Instance);
			_description = new Field<string>(_context, nameof(Description));
			_isActive = new Field<bool?>(_context, nameof(IsActive));
			_isActive.AddRule(NullableHasValueRule<bool>.Instance);
			_target = new Field<T>(_context, nameof(Target));
			_target.AddRule(NotNullRule<T>.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		/// <summary>
		/// Creates a new instance of the <see cref="Webhook{T}"/> object for a webhook which has already been registered with Trello.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Webhook(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new WebhookContext<T>(Id, auth);
			_context.Synchronized += Synchronized;

			_callBackUrl = new Field<string>(_context, nameof(CallBackUrl));
			_callBackUrl.AddRule(UriRule.Instance);
			_description = new Field<string>(_context, nameof(Description));
			_isActive = new Field<bool?>(_context, nameof(IsActive));
			_isActive.AddRule(NullableHasValueRule<bool>.Instance);
			_target = new Field<T>(_context, nameof(Target));
			_target.AddRule(NotNullRule<T>.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Webhook(IJsonWebhook json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Deletes the webhook.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the webhook to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			handler?.Invoke(this, properties);
		}
	}
}