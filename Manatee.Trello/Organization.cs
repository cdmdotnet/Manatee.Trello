using System;
using System.Linq;
using Manatee.Json;
using System.Collections.Generic;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//{
	//   "id":"50d4eb07a1b0902152003329",
	//   "name":"littlecrabsolutions",
	//   "displayName":"Little Crab Solutions",
	//   "desc":"",
	//   "url":"https://trello.com/littlecrabsolutions",
	//   "website":null,
	//   "logoHash":null,
	//   "powerUps":[
	//   ]
	//}
	public class Organization : EntityBase
	{
		private readonly ExpiringList<Organization, Action> _actions;
		private readonly ExpiringList<Organization, Board> _boards;
		private string _description;
		private string _displayName;
		private string _logoHash;
		private readonly ExpiringList<Organization, Member> _members;
		private string _name;
		private List<string> _powerUps;
		private readonly OrganizationPreferences _preferences;
		private string _url;
		private string _website;

		public IEntityCollection<Action> Actions { get { return _actions; } }
		public IEntityCollection<Board> Boards { get { return _boards; } }
		public string Description
		{
			get
			{
				VerifyNotExpired();
				return _description;
			}
			set { _description = value; }
		}
		public string DisplayName
		{
			get
			{
				VerifyNotExpired();
				return _displayName;
			}
			set { _displayName = value; }
		}
		public string LogoHash
		{
			get
			{
				VerifyNotExpired();
				return _logoHash;
			}
			set { _logoHash = value; }
		}
		public IEntityCollection<Member> Members { get { return _members; } }
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
			set { _name = value; }
		}
		public List<string> PowerUps
		{
			get
			{
				VerifyNotExpired();
				return _powerUps;
			}
			private set { _powerUps = value; }
		}
		public OrganizationPreferences Preferences { get { return _preferences; } }
		public string Url
		{
			get
			{
				VerifyNotExpired();
				return _url;
			}
		}
		public string Website
		{
			get
			{
				VerifyNotExpired();
				return _website;
			}
			set { _website = value; }
		}

		public Organization()
		{
			_actions = new ExpiringList<Organization, Action>(this);
			_boards = new ExpiringList<Organization, Board>(this);
			_members = new ExpiringList<Organization, Member>(this);
			_preferences = new OrganizationPreferences();
		}
		internal Organization(TrelloService svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<Organization, Action>(svc, this);
			_boards = new ExpiringList<Organization, Board>(svc, this);
			_members = new ExpiringList<Organization, Member>(svc, this);
			_preferences = new OrganizationPreferences(svc, this);
		}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_description = obj.TryGetString("desc");
			_displayName = obj.TryGetString("displayName");
			_logoHash = obj.TryGetString("logoHash");
			_name = obj.TryGetString("name");
			_powerUps = obj.TryGetArray("powerUps").StringsFromJson();
			_url = obj.TryGetString("url");
			_website = obj.TryGetString("website");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"desc", _description},
			           		{"displayName", _displayName},
			           		{"logoHash", _logoHash},
			           		{"name", _name},
			           		{"powerUps", _powerUps.ToJson()},
			           		{"url", _url},
			           		{"website", _website}
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			var org = other as Organization;
			if (org == null) return false;
			return Id == org.Id;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var org = entity as Organization;
			if (org == null) return;
			_description = org._description;
			_displayName = org._displayName;
			_logoHash = org._logoHash;
			_name = org._name;
			_powerUps = org._powerUps;
			_url = org._url;
			_website = org._website;
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetEntity<Organization>(Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_actions.Svc = Svc;
			_boards.Svc = Svc;
			_members.Svc = Svc;
			_preferences.Svc = Svc;
		}
	}
}
