using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CardContext : SynchronizationContext<IJsonCard>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Card.Fields MemberFields;

		private bool _deleted;

		public AttachmentCollection Attachments { get; }
		public BadgesContext BadgesContext { get; }
		public CheckListCollection CheckLists { get; }
		public CommentCollection Comments { get; }
		public CardLabelCollection Labels { get; }
		public MemberCollection Members { get; }
		public ReadOnlyPowerUpDataCollection PowerUpData { get; }
		public CardStickerCollection Stickers { get; }
		public ReadOnlyMemberCollection VotingMembers { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		protected override bool IsDataComplete => !Data.Name.IsNullOrWhiteSpace();

		static CardContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Card.Fields.Badges |
			               Card.Fields.Board |
			               Card.Fields.DateLastActivity |
			               Card.Fields.Description |
			               Card.Fields.Due |
			               Card.Fields.IsArchived |
			               Card.Fields.IsComplete |
			               Card.Fields.IsSubscribed |
			               Card.Fields.List |
			               Card.Fields.ManualCoverAttachment |
			               Card.Fields.Name |
			               Card.Fields.Position |
			               Card.Fields.ShortId |
			               Card.Fields.ShortUrl |
			               Card.Fields.Url;
			Properties = new Dictionary<string, Property<IJsonCard>>
				{
					{
						nameof(Card.Board),
						new Property<IJsonCard, Board>((d, a) => d.Board?.GetFromCache<Board>(a),
						                               (d, o) => d.Board = o?.Json)
					},
					{
						nameof(Card.CustomFields),
						new Property<IJsonCard, List<IJsonCustomField>>((d, a) => d.CustomFields, (d, o) => d.CustomFields = o)
					},
					{
						nameof(Card.Description),
						new Property<IJsonCard, string>((d, a) => d.Desc, (d, o) => d.Desc = o)
					},
					{
						nameof(Card.DueDate),
						new Property<IJsonCard, DateTime?>((d, a) => d.Due?.Decode(), (d, o) =>
							{
								d.Due = o?.Encode();
								d.ForceDueDate = o == null;
							})
					},
					{
						nameof(Card.Id),
						new Property<IJsonCard, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Card.IsArchived),
						new Property<IJsonCard, bool?>((d, a) => d.Closed, (d, o) => d.Closed = o)
					},
					{
						nameof(Card.IsComplete),
						new Property<IJsonCard, bool?>((d, a) => d.DueComplete, (d, o) => d.DueComplete = o)
					},
					{
						nameof(Card.IsSubscribed),
						new Property<IJsonCard, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)
					},
					{
						nameof(Card.Labels),
						new Property<IJsonCard, List<IJsonLabel>>((d, a) => d.Labels, (d, o) => d.Labels = o)
					},
					{
						nameof(Card.LastActivity),
						new Property<IJsonCard, DateTime?>((d, a) => d.DateLastActivity, (d, o) => d.DateLastActivity = o)
					},
					{
						nameof(Card.List),
						new Property<IJsonCard, List>((d, a) => d.List?.GetFromCache<List>(a),
						                              (d, o) => d.List = o?.Json)
					},
					{
						nameof(Card.Name),
						new Property<IJsonCard, string>((d, a) => d.Name, (d, o) => d.Name = o)
					},
					{
						nameof(Card.Position),
						new Property<IJsonCard, Position>((d, a) => Position.GetPosition(d.Pos),
						                                  (d, o) => d.Pos = Position.GetJson(o))
					},
					{
						nameof(Card.ShortId),
						new Property<IJsonCard, int?>((d, a) => d.IdShort, (d, o) => d.IdShort = o)
					},
					{
						nameof(Card.ShortUrl),
						new Property<IJsonCard, string>((d, a) => d.ShortUrl, (d, o) => d.ShortUrl = o)
					},
					{
						nameof(Card.Url),
						new Property<IJsonCard, string>((d, a) => d.Url, (d, o) => d.Url = o)
					},
				};
		}
		public CardContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			Attachments = new AttachmentCollection(() => Data.Id, auth);
			BadgesContext = new BadgesContext(Auth);
			BadgesContext.SynchronizeRequested += ct => Synchronize(ct);
			Data.Badges = BadgesContext.Data;
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(Card.Fields)).Cast<Card.Fields>().ToList();
				var availableFields = (Card.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & (~MemberFields);
				if (parameterFields.HasFlag(Card.Fields.Attachments))
					Parameters["attachments"] = "true";
				if (parameterFields.HasFlag(Card.Fields.CustomFields))
					Parameters["customFieldItems"] = "true";
			}
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_Delete,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}
		public override async Task Expire(CancellationToken ct)
		{
			await base.Expire(ct);
		}

		protected override async Task<IJsonCard> GetData(CancellationToken ct)
		{
			try
			{
				Dictionary<string, object> parameters;
				lock (Parameters)
				{
					parameters = new Dictionary<string, object>(Parameters);
				}
				var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Refresh, 
				                                     new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonCard>(Auth, endpoint, ct, parameters);

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
		protected override async Task SubmitData(IJsonCard json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_Update,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			Merge(newData);
		}
		protected override IEnumerable<string> MergeDependencies(IJsonCard json)
		{
			if (json.Attachments != null)
				Attachments.Update(json.Attachments.Select(a => a.TryGetFromCache<Attachment>() ?? new Attachment(a, Data.Id, Auth)));

			return BadgesContext.Merge(json.Badges);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}