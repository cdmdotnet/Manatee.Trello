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
		private bool _deleted;

		static AttachmentContext()
		{
			_properties = new Dictionary<string, Property<IJsonAttachment>>
				{
					{"Bytes", new Property<IJsonAttachment, int?>((d, a) => d.Bytes, (d, o) => d.Bytes = o)},
					{"Date", new Property<IJsonAttachment, DateTime?>((d, a) => d.Date, (d, o) => d.Date = o)},
					{
						"Member", new Property<IJsonAttachment, Member>((d, a) => d.Member?.GetFromCache<Member>(a),
						                                                (d, o) => d.Member = o?.Json)
					},
					{"Id", new Property<IJsonAttachment, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsUpload", new Property<IJsonAttachment, bool?>((d, a) => d.IsUpload, (d, o) => d.IsUpload = o)},
					{"MimeType", new Property<IJsonAttachment, string>((d, a) => d.MimeType, (d, o) => d.MimeType = o)},
					{"Name", new Property<IJsonAttachment, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Url", new Property<IJsonAttachment, string>((d, a) => d.Url, (d, o) => d.Url = o)},
				};
		}

		public AttachmentContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		protected override void SubmitData(IJsonAttachment json)
		{
			// This may make a call to get the card, but it can't be avoided.  We need its ID.
			var endpoint = EndpointFactory.Build(EntityRequestType.Attachment_Write_Update,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_cardId", _ownerId},
					                                     {"_id", Data.Id},
				                                     });
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Attachment_Write_Delete,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_cardId", _ownerId},
					                                     {"_id", Data.Id}
				                                     });
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}