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
	internal class OrganizationContext : SynchronizationContext<IJsonOrganization>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Organization.Fields MemberFields;

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
		public BoardCollection Boards { get; }
		public ReadOnlyMemberCollection Members { get; }
		public OrganizationMembershipCollection Memberships { get; }
		public ReadOnlyPowerUpDataCollection PowerUpData { get; }
		public OrganizationPreferencesContext OrganizationPreferencesContext { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static OrganizationContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Organization.Fields.Description |
			               Organization.Fields.DisplayName |
			               Organization.Fields.LogoHash |
			               Organization.Fields.Name |
			               Organization.Fields.Preferences |
			               Organization.Fields.Url |
			               Organization.Fields.Website;
			Properties = new Dictionary<string, Property<IJsonOrganization>>
				{
					{
						nameof(Organization.Description),
						new Property<IJsonOrganization, string>((d, a) => d.Desc, (d, o) => d.Desc = o)
					},
					{
						nameof(Organization.DisplayName),
						new Property<IJsonOrganization, string>((d, a) => d.DisplayName, (d, o) => d.DisplayName = o)
					},
					{
						nameof(Organization.Id),
						new Property<IJsonOrganization, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Organization.IsBusinessClass),
						new Property<IJsonOrganization, bool?>((d, a) => d.PaidAccount, (d, o) => d.PaidAccount = o)
					},
					{
						nameof(Organization.Name),
						new Property<IJsonOrganization, string>((d, a) => d.Name, (d, o) => d.Name = o)
					},
					{
						nameof(Organization.Preferences),
						new Property<IJsonOrganization, IJsonOrganizationPreferences>((d, a) => d.Prefs, (d, o) => d.Prefs = o)
					},
					{
						nameof(Organization.Url),
						new Property<IJsonOrganization, string>((d, a) => d.Url, (d, o) => d.Url = o)
					},
					{
						nameof(Organization.Website),
						new Property<IJsonOrganization, string>((d, a) => d.Website, (d, o) => d.Website = o)
					},
					{
						nameof(IJsonOrganization.ValidForMerge),
						new Property<IJsonOrganization, bool>((d, a) => d.ValidForMerge, (d, o) => d.ValidForMerge = o, true)
					},
				};
		}
		public OrganizationContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;

			Actions = new ReadOnlyActionCollection(typeof(Organization), () => Data.Id, auth);
			Boards = new BoardCollection(typeof(Organization), () => Data.Id, auth);
			Members = new ReadOnlyMemberCollection(EntityRequestType.Organization_Read_Members, () => Data.Id, auth);
			Memberships = new OrganizationMembershipCollection(() => Data.Id, auth);
			PowerUpData = new ReadOnlyPowerUpDataCollection(EntityRequestType.Organization_Read_PowerUpData, () => Data.Id, auth);

			OrganizationPreferencesContext = new OrganizationPreferencesContext(Auth);
			OrganizationPreferencesContext.SubmitRequested += ct => HandleSubmitRequested("Preferences", ct);
			Data.Prefs = OrganizationPreferencesContext.Data;
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
				var flags = Enum.GetValues(typeof(Organization.Fields)).Cast<Organization.Fields>().ToList();
				var availableFields = (Organization.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Organization.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & Organization.DownloadedFields & (~MemberFields);
				if (parameterFields.HasFlag(Organization.Fields.Actions))
				{
					Parameters["actions"] = "all";
					Parameters["actions_format"] = "list";
				}

				if (parameterFields.HasFlag(Organization.Fields.Boards))
				{
					Parameters["boards"] = "open";
					Parameters["board_fields"] = BoardContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Organization.Fields.Members))
				{
					Parameters["members"] = "all";
					Parameters["member_fields"] = MemberContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Organization.Fields.Memberships))
				{
					Parameters["memberships"] = "all";
					Parameters["memberships_member"] = "true";
					Parameters["membership_member_fields"] = MemberContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(Organization.Fields.PowerUpData))
					Parameters["pluginData"] = "true";
			}
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}

		protected override async Task<IJsonOrganization> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonOrganization>(Auth, endpoint, ct, CurrentParameters);

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
		protected override async Task SubmitData(IJsonOrganization json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Organization_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}
		protected override void ApplyDependentChanges(IJsonOrganization json)
		{
			if (json.Prefs != null)
			{
				json.Prefs = OrganizationPreferencesContext.GetChanges();
				OrganizationPreferencesContext.ClearChanges();
			}
		}
		protected override IEnumerable<string> MergeDependencies(IJsonOrganization json, bool overwrite)
		{
			var properties = OrganizationPreferencesContext.Merge(json.Prefs, overwrite)
			                                               .Select(p => $"{nameof(Organization.Preferences)}.{p}")
			                                               .ToList();

			if (json.Actions != null)
			{
				Actions.Update(json.Actions.Select(a => a.GetFromCache<Action, IJsonAction>(Auth, overwrite)));
				properties.Add(nameof(Organization.Actions));
			}
			if (json.Boards != null)
			{
				Boards.Update(json.Boards.Select(a => a.GetFromCache<Board, IJsonBoard>(Auth, overwrite)));
				properties.Add(nameof(Organization.Boards));
			}
			if (json.Members != null)
			{
				Members.Update(json.Members.Select(a => a.GetFromCache<Member, IJsonMember>(Auth, overwrite)));
				properties.Add(nameof(Organization.Members));
			}
			if (json.Memberships != null)
			{
				Memberships.Update(json.Memberships.Select(a => a.TryGetFromCache<OrganizationMembership, IJsonOrganizationMembership>(overwrite) ??
				                                                new OrganizationMembership(a, Data.Id, Auth)));
				properties.Add(nameof(Organization.Memberships));
			}
			if (json.PowerUpData != null)
			{
				PowerUpData.Update(json.PowerUpData.Select(a => a.GetFromCache<PowerUpData, IJsonPowerUpData>(Auth, overwrite)));
				properties.Add(nameof(Organization.PowerUpData));
			}

			return properties;
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}