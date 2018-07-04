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

		public static Dictionary<string, object> CurrentParameters
		{
			get
			{
				lock (Parameters)
				{
					if (!Parameters.Any())
						GenerateParameters();

					return new Dictionary<string, object>(Parameters);
				}
			}
		}

		private bool _deleted;

		public ReadOnlyActionCollection Actions { get; }
		public AttachmentCollection Attachments { get; }
		public BadgesContext BadgesContext { get; }
		public CheckListCollection CheckLists { get; }
		public CommentCollection Comments { get; }
		public ReadOnlyCustomFieldCollection CustomFields { get; }
		public CardLabelCollection Labels { get; }
		public MemberCollection Members { get; }
		public ReadOnlyPowerUpDataCollection PowerUpData { get; }
		public CardStickerCollection Stickers { get; }
		public ReadOnlyMemberCollection VotingMembers { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static CardContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Card.Fields.Badges |
			               Card.Fields.DateLastActivity |
			               Card.Fields.Description |
			               Card.Fields.Due |
			               Card.Fields.IsArchived |
			               Card.Fields.IsComplete |
			               Card.Fields.IsSubscribed |
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
						new Property<IJsonCard, Board>((d, a) => d.Board?.GetFromCache<Board, IJsonBoard>(a),
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
						new Property<IJsonCard, List>((d, a) => d.List?.GetFromCache<List, IJsonList>(a),
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
					{
						nameof(IJsonCard.ValidForMerge),
						new Property<IJsonCard, bool>((d, a) => d.ValidForMerge, (d, o) => d.ValidForMerge = o, true)
					},
				};
		}
		public CardContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;

			Actions = new ReadOnlyActionCollection(typeof(Card), () => Data.Id, auth);
			Attachments = new AttachmentCollection(() => Data.Id, auth);
			CheckLists = new CheckListCollection(() => Data.Id, auth);
			CheckLists.Refreshed += (s, e) => OnMerged(new[] {nameof(CheckLists)});
			Comments = new CommentCollection(() => Data.Id, auth);
			CustomFields = new ReadOnlyCustomFieldCollection(() => Data.Id, auth);
			Labels = new CardLabelCollection(this, auth);
			Members = new MemberCollection(() => Data.Id, auth);
			PowerUpData = new ReadOnlyPowerUpDataCollection(EntityRequestType.Card_Read_PowerUpData, () => Data.Id, auth);
			Stickers = new CardStickerCollection(() => Data.Id, auth);
			VotingMembers = new ReadOnlyMemberCollection(EntityRequestType.Card_Read_MembersVoted, () => Data.Id, auth);

			BadgesContext = new BadgesContext(Auth);

			Data.Badges = BadgesContext.Data;
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
			}
		}

		private static void GenerateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(Card.Fields)).Cast<Card.Fields>().ToList();
				var availableFields = (Card.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Card.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & Card.DownloadedFields & (~MemberFields);

				if (parameterFields.HasFlag(Card.Fields.Actions))
				{
					Parameters["actions"] = "all";
					Parameters["actions_format"] = "list";
				}
				if (parameterFields.HasFlag(Card.Fields.Attachments))
				{
					Parameters["attachments"] = "true";
					Parameters["attachment_fields"] = AttachmentContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Card.Fields.CustomFields))
					Parameters["customFieldItems"] = "true";
				if (parameterFields.HasFlag(Card.Fields.Checklists))
				{
					Parameters["checklists"] = "all";
					Parameters["checklist_fields"] = CheckListContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Card.Fields.Comments))
				{
					Parameters["actions"] = "commentCard";
					Parameters["actions_format"] = "list";
				}
				if (parameterFields.HasFlag(Card.Fields.Members))
				{
					Parameters["members"] = "true";
					Parameters["member_fields"] = MemberContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Card.Fields.Stickers))
					Parameters["stickers"] = "true";
				if (parameterFields.HasFlag(Card.Fields.VotingMembers))
				{
					Parameters["membersVoted"] = "true";
					Parameters["membersVoted_fields"] = MemberContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Card.Fields.Board))
				{
					Parameters["board"] = "true";
					Parameters["board_fields"] = BoardContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Card.Fields.List))
				{
					Parameters["list"] = "true";
					Parameters["list_fields"] = ListContext.CurrentParameters["fields"];
				}
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
			RaiseDeleted();
		}

		protected override async Task<IJsonCard> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Refresh, 
				                                     new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonCard>(Auth, endpoint, ct, CurrentParameters);

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
		protected override IEnumerable<string> MergeDependencies(IJsonCard json, bool overwrite)
		{
			var properties = BadgesContext.Merge(json.Badges, overwrite)
			                              .Select(p => $"{nameof(Card.Badges)}.{p}")
			                              .ToList();

			if (json.Actions != null)
			{
				Actions.Update(json.Actions.Select(a => a.GetFromCache<Action, IJsonAction>(Auth, overwrite)));
				properties.Add(nameof(Card.Actions));
			}
			if (json.Attachments != null)
			{
				Attachments.Update(json.Attachments.Select(a => a.TryGetFromCache<Attachment, IJsonAttachment>(overwrite) ?? new Attachment(a, Data.Id, Auth)));
				properties.Add(nameof(Card.Attachments));
			}
			if (json.CheckLists != null)
			{
				CheckLists.Update(json.CheckLists.Select(a => a.GetFromCache<CheckList, IJsonCheckList>(Auth, overwrite)));
				properties.Add(nameof(Card.CheckLists));
			}
			if (json.Comments != null)
			{
				Comments.Update(json.Comments.Select(a => a.GetFromCache<Action, IJsonAction>(Auth, overwrite)));
				properties.Add(nameof(Card.Comments));
			}
			if (json.CustomFields != null)
			{
				CustomFields.Update(json.CustomFields.Select(a => a.GetFromCache<CustomField, IJsonCustomField>(Auth, overwrite, Data.Id)));
				properties.Add(nameof(Card.CustomFields));
			}
			if (json.Labels != null)
			{
				Labels.Update(json.Labels.Select(a => a.GetFromCache<Label, IJsonLabel>(Auth, overwrite)));
				properties.Add(nameof(Card.Labels));
			}
			if (json.Members != null)
			{
				Members.Update(json.Members.Select(a => a.GetFromCache<Member, IJsonMember>(Auth, overwrite)));
				properties.Add(nameof(Card.Members));
			}
			if (json.PowerUpData != null)
			{
				PowerUpData.Update(json.PowerUpData.Select(a => a.GetFromCache<PowerUpData, IJsonPowerUpData>(Auth, overwrite)));
				properties.Add(nameof(Card.PowerUpData));
			}
			if (json.Stickers != null)
			{
				Stickers.Update(json.Stickers.Select(a => a.TryGetFromCache<Sticker, IJsonSticker>(overwrite) ?? new Sticker(a, Data.Id, Auth)));
				properties.Add(nameof(Card.Stickers));
			}
			if (json.MembersVoted != null)
			{
				VotingMembers.Update(json.MembersVoted.Select(a => a.GetFromCache<Member, IJsonMember>(Auth, overwrite)));
				properties.Add(nameof(Card.VotingMembers));
			}

			return properties;
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}