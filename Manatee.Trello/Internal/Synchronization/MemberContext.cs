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
	internal class MemberContext : SynchronizationContext<IJsonMember>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Member.Fields MemberFields;

		public ReadOnlyActionCollection Actions { get; }
		public ReadOnlyBoardCollection Boards { get; }
		public ReadOnlyCardCollection Cards { get; }
		public ReadOnlyNotificationCollection Notifications { get; }
		public ReadOnlyOrganizationCollection Organizations { get; }
		public MemberPreferencesContext MemberPreferencesContext { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static MemberContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Member.Fields.AvatarHash |
			               Member.Fields.AvatarSource |
			               Member.Fields.Bio |
			               Member.Fields.IsConfirmed |
			               Member.Fields.Email |
			               Member.Fields.FullName |
			               Member.Fields.GravatarHash |
			               Member.Fields.Initials |
			               Member.Fields.LoginTypes |
			               Member.Fields.MemberType |
			               Member.Fields.OneTimeMessagesDismissed |
			               Member.Fields.Preferencess |
			               Member.Fields.Status |
			               Member.Fields.Trophies |
			               Member.Fields.UploadedAvatarHash |
			               Member.Fields.Url |
			               Member.Fields.Username;
			Properties = new Dictionary<string, Property<IJsonMember>>
				{
					{
						nameof(Member.AvatarSource),
						new Property<IJsonMember, AvatarSource?>((d, a) => d.AvatarSource, (d, o) => d.AvatarSource = o)
					},
					{
						nameof(Member.AvatarUrl),
						new Property<IJsonMember, string>((d, a) => d.AvatarHash, (d, o) => d.AvatarHash = o)
					},
					{
						nameof(Member.Bio),
						new Property<IJsonMember, string>((d, a) => d.Bio, (d, o) => d.Bio = o)
					},
					{
						nameof(Me.Email),
						new Property<IJsonMember, string>((d, a) => d.Email, (d, o) => d.Email = o)
					},
					{
						nameof(Member.FullName),
						new Property<IJsonMember, string>((d, a) => d.FullName, (d, o) => d.FullName = o)
					},
					{
						nameof(Member.Id),
						new Property<IJsonMember, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Member.Initials),
						new Property<IJsonMember, string>((d, a) => d.Initials, (d, o) => d.Initials = o)
					},
					{
						nameof(Member.IsConfirmed),
						new Property<IJsonMember, bool?>((d, a) => d.Confirmed, (d, o) => d.Confirmed = o)
					},
					{
						nameof(Me.Preferences),
						new Property<IJsonMember, IJsonMemberPreferences>((d, a) => d.Prefs, (d, o) => d.Prefs = o)
					},
					{
						nameof(Member.Status),
						new Property<IJsonMember, MemberStatus?>((d, a) => d.Status, (d, o) => d.Status = o)
					},
					{
						nameof(Member.Trophies),
						new Property<IJsonMember, List<string>>((d, a) => d.Trophies, (d, o) => d.Trophies = o?.ToList())
					},
					{
						nameof(Member.Url),
						new Property<IJsonMember, string>((d, a) => d.Url, (d, o) => d.Url = o)
					},
					{
						nameof(Member.UserName),
						new Property<IJsonMember, string>((d, a) => d.Username, (d, o) => d.Username = o)
					},
				};
		}
		public MemberContext(string id, bool isMe, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;

			Actions = new ReadOnlyActionCollection(typeof(Member), () => Data.Id, auth);
			Boards = isMe
				         ? new BoardCollection(typeof(Member), () => Data.Id, auth) 
				         : new ReadOnlyBoardCollection(typeof(Member), () => Data.Id, auth);
			Cards = new ReadOnlyCardCollection(EntityRequestType.Member_Read_Cards, () => Data.Id, auth);
			Organizations = isMe 
				                ? new OrganizationCollection(() => Data.Id, auth)
				                : new ReadOnlyOrganizationCollection(() => Data.Id, auth);
			Notifications = new ReadOnlyNotificationCollection(() => Data.Id, auth);

			MemberPreferencesContext = new MemberPreferencesContext(Auth);
			MemberPreferencesContext.SubmitRequested += ct => HandleSubmitRequested("Preferences", ct);
			Data.Prefs = MemberPreferencesContext.Data;
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(Member.Fields)).Cast<Member.Fields>().ToList();
				var availableFields = (Member.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Member.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & Member.DownloadedFields & (~MemberFields);
				if (parameterFields.HasFlag(Member.Fields.Actions))
				{
					Parameters["actions"] = "all";
					Parameters["actions_format"] = "list";
				}
				if (parameterFields.HasFlag(Member.Fields.Boards))
					Parameters["boards"] = "all";
				if (parameterFields.HasFlag(Member.Fields.Cards))
					Parameters["cards"] = "all";
				if (parameterFields.HasFlag(Member.Fields.Notifications))
					Parameters["notifications"] = "all";
				if (parameterFields.HasFlag(Member.Fields.Organizations))
					Parameters["organizations"] = "all";
			}
		}

		protected override async Task<IJsonMember> GetData(CancellationToken ct)
		{
			Dictionary<string, object> parameters;
			lock (Parameters)
			{
				parameters = new Dictionary<string, object>(Parameters);
			}
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonMember>(Auth, endpoint, ct, parameters);

			return newData;
		}
		protected override async Task SubmitData(IJsonMember json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			Merge(newData);
		}
		protected override void ApplyDependentChanges(IJsonMember json)
		{
			if (json.Prefs != null)
			{
				json.Prefs = MemberPreferencesContext.GetChanges();
				MemberPreferencesContext.ClearChanges();
			}
		}

		protected override IEnumerable<string> MergeDependencies(IJsonMember json)
		{
			var properties = MemberPreferencesContext.Merge(json.Prefs).ToList();

			if (json.Actions != null)
			{
				Actions.Update(json.Actions.Select(a => a.GetFromCache<Action, IJsonAction>(Auth)));
				properties.Add(nameof(Member.Actions));
			}
			if (json.Boards != null)
			{
				Boards.Update(json.Boards.Select(a => a.GetFromCache<Board, IJsonBoard>(Auth)));
				properties.Add(nameof(Member.Boards));
			}
			if (json.Cards != null)
			{
				Cards.Update(json.Cards.Select(a => a.GetFromCache<Card, IJsonCard>(Auth)));
				properties.Add(nameof(Member.Cards));
			}
			if (json.Notifications != null)
			{
				Notifications.Update(json.Notifications.Select(a => a.GetFromCache<Notification, IJsonNotification>(Auth)));
				properties.Add(nameof(Me.Notifications));
			}
			if (json.Organizations != null)
			{
				Organizations.Update(json.Organizations.Select(a => a.GetFromCache<Organization, IJsonOrganization>(Auth)));
				properties.Add(nameof(Member.Organizations));
			}

			return properties;
		}
	}
}