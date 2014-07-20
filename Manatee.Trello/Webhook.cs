/***************************************************************************************

	Copyright 2014 Greg Dennis

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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class Webhook<T>
		where T : class, ICanWebhook
	{
		private readonly Field<string> _callBackUrl;
		private readonly Field<string> _description;
		private readonly Field<bool?> _isActive;
		private readonly Field<T> _target;
		private readonly WebhookContext<T> _context;

		private bool _deleted;

		public string CallBackUrl
		{
			get { return _callBackUrl.Value; }
			set { _callBackUrl.Value = value; }
		}
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		public string Id { get; private set; }
		public bool? IsActive
		{
			get { return _isActive.Value; }
			set { _isActive.Value = value; }
		}
		public T Target
		{
			get { return _target.Value; }
			set { _target.Value = value; }
		}

		public event Action<Webhook<T>, IEnumerable<string>> Updated;

		public Webhook(T target, string description, string callBackUrl)
		{
			_context = new WebhookContext<T>();
			Id = _context.Create(target, description, callBackUrl);

			_callBackUrl = new Field<string>(_context, () => CallBackUrl);
			_callBackUrl.AddRule(UriRule.Instance);
			_description = new Field<string>(_context, () => Description);
			_isActive = new Field<bool?>(_context, () => IsActive);
			_target = new Field<T>(_context, () => Target);
			_target.AddRule(NotNullRule<T>.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		public Webhook(string id)
		{
			Id = id;
			_context = new WebhookContext<T>(Id);
			_context.Synchronized += Synchronized;

			_callBackUrl = new Field<string>(_context, () => CallBackUrl);
			_description = new Field<string>(_context, () => Description);
			_isActive = new Field<bool?>(_context, () => IsActive);
			_isActive.AddRule(NullableHasValueRule<bool>.Instance);
			_target = new Field<T>(_context, () => Target);
			_target.AddRule(NotNullRule<T>.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Webhook(IJsonWebhook json)
			: this(json.Id)
		{
			_context.Merge(json);
		}

		public void Delete()
		{
			if (_deleted) return;

			_context.Delete();
			_deleted = true;
			TrelloConfiguration.Cache.Remove(this);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			if (handler != null)
				handler(this, properties);
		}
	}
}