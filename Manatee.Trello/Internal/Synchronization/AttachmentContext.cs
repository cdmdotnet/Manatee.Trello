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
 
	File Name:		AttachmentContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		AttachmentContext
	Purpose:		Provides a data context for an attachment to a card.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class AttachmentContext : SynchronizationContext<IJsonAttachment>
	{
		private readonly string _ownerId;

		static AttachmentContext()
		{
			_properties = new Dictionary<string, Property<IJsonAttachment>>
				{
					{"Bytes", new Property<IJsonAttachment, int?>(d => d.Bytes, (d, o) => d.Bytes = o)},
					{"Date", new Property<IJsonAttachment, DateTime?>(d => d.Date, (d, o) => d.Date = o)},
					{
						"Member", new Property<IJsonAttachment, Member>(d => d.Member == null ? null : d.Member.GetFromCache<Member>(),
						                                                (d, o) => d.Member = o != null ? o.Json : null)
					},
					{"Id", new Property<IJsonAttachment, string>(d => d.Id, (d, o) => d.Id = o)},
					{"IsUpload", new Property<IJsonAttachment, bool?>(d => d.IsUpload, (d, o) => d.IsUpload = o)},
					{"MimeType", new Property<IJsonAttachment, string>(d => d.MimeType, (d, o) => d.MimeType = o)},
					{"Name", new Property<IJsonAttachment, string>(d => d.Name, (d, o) => d.Name = o)},
					{"Previews", new Property<IJsonAttachment, List<IJsonAttachmentPreview>>(d => d.Previews, (d, o) => d.Previews = o)},
					{"Url", new Property<IJsonAttachment, string>(d => d.Url, (d, o) => d.Url = o)},
				};
		}
		public AttachmentContext(string id, string ownerId)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		public void Delete()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Attachment_Write_Delete, new Dictionary<string, object> {{"_cardId", _ownerId}, {"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);
		}
	}
}