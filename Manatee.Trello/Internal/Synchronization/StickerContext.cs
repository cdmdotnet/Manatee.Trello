using System.Collections.Generic;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class StickerContext : SynchronizationContext<IJsonSticker>
	{
		private readonly string _ownerId;
		private bool _deleted;

		static StickerContext()
		{
			_properties = new Dictionary<string, Property<IJsonSticker>>
				{
					{"Id", new Property<IJsonSticker, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Left", new Property<IJsonSticker, double?>((d, a) => d.Left, (d, o) => d.Left = o)},
					{"Name", new Property<IJsonSticker, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Previews", new Property<IJsonSticker, List<IJsonImagePreview>>((d, a) => d.Previews, (d, o) => d.Previews = o)},
					{"Rotation", new Property<IJsonSticker, int?>((d, a) => d.Rotation, (d, o) => d.Rotation = o)},
					{"Top", new Property<IJsonSticker, double?>((d, a) => d.Top, (d, o) => d.Top = o)},
					{"ImageUrl", new Property<IJsonSticker, string>((d, a) => d.Url, (d, o) => d.Url = o)},
					{"ZIndex", new Property<IJsonSticker, int?>((d, a) => d.ZIndex, (d, o) => d.ZIndex = o)},
				};
		}
		public StickerContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		public async Task Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Sticker_Write_Delete, new Dictionary<string, object> {{"_cardId", _ownerId}, {"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}
		protected override async Task SubmitData(IJsonSticker json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Sticker_Write_Update, new Dictionary<string, object> {{"_cardId", _ownerId}, {"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}