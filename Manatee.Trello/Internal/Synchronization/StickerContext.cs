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
 
	File Name:		StickerContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		StickerContext
	Purpose:		Provides a data context for an sticker on a card.

***************************************************************************************/

using System.Collections.Generic;
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

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Sticker_Write_Delete, new Dictionary<string, object> {{"_cardId", _ownerId}, {"_id", Data.Id}});
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}
		protected override void SubmitData(IJsonSticker json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Sticker_Write_Update, new Dictionary<string, object> {{"_cardId", _ownerId}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}