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
	public class ReadOnlyAttachmentCollection : ReadOnlyCollection<IAttachment>,
	                                            IHandle<EntityDeletedEvent<IJsonAttachment>>
	{
		internal ReadOnlyAttachmentCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			EventAggregator.Subscribe(this);
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var allParameters = AdditionalParameters.Concat(AttachmentContext.CurrentParameters)
			                                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Attachments, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonAttachment>>(Auth, endpoint, ct, allParameters);

			Items.Clear();
			Items.AddRange(newData.Select(ja =>
				{
					var attachment = TrelloConfiguration.Cache.Find<Attachment>(ja.Id) ?? new Attachment(ja, OwnerId, Auth);
					attachment.Json = ja;
					return attachment;
				}));
		}

		void IHandle<EntityDeletedEvent<IJsonAttachment>>.Handle(EntityDeletedEvent<IJsonAttachment> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}