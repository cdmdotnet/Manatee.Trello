using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	public class ReadOnlyAttachmentCollection : ReadOnlyCollection<Attachment>
	{
		internal ReadOnlyAttachmentCollection(string ownerId)
			: base(ownerId) {}

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Attachments, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonAttachment>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(ja => TrelloConfiguration.Cache.Find<Attachment>(a => a.Id == ja.Id) ?? new Attachment(ja, OwnerId, true)));
		}
	}

	public class AttachmentCollection : ReadOnlyAttachmentCollection
	{
		internal AttachmentCollection(string ownerId)
			: base(ownerId) {}

		public Attachment Add(string name, string url)
		{
			var parameters = new Dictionary<string, object>
				{
					{"name", name},
					{"url", url},
				};
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddAttachment, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<IJsonAttachment>(TrelloAuthorization.Default, endpoint, parameters);

			return new Attachment(newData, OwnerId, true);
		}
		public Attachment Add(string name, byte[] data)
		{
			var parameters = new Dictionary<string, object> {{RestFile.ParameterKey, new RestFile {ContentBytes = data, FileName = name}}};
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddAttachment, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<IJsonAttachment>(TrelloAuthorization.Default, endpoint, parameters);

			return new Attachment(newData, OwnerId, true);
		}
	}
}