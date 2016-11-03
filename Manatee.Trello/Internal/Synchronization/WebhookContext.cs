﻿using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
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
					{"CallbackUrl", new Property<IJsonWebhook, string>((d, a) => d.CallbackUrl, (d, o) => d.CallbackUrl = o)},
					{"Description", new Property<IJsonWebhook, string>((d, a) => d.Description, (d, o) => d.Description = o)},
					{"Id", new Property<IJsonWebhook, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsActive", new Property<IJsonWebhook, bool?>((d, a) => d.Active, (d, o) => d.Active = o)},
					{
						"Target", new Property<IJsonWebhook, T>((d, a) => d.IdModel == null ? null : TrelloConfiguration.Cache.Find<T>(b => b.Id == d.IdModel) ?? BuildModel(d.IdModel),
						                                        (d, o) => d.IdModel = o?.Id)
					},
				};
		}
		public WebhookContext(TrelloAuthorization auth)
			: base(auth)
		{
		}
		public WebhookContext(string id, TrelloAuthorization auth)
			: this(auth)
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
			var data = JsonRepository.Execute(Auth, endpoint, json);

			Merge(data);
			return data.Id;
		}
		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override IJsonWebhook GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonWebhook>(Auth, endpoint);
				MarkInitialized();

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override void SubmitData(IJsonWebhook json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}

		private static T BuildModel(string id)
		{
			return (T) Activator.CreateInstance(typeof (T), id, null);
		}
	}
}