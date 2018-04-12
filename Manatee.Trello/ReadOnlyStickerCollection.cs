using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of attachments.
	/// </summary>
	public class ReadOnlyStickerCollection : ReadOnlyCollection<ISticker>
	{
		internal ReadOnlyStickerCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		public sealed override async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Stickers, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonSticker>>(Auth, endpoint, ct);

			Items.Clear();
			Items.AddRange(newData.Select(ja =>
				{
					var attachment = TrelloConfiguration.Cache.Find<Sticker>(a => a.Id == ja.Id) ?? new Sticker(ja, OwnerId, Auth);
					attachment.Json = ja;
					return attachment;
				}));
		}
	}
}