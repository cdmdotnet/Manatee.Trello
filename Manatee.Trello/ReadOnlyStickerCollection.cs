using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Eventing;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of attachments.
	/// </summary>
	public class ReadOnlyStickerCollection : ReadOnlyCollection<ISticker>,
	                                         IHandle<EntityDeletedEvent<IJsonSticker>>
	{
		internal ReadOnlyStickerCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			EventAggregator.Subscribe(this);
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var allParameters = AdditionalParameters.Concat(StickerContext.CurrentParameters)
			                                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Stickers, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonSticker>>(Auth, endpoint, ct, allParameters);

			Items.Clear();
			EventAggregator.Unsubscribe(this);
			Items.AddRange(newData.Select(ja =>
				{
					var attachment = TrelloConfiguration.Cache.Find<Sticker>(ja.Id) ?? new Sticker(ja, OwnerId, Auth);
					attachment.Json = ja;
					return attachment;
				}));
			EventAggregator.Subscribe(this);
		}

		void IHandle<EntityDeletedEvent<IJsonSticker>>.Handle(EntityDeletedEvent<IJsonSticker> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}