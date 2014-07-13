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
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
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
					{"Bytes", new Property<IJsonAttachment>(d => d.Bytes, (d, o) => d.Bytes = (int?) o)},
					{"Date", new Property<IJsonAttachment>(d => d.Date, (d, o) => d.Date = (DateTime?) o)},
					{
						"Member", new Property<IJsonAttachment>(d => d.Member != null ? TrelloConfiguration.Cache.Find<Member>(b => b.Id == d.Member.Id) ?? new Member(d.Member) : null,
						                                        (d, o) => d.Member = o != null ? ((Member) o).Json : null)
					},
					{"IsUpload", new Property<IJsonAttachment>(d => d.IsUpload, (d, o) => d.IsUpload = (bool?) o)},
					{"MimeType", new Property<IJsonAttachment>(d => d.MimeType, (d, o) => d.MimeType = (string) o)},
					{"Name", new Property<IJsonAttachment>(d => d.Name, (d, o) => d.Name = (string) o)},
					{"Previews", new Property<IJsonAttachment>(d => d.Previews, (d, o) => d.Previews = (List<IJsonAttachmentPreview>) o)},
					{"Url", new Property<IJsonAttachment>(d => d.Url, (d, o) => d.Url = (string) o)},
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