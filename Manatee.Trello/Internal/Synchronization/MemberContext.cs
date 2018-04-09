using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class MemberContext : SynchronizationContext<IJsonMember>
	{
		public MemberPreferencesContext MemberPreferencesContext { get; }
		protected override bool IsDataComplete => !Data.FullName.IsNullOrWhiteSpace();
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static MemberContext()
		{
			_properties = new Dictionary<string, Property<IJsonMember>>
				{
					{"AvatarSource", new Property<IJsonMember, AvatarSource?>((d, a) => d.AvatarSource, (d, o) => d.AvatarSource = o)},
					{"AvatarUrl", new Property<IJsonMember, string>((d, a) => d.AvatarHash, (d, o) => d.AvatarHash = o)},
					{"Bio", new Property<IJsonMember, string>((d, a) => d.Bio, (d, o) => d.Bio = o)},
					{"Email", new Property<IJsonMember, string>((d, a) => d.Email, (d, o) => d.Email = o)},
					{"FullName", new Property<IJsonMember, string>((d, a) => d.FullName, (d, o) => d.FullName = o)},
					{"Id", new Property<IJsonMember, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Initials", new Property<IJsonMember, string>((d, a) => d.Initials, (d, o) => d.Initials = o)},
					{"IsConfirmed", new Property<IJsonMember, bool?>((d, a) => d.Confirmed, (d, o) => d.Confirmed = o)},
					{"Preferences", new Property<IJsonMember, IJsonMemberPreferences>((d, a) => d.Prefs, (d, o) => d.Prefs = o)},
					{"Similarity", new Property<IJsonMember, int?>((d, a) => d.Similarity, (d, o) => d.Similarity = o)},
					{"Status", new Property<IJsonMember, MemberStatus?>((d, a) => d.Status, (d, o) => d.Status = o)},
					{"Trophies", new Property<IJsonMember, List<string>>((d, a) => d.Trophies, (d, o) => d.Trophies = o?.ToList())},
					{"Url", new Property<IJsonMember, string>((d, a) => d.Url, (d, o) => d.Url = o)},
					{"UserName", new Property<IJsonMember, string>((d, a) => d.Username, (d, o) => d.Username = o)},
				};
		}
		public MemberContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			MemberPreferencesContext = new MemberPreferencesContext(Auth);
			MemberPreferencesContext.SynchronizeRequested += ct => Synchronize(ct);
			MemberPreferencesContext.SubmitRequested += ct => HandleSubmitRequested("Preferences", ct);
			Data.Prefs = MemberPreferencesContext.Data;
		}

		public override async Task Expire(CancellationToken ct)
		{
			if (TrelloConfiguration.AutoUpdate)
				await MemberPreferencesContext.Expire(ct);
			await base.Expire(ct);
		}

		protected override async Task<IJsonMember> GetData(CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonMember>(Auth, endpoint, ct);

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
	}
}