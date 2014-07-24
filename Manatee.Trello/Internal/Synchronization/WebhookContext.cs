﻿/***************************************************************************************

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
 
	File Name:		WebhookContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		WebhookContext
	Purpose:		Provides a data context for webhooks.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class WebhookContext<T> : SynchronizationContext<IJsonWebhook>
		where T : class, ICanWebhook
	{
		private bool _deleted;

		static WebhookContext()
		{
			_properties = new Dictionary<string, Property<IJsonWebhook>>
				{
					{"CallbackUrl", new Property<IJsonWebhook, string>(d => d.CallbackUrl, (d, o) => d.CallbackUrl = o)},
					{"Description", new Property<IJsonWebhook, string>(d => d.Description, (d, o) => d.Description = o)},
					{"Id", new Property<IJsonWebhook, string>(d => d.Id, (d, o) => d.Id = o)},
					{"IsActive", new Property<IJsonWebhook, bool?>(d => d.Active, (d, o) => d.Active = o)},
					{
						"Target", new Property<IJsonWebhook, T>(d => d.IdModel == null ? null : TrelloConfiguration.Cache.Find<T>(b => b.Id == d.IdModel) ?? BuildModel(d.IdModel),
						                                        (d, o) => d.IdModel = o == null ? null : o.Id)
					},
				};
		}
		public WebhookContext() {}
		public WebhookContext(string id)
		{
			Data.Id = id;
		}

		public string Create(ICanWebhook target, string description, string callBackUrl)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonWebhook>();
			json.IdModel = target.Id;
			json.Description = description;
			json.CallbackUrl = callBackUrl;

			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Write_Entity);
			var data = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			Merge(data);
			return data.Id;
		}
		public void Delete()
		{
			if (_deleted) return;

			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);

			_deleted = true;
		}

		protected override IJsonWebhook GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonWebhook>(TrelloAuthorization.Default, endpoint);

			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}

		private static T BuildModel(string id)
		{
			return (T) Activator.CreateInstance(typeof (T), id);
		}
	}
}