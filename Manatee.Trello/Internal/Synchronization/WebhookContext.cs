using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
			Properties = new Dictionary<string, Property<IJsonWebhook>>
				{
					{
						nameof(Webhook<T>.CallBackUrl),
						new Property<IJsonWebhook, string>((d, a) => d.CallbackUrl, (d, o) => d.CallbackUrl = o)
					},
					{
						nameof(Webhook<T>.Description),
						new Property<IJsonWebhook, string>((d, a) => d.Description, (d, o) => d.Description = o)
					},
					{
						nameof(Webhook<T>.Id),
						new Property<IJsonWebhook, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Webhook<T>.IsActive),
						new Property<IJsonWebhook, bool?>((d, a) => d.Active, (d, o) => d.Active = o)
					},
					{
						nameof(Webhook<T>.Target),
						new Property<IJsonWebhook, T>(
							(d, a) => d.IdModel == null
								          ? null
								          : TrelloConfiguration.Cache.Find<T>(b => b.Id == d.IdModel) ?? BuildModel(d.IdModel),
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

		public async Task<string> Create(ICanWebhook target, string description, string callBackUrl, CancellationToken ct)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonWebhook>();
			json.IdModel = target.Id;
			json.Description = description;
			json.CallbackUrl = callBackUrl;

			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Write_Entity);
			var data = await JsonRepository.Execute(Auth, endpoint, json, ct);

			Merge(data);
			return data.Id;
		}
		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Write_Delete, 
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}

		protected override async Task<IJsonWebhook> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Read_Refresh,
				                                     new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonWebhook>(Auth, endpoint, ct);
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
		protected override async Task SubmitData(IJsonWebhook json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Webhook_Write_Update,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
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