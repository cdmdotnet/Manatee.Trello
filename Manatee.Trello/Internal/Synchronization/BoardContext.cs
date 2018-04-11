﻿using System;
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
	internal class BoardContext : SynchronizationContext<IJsonBoard>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Board.Fields MemberFields;

		private bool _deleted;

		public ReadOnlyActionCollection Actions { get; }
		public ReadOnlyCardCollection Cards { get; }
		public ReadOnlyCustomFieldDefinitionCollection CustomFields { get; }
		public BoardLabelCollection Labels { get; }
		public ListCollection Lists { get; }
		public ReadOnlyMemberCollection Members { get; }
		public BoardMembershipCollection Memberships { get; }
		public ReadOnlyPowerUpCollection PowerUps { get; }
		public ReadOnlyPowerUpDataCollection PowerUpData { get; }
		public BoardPreferencesContext BoardPreferencesContext { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static BoardContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Board.Fields.Closed |
			               Board.Fields.Organization |
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
						new Property<IJsonBoard, Organization>((d, a) => d.Organization?.GetFromCache<Organization>(a),
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
				};
		}
		public BoardContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;

			Actions = new ReadOnlyActionCollection(typeof(Board), () => Data.Id, auth);
			Cards = new ReadOnlyCardCollection(typeof(Board), () => Data.Id, auth);
			CustomFields = new ReadOnlyCustomFieldDefinitionCollection(() => Data.Id, auth);
			Labels = new BoardLabelCollection(() => Data.Id, auth);
			Lists = new ListCollection(() => Data.Id, auth);
			Members = new ReadOnlyMemberCollection(EntityRequestType.Board_Read_Members, () => Data.Id, auth);
			Memberships = new BoardMembershipCollection(() => Data.Id, auth);
			PowerUps = new ReadOnlyPowerUpCollection(() => Data.Id, auth);
			PowerUpData = new ReadOnlyPowerUpDataCollection(EntityRequestType.Board_Read_PowerUpData, () => Data.Id, auth);

			BoardPreferencesContext = new BoardPreferencesContext(Auth);
			BoardPreferencesContext.SynchronizeRequested += ct => Synchronize(ct);
			BoardPreferencesContext.SubmitRequested += ct => HandleSubmitRequested("Preferences", ct);
			Data.Prefs = BoardPreferencesContext.Data;
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(Board.Fields)).Cast<Board.Fields>().ToList();
				var availableFields = (Board.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & (~MemberFields);
				if (parameterFields.HasFlag(Board.Fields.Actions))
				{
					Parameters["actions"] = "all";
					Parameters["actions_format"] = "list";
				}
				if (parameterFields.HasFlag(Board.Fields.Cards))
					Parameters["cards"] = "visible";
				if (parameterFields.HasFlag(Board.Fields.CustomFields))
					Parameters["customFieldItems"] = "true";
				if (parameterFields.HasFlag(Board.Fields.Labels))
					Parameters["labels"] = "all";
				if (parameterFields.HasFlag(Board.Fields.Lists))
					Parameters["lists"] = "open";
				if (parameterFields.HasFlag(Board.Fields.Members))
					Parameters["members"] = "all";
				if (parameterFields.HasFlag(Board.Fields.Memberships))
					Parameters["memberships"] = "all";
				if (parameterFields.HasFlag(Board.Fields.PowerUps))
					Parameters["plugins"] = "true";
				if (parameterFields.HasFlag(Board.Fields.PowerUpData))
					Parameters["pluginData"] = "true";
			}
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_Delete,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}
		public override async Task Expire(CancellationToken ct)
		{
			await base.Expire(ct);
		}

		protected override async Task<IJsonBoard> GetData(CancellationToken ct)
		{
			Dictionary<string, object> parameters;
			lock (Parameters)
			{
				parameters = new Dictionary<string, object>(Parameters);
			}
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Refresh,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonBoard>(Auth, endpoint, ct, parameters);

			return newData;
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
		protected override IEnumerable<string> MergeDependencies(IJsonBoard json)
		{
			var properties = BoardPreferencesContext.Merge(json.Prefs).ToList();

			if (json.Actions != null)
			{
				Actions.Update(json.Actions.Select(a => a.GetFromCache<Action, IJsonAction>(Auth)));
				properties.Add(nameof(Board.Actions));
			}
			if (json.Cards != null)
			{
				Cards.Update(json.Cards.Select(a => a.GetFromCache<Card, IJsonCard>(Auth)));
				properties.Add(nameof(Board.Cards));
			}
			if (json.CustomFields != null)
			{
				CustomFields.Update(json.CustomFields.Select(a => a.GetFromCache<CustomFieldDefinition>(Auth)));
				properties.Add(nameof(Board.CustomFields));
			}
			if (json.Labels != null)
			{
				Labels.Update(json.Labels.Select(a => a.GetFromCache<Label, IJsonLabel>(Auth)));
				properties.Add(nameof(Board.Labels));
			}
			if (json.Lists != null)
			{
				Lists.Update(json.Lists.Select(a => a.GetFromCache<List, IJsonList>(Auth)));
				properties.Add(nameof(Board.Lists));
			}
			if (json.Members != null)
			{
				Members.Update(json.Members.Select(a => a.GetFromCache<Member, IJsonMember>(Auth)));
				properties.Add(nameof(Board.Members));
			}
			if (json.Memberships != null)
			{
				Memberships.Update(json.Memberships.Select(a => a.TryGetFromCache<BoardMembership, IJsonBoardMembership>() ??
				                                                new BoardMembership(a, Data.Id, Auth)));
				properties.Add(nameof(Board.Memberships));
			}
			if (json.PowerUps != null)
			{
				PowerUps.Update(json.PowerUps.Select(a => a.GetFromCache<IPowerUp>(Auth)));
				properties.Add(nameof(Card.Actions));
			}
			if (json.PowerUpData != null)
			{
				PowerUpData.Update(json.PowerUpData.Select(a => a.GetFromCache<PowerUpData, IJsonPowerUpData>(Auth)));
				properties.Add(nameof(Card.Actions));
			}

			return properties;
		}
		protected override bool IsDataComplete => !Data.Name.IsNullOrWhiteSpace();
	}
}