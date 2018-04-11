using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class AttachmentContext : SynchronizationContext<IJsonAttachment>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Attachment.Fields MemberFields;

		private readonly string _ownerId;
		private bool _deleted;

		public ReadOnlyAttachmentPreviewCollection Previews { get; }

		static AttachmentContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Attachment.Fields.Bytes |
			               Attachment.Fields.Date |
			               Attachment.Fields.IsUpload |
			               Attachment.Fields.Member |
			               Attachment.Fields.MimeType |
			               Attachment.Fields.Name |
			               Attachment.Fields.Url |
			               Attachment.Fields.EdgeColor |
						   Attachment.Fields.Position;
			Properties = new Dictionary<string, Property<IJsonAttachment>>
				{
					{
						nameof(Attachment.Bytes),
						new Property<IJsonAttachment, int?>((d, a) => d.Bytes, (d, o) => d.Bytes = o)
					},
					{
						nameof(Attachment.Date),
						new Property<IJsonAttachment, DateTime?>((d, a) => d.Date, (d, o) => d.Date = o)
					},
					{
						nameof(Attachment.Member),
						new Property<IJsonAttachment, Member>((d, a) => d.Member?.GetFromCache<Member>(a),
						                                      (d, o) => d.Member = o?.Json)
					},
					{
						nameof(Attachment.Id),
						new Property<IJsonAttachment, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Attachment.IsUpload),
						new Property<IJsonAttachment, bool?>((d, a) => d.IsUpload, (d, o) => d.IsUpload = o)
					},
					{
						nameof(Attachment.MimeType),
						new Property<IJsonAttachment, string>((d, a) => d.MimeType, (d, o) => d.MimeType = o)
					},
					{
						nameof(Attachment.Name),
						new Property<IJsonAttachment, string>((d, a) => d.Name, (d, o) => d.Name = o)
					},
					{
						nameof(Attachment.Url),
						new Property<IJsonAttachment, string>((d, a) => d.Url, (d, o) => d.Url = o)
					},
					{
						nameof(Attachment.Position),
						new Property<IJsonAttachment, Position>((d, a) => Position.GetPosition(d.Pos),
						                                        (d, o) => d.Pos = Position.GetJson(o))
					},
					{
						nameof(Attachment.EdgeColor),
						new Property<IJsonAttachment, WebColor>(
							(d, a) => d.EdgeColor.IsNullOrWhiteSpace() ? null : new WebColor(d.EdgeColor),
							(d, o) => d.EdgeColor = o?.ToString())
					},
				};
		}

		public AttachmentContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;

			Previews = new ReadOnlyAttachmentPreviewCollection(this, Auth);
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(Attachment.Fields)).Cast<Attachment.Fields>().ToList();
				var availableFields = (Attachment.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Attachment.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & Attachment.DownloadedFields & (~MemberFields);
				if (parameterFields.HasFlag(Attachment.Fields.Previews))
					Parameters["previews"] = "true";
			}
		}

		protected override async Task<IJsonAttachment> GetData(CancellationToken ct)
		{
			try
			{
				Dictionary<string, object> parameters;
				lock (Parameters)
				{
					parameters = new Dictionary<string, object>(Parameters);
				}
				var endpoint = EndpointFactory.Build(EntityRequestType.Attachment_Read_Refresh,
				                                     new Dictionary<string, object> { { "_cardId", _ownerId }, { "_id", Data.Id } });
				var newData = await JsonRepository.Execute<IJsonAttachment>(Auth, endpoint, ct, parameters);

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

		protected override async Task SubmitData(IJsonAttachment json, CancellationToken ct)
		{
			// This may make a call to get the card, but it can't be avoided.  We need its ID.
			var endpoint = EndpointFactory.Build(EntityRequestType.Attachment_Write_Update,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_cardId", _ownerId},
					                                     {"_id", Data.Id},
				                                     });
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Attachment_Write_Delete,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_cardId", _ownerId},
					                                     {"_id", Data.Id}
				                                     });
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}

		protected override IEnumerable<string> MergeDependencies(IJsonAttachment json)
		{
			var properties = new List<string>();

			if (json.Previews != null)
			{
				Previews.Update(json.Previews.Select(a => a.GetFromCache<ImagePreview>(Auth)));
				properties.Add(nameof(Board.Actions));
			}

			return properties;
		}

		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}