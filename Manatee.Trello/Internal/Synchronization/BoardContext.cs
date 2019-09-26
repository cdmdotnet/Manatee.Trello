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
	internal class BoardContext : DeletableSynchronizationContext<IJsonBoard>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Board.Fields MemberFields;

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

		public ReadOnlyActionCollection Actions { get; }
		public ReadOnlyCardCollection Cards { get; }
		public CustomFieldDefinitionCollection CustomFields { get; }
		public BoardLabelCollection Labels { get; }
		public ListCollection Lists { get; }
		public ReadOnlyMemberCollection Members { get; }
		public BoardMembershipCollection Memberships { get; }
		public PowerUpCollection PowerUps { get; }
		public ReadOnlyPowerUpDataCollection PowerUpData { get; }
		public BoardPreferencesContext BoardPreferencesContext { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static BoardContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Board.Fields.Closed |
			               Board.Fields.Pinned |
			               Board.Fields.Description |
			               Board.Fields.Starred |
			               Board.Fields.Preferencess |
			               Board.Fields.IsSubscribed |
			               Board.Fields.LastActivityDate |
			               Board.Fields.LastViewDate |
			               Board.Fields.Name |
			               Board.Fields.ShortLink |
			               Board.Fields.ShortUrl |
			               Board.Fields.Url;
			Properties = new Dictionary<string, Property<IJsonBoard>>
				{
					{
						nameof(Board.Description),
						new Property<IJsonBoard, string>((d, a) => d.Desc, (d, o) => d.Desc = o)
					},
					{
						nameof(Board.Id),
						new Property<IJsonBoard, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Board.IsClosed),
						new Property<IJsonBoard, bool?>((d, a) => d.Closed, (d, o) => d.Closed = o)
					},
					{
						nameof(Board.IsSubscribed),
						new Property<IJsonBoard, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)
					},
					{
						nameof(Board.Name),
						new Property<IJsonBoard, string>((d, a) => d.Name, (d, o) => d.Name = o)
					},
					{
						nameof(Board.Organization),
						new Property<IJsonBoard, Organization>((d, a) => d.Organization?.GetFromCache<Organization, IJsonOrganization>(a),
															   (d, o) => d.Organization = o?.Json)
					},
					{
						nameof(Board.Preferences),
						new Property<IJsonBoard, IJsonBoardPreferences>((d, a) => d.Prefs, (d, o) => d.Prefs = o)
					},
					{
						nameof(Board.Url),
						new Property<IJsonBoard, string>((d, a) => d.Url, (d, o) => d.Url = o)
					},
					{
						nameof(Board.IsPinned),
						new Property<IJsonBoard, bool?>((d, a) => d.Pinned, (d, o) => d.Pinned = o)
					},
					{
						nameof(Board.IsStarred),
						new Property<IJsonBoard, bool?>((d, a) => d.Starred, (d, o) => d.Starred = o)
					},
					{
						nameof(Board.LastViewed),
						new Property<IJsonBoard, DateTime?>((d, a) => d.DateLastView, (d, o) => d.DateLastView = o)
					},
					{
						nameof(Board.LastActivity),
						new Property<IJsonBoard, DateTime?>((d, a) => d.DateLastActivity, (d, o) => d.DateLastActivity = o)
					},
					{
						nameof(Board.ShortUrl),
						new Property<IJsonBoard, string>((d, a) => d.ShortUrl, (d, o) => d.ShortUrl = o)
					},
					{
						nameof(Board.ShortLink),
						new Property<IJsonBoard, string>((d, a) => d.ShortLink, (d, o) => d.ShortLink = o)
					},
					{
						nameof(IJsonBoard.ValidForMerge),
						new Property<IJsonBoard, bool>((d, a) => d.ValidForMerge, (d, o) => d.ValidForMerge = o, true)
					},
				};
		}
		public BoardContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;

			Actions = new ReadOnlyActionCollection(typeof(Board), () => Data.Id, auth);
			Actions.Refreshed += (s, e) => OnMerged(new[] {nameof(Actions) });
			Cards = new ReadOnlyCardCollection(typeof(Board), () => Data.Id, auth);
			Cards.Refreshed += (s, e) => OnMerged(new[] {nameof(Cards) });
			CustomFields = new CustomFieldDefinitionCollection(() => Data.Id, auth);
			CustomFields.Refreshed += (s, e) => OnMerged(new[] {nameof(CustomFields) });
			Labels = new BoardLabelCollection(() => Data.Id, auth);
			Labels.Refreshed += (s, e) => OnMerged(new[] {nameof(Labels) });
			Lists = new ListCollection(() => Data.Id, auth);
			Lists.Refreshed += (s, e) => OnMerged(new[] {nameof(Lists) });
			Members = new ReadOnlyMemberCollection(EntityRequestType.Board_Read_Members, () => Data.Id, auth);
			Members.Refreshed += (s, e) => OnMerged(new[] {nameof(Members) });
			Memberships = new BoardMembershipCollection(() => Data.Id, auth);
			Memberships.Refreshed += (s, e) => OnMerged(new[] {nameof(Memberships) });
			PowerUps = new PowerUpCollection(() => Data.Id, auth);
			PowerUps.Refreshed += (s, e) => OnMerged(new[] {nameof(PowerUps) });
			PowerUpData = new ReadOnlyPowerUpDataCollection(EntityRequestType.Board_Read_PowerUpData, () => Data.Id, auth);
			PowerUpData.Refreshed += (s, e) => OnMerged(new[] { nameof(PowerUpData) });

			BoardPreferencesContext = new BoardPreferencesContext(Auth);
			BoardPreferencesContext.SubmitRequested += ct => HandleSubmitRequested("Preferences", ct);
			Data.Prefs = BoardPreferencesContext.Data;
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
				var flags = Enum.GetValues(typeof(Board.Fields)).Cast<Board.Fields>().ToList();
				var availableFields = (Board.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Board.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				if (!TrelloConfiguration.EnableDeepDownloads) return;

				var parameterFields = availableFields & Board.DownloadedFields & ~MemberFields;
				if (parameterFields.HasFlag(Board.Fields.Actions))
				{
					Parameters["actions"] = "all";
					Parameters["actions_format"] = "list";
				}
				if (parameterFields.HasFlag(Board.Fields.Cards))
				{
					Parameters["cards"] = "open";
					var fields = CardContext.CurrentParameters["fields"];
					if (Board.DownloadedFields.HasFlag(Board.Fields.Lists) || Card.DownloadedFields.HasFlag(Card.Fields.List))
						fields += ",idList";
					Parameters["card_fields"] = fields;
					if (Card.DownloadedFields.HasFlag(Card.Fields.Members))
						Parameters["card_members"] = "true";
					if (Card.DownloadedFields.HasFlag(Card.Fields.Attachments))
						Parameters["card_attachments"] = "true";
					if (Card.DownloadedFields.HasFlag(Card.Fields.Stickers))
						Parameters["card_stickers"] = "true";
					if (Card.DownloadedFields.HasFlag(Card.Fields.CustomFields))
						Parameters["card_customFieldItems"] = "true";
				}
				if (parameterFields.HasFlag(Board.Fields.CustomFields))
					Parameters["customFields"] = "true";
				if (parameterFields.HasFlag(Board.Fields.Labels))
				{
					Parameters["labels"] = "all";
					Parameters["label_fields"] = LabelContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Board.Fields.Lists))
				{
					Parameters["lists"] = "open";
					Parameters["list_fields"] = ListContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Board.Fields.Members))
				{
					Parameters["members"] = "all";
					Parameters["member_fields"] = MemberContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Board.Fields.Memberships))
				{
					Parameters["memberships"] = "all";
					Parameters["memberships_member"] = "true";
					Parameters["membership_member_fields"] = MemberContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Board.Fields.PowerUps))
					Parameters["plugins"] = "enabled";
				if (parameterFields.HasFlag(Board.Fields.PowerUpData))
					Parameters["pluginData"] = "true";
				if (parameterFields.HasFlag(Board.Fields.Organization))
				{
					Parameters["organization"] = "true";
					Parameters["organization_fields"] = OrganizationContext.CurrentParameters["fields"];
				}
			}
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.Board_Read_Refresh,
			                             new Dictionary<string, object> {{"_id", Data.Id}});
		}

		protected override Dictionary<string, object> GetParameters()
		{
			return CurrentParameters;
		}

		protected override Endpoint GetDeleteEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.Board_Write_Delete,
			                             new Dictionary<string, object> {{"_id", Data.Id}});
		}

		protected override async Task SubmitData(IJsonBoard json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_Update,
												 new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			Merge(newData);
			Data.Prefs = BoardPreferencesContext.Data;
		}
		protected override void ApplyDependentChanges(IJsonBoard json)
		{
			Data.Prefs = BoardPreferencesContext.Data;
			if (BoardPreferencesContext.HasChanges)
			{
				json.Prefs = BoardPreferencesContext.GetChanges();
				BoardPreferencesContext.ClearChanges();
			}
		}
		protected override IEnumerable<string> MergeDependencies(IJsonBoard json, bool overwrite)
		{
			var properties = BoardPreferencesContext.Merge(json.Prefs, overwrite)
			                                        .Select(p => $"{nameof(Board.Preferences)}.{p}")
			                                        .ToList();

			if (json.Actions != null)
			{
				Actions.Update(json.Actions.Select(a => a.GetFromCache<Action, IJsonAction>(Auth, overwrite)));
				properties.Add(nameof(Board.Actions));
			}
			if (json.Cards != null)
			{
				Cards.Update(json.Cards.Select(a => a.GetFromCache<Card, IJsonCard>(Auth, overwrite)));
				properties.Add(nameof(Board.Cards));
			}
			if (json.CustomFields != null)
			{
				CustomFields.Update(json.CustomFields.Select(a => a.GetFromCache<CustomFieldDefinition, IJsonCustomFieldDefinition>(Auth, overwrite)));
				properties.Add(nameof(Board.CustomFields));
			}
			if (json.Labels != null)
			{
				Labels.Update(json.Labels.Select(a => a.GetFromCache<Label, IJsonLabel>(Auth, overwrite)));
				properties.Add(nameof(Board.Labels));
			}
			if (json.Lists != null)
			{
				Lists.Update(json.Lists.Select(a => a.GetFromCache<List, IJsonList>(Auth, overwrite)));
				properties.Add(nameof(Board.Lists));
			}
			if (json.Members != null)
			{
				Members.Update(json.Members.Select(a => a.GetFromCache<Member, IJsonMember>(Auth, overwrite)));
				properties.Add(nameof(Board.Members));
			}
			if (json.Memberships != null)
			{
				Memberships.Update(json.Memberships.Select(a => a.TryGetFromCache<BoardMembership, IJsonBoardMembership>(overwrite) ??
																new BoardMembership(a, Data.Id, Auth)));
				properties.Add(nameof(Board.Memberships));
			}
			if (json.PowerUps != null)
			{
				PowerUps.Update(json.PowerUps.Select(a => a.GetFromCache<IPowerUp>(Auth)));
				properties.Add(nameof(Board.PowerUps));
			}
			if (json.PowerUpData != null)
			{
				PowerUpData.Update(json.PowerUpData.Select(a => a.GetFromCache<PowerUpData, IJsonPowerUpData>(Auth, overwrite)));
				properties.Add(nameof(Board.PowerUpData));
			}

			return properties;
		}
	}
}