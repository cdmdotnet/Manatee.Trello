using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//{
	//   "id":"514464db3fa062da6e00254f",
	//   "avatarHash":null,
	//   "bio":"This is a service account for the Little Crab Solutions organization.",
	//   "fullName":"Little Crab Solutions",
	//   "initials":"LS",
	//   "memberType":"normal",
	//   "status":"active",
	//   "url":"https://trello.com/s_littlecrabsolutions",
	//   "username":"s_littlecrabsolutions",
	//   "avatarSource":"none",
	//   "confirmed":true,
	//   "email":null,
	//   "gravatarHash":"946866d028bf7a004a51ca57f7cc1cf2",
	//   "idBoards":[
	//      "514464db3fa062da6e002550",
	//      "5144051cbd0da6681200201e"
	//   ],
	//   "idBoardsInvited":[

	//   ],
	//   "idBoardsPinned":[
	//      "514464db3fa062da6e002550",
	//      "5144051cbd0da6681200201e"
	//   ],
	//   "idOrganizations":[
	//      "50d4eb07a1b0902152003329"
	//   ],
	//   "idOrganizationsInvited":[

	//   ],
	//   "idPremOrgsAdmin":[

	//   ],
	//   "loginTypes":null,
	//   "prefs":{
	//      "sendSummaries":true,
	//      "minutesBetweenSummaries":60,
	//      "minutesBeforeDeadlineToNotify":1440,
	//      "colorBlind":false
	//   },
	//   "trophies":[

	//   ],
	//   "uploadedAvatarHash":null
	//}
	public class Member : EntityBase
	{
		private static readonly OneToOneMap<MemberStatus, string> _statusMap;

		private string _apiStatus;
		private string _avatarHash;
		private string _avatarSource;
		private string _bio;
		private readonly ExpiringList<Member, Board> _boards;
		private bool? _confirmed;
		private string _email;
		private string _fullName;
		private string _gravatarHash;
		private string _initials;
		private readonly ExpiringList<Member, InvitedBoard> _invitedBoards;
		private readonly ExpiringList<Member, InvitedOrganization> _invitedOrganizations;
		private List<string> _loginTypes;
		private string _memberType;
		private readonly ExpiringList<Member, Organization> _organizations;
		private readonly ExpiringList<Member, PinnedBoard> _pinnedBoards;
		private readonly MemberPreferences _preferences;
		private readonly ExpiringList<Member, PremiumOrganization> _premiumOrganizations;
		private MemberStatus _status;
		private List<string> _trophies;
		private string _uploadedAvatarHash;
		private string _url;
		private string _username;

		public string AvatarHash
		{
			get
			{
				VerifyNotExpired();
				return _avatarHash;
			}
			set { _avatarHash = value; }
		}
		public string AvatarSource
		{
			get
			{
				VerifyNotExpired();
				return _avatarSource;
			}
			set { _avatarSource = value; }
		}
		public string Bio
		{
			get
			{
				VerifyNotExpired();
				return _bio;
			}
			set { _bio = value; }
		}
		public IEntityCollection<Board> Boards { get { return _boards; } }
		private bool? Confirmed
		{
			get
			{
				VerifyNotExpired();
				return _confirmed;
			}
			set { _confirmed = value; }
		}
		public string Email
		{
			get
			{
				VerifyNotExpired();
				return _email;
			}
			set { _email = value; }
		}
		public string FullName
		{
			get
			{
				VerifyNotExpired();
				return _fullName;
			}
			set { _fullName = value; }
		}
		public string GravatarHash
		{
			get
			{
				VerifyNotExpired();
				return _gravatarHash;
			}
			set { _gravatarHash = value; }
		}
		public string Initials
		{
			get
			{
				VerifyNotExpired();
				return _initials;
			}
			set { _initials = value; }
		}
		//public IEntityCollection<Board> InvitedBoardIds { get { return _invitedBoards; } }
		//public IEntityCollection<Organization> InvitedOrganizations { get { return _invitedOrganizations; } }
		public List<string> LoginTypes
		{
			get { return _loginTypes; }
		}
		public string MemberType
		{
			get { return _memberType; }
			set { _memberType = value; }
		}
		public IEntityCollection<Organization> Organizations { get { return _organizations; } }
		//public IEntityCollection<Board> PinnedBoards { get { return _pinnedBoards; } }
		public MemberPreferences Preferences { get { return _preferences; } }
		//public IEntityCollection<Organization> PremiumOrganizations { get { return _premiumOrganizations; } }
		public MemberStatus Status
		{
			get { return _status; }
			set
			{
				_status = value;
				UpdateApiStatus();
			}
		}
		public List<string> Trophies
		{
			get
			{
				VerifyNotExpired();
				return _trophies;
			}
		}
		public string UploadedAvatarHash
		{
			get
			{
				VerifyNotExpired();
				return _uploadedAvatarHash;
			}
			set { _uploadedAvatarHash = value; }
		}
		public string Url
		{
			get
			{
				VerifyNotExpired();
				return _url;
			}
		}
		public string Username
		{
			get
			{
				VerifyNotExpired();
				return _username;
			}
			set { _username = value; }
		}

		static Member()
		{
			_statusMap = new OneToOneMap<MemberStatus, string>
			           	{
			           		{MemberStatus.Disconnected, "disconnected"},
			           		{MemberStatus.Idle, "idle"},
			           		{MemberStatus.Active, "active"},
			           	};
		}
		public Member()
		{
			_premiumOrganizations = new ExpiringList<Member, PremiumOrganization>(this);
			_boards = new ExpiringList<Member, Board>(this);
			_invitedBoards = new ExpiringList<Member, InvitedBoard>(this);
			_invitedOrganizations = new ExpiringList<Member, InvitedOrganization>(this);
			_organizations = new ExpiringList<Member, Organization>(this);
			_pinnedBoards = new ExpiringList<Member, PinnedBoard>(this);
			_preferences = new MemberPreferences(null, this);
		}
		internal Member(TrelloService svc, string id)
			: base(svc, id)
		{
			_premiumOrganizations = new ExpiringList<Member, PremiumOrganization>(svc, this);
			_boards = new ExpiringList<Member, Board>(svc, this);
			_invitedBoards = new ExpiringList<Member, InvitedBoard>(svc, this);
			_invitedOrganizations = new ExpiringList<Member, InvitedOrganization>(svc, this);
			_organizations = new ExpiringList<Member, Organization>(svc, this);
			_pinnedBoards = new ExpiringList<Member, PinnedBoard>(svc, this);
			_preferences = new MemberPreferences(svc, this);
		}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_avatarHash = obj.TryGetString("avatarHash");
			_avatarSource = obj.TryGetString("avatarSource");
			_bio = obj.TryGetString("bio");
			_confirmed = obj.TryGetBoolean("confirmed");
			_email = obj.TryGetString("email");
			_fullName = obj.TryGetString("fullName");
			_gravatarHash = obj.TryGetString("gravatarHash");
			_initials = obj.TryGetString("initials");
			_loginTypes = obj.TryGetArray("loginTypes").StringsFromJson();
			_memberType = obj.TryGetString("memberType");
			_apiStatus = obj.TryGetString("status");
			_trophies = obj.TryGetArray("trophies").StringsFromJson();
			_uploadedAvatarHash = obj.TryGetString("uploadedAvatarHash");
			_url = obj.TryGetString("url");
			_username = obj.TryGetString("username");
			UpdateStatus();
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"avatarHash", _avatarHash},
			           		{"avatarSource", _avatarSource},
			           		{"bio", _bio},
			           		{"confirmed", _confirmed.HasValue ? _confirmed.Value : JsonValue.Null},
			           		{"email", _email},
			           		{"fullName", _fullName},
			           		{"gravatarHash", _gravatarHash},
			           		{"initials", _initials},
			           		{"loginTypes", _loginTypes.ToJson()},
			           		{"memberType", _memberType},
			           		{"status", _apiStatus},
			           		{"trophies", _trophies.ToJson()},
			           		{"uploadedAvatarHash", _uploadedAvatarHash},
			           		{"url", _url},
			           		{"username", _username}
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			var member = other as Member;
			if (member == null) return false;
			return Id == member.Id;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var member = entity as Member;
			if (member == null) return;
			_avatarHash = member._avatarHash;
			_avatarSource = member._avatarSource;
			_bio = member._bio;
			_confirmed = member._confirmed;
			_email = member._email;
			_fullName = member._fullName;
			_gravatarHash = member._gravatarHash;
			_initials = member._initials;
			_loginTypes = member._loginTypes;
			_memberType = member._memberType;
			_apiStatus = member._apiStatus;
			_trophies = member._trophies;
			_uploadedAvatarHash = member._uploadedAvatarHash;
			_url = member._url;
			_username = member._username;
			UpdateStatus();
		}
		internal override bool Match(string id)
		{
			return (Id == id) || (Username == id);
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetEntity<CheckItem>(Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_premiumOrganizations.Svc = Svc;
			_boards.Svc = Svc;
			_invitedBoards.Svc = Svc;
			_invitedOrganizations.Svc = Svc;
			_organizations.Svc = Svc;
			_pinnedBoards.Svc = Svc;
			_preferences.Svc = Svc;
		}

		private void UpdateStatus()
		{
			_status = _statusMap.Any(kvp => kvp.Value == _apiStatus) ? _statusMap[_apiStatus] : MemberStatus.Unknown;
		}
		private void UpdateApiStatus()
		{
			if (_statusMap.Any(kvp => kvp.Key == _status))
				_apiStatus = _statusMap[_status];
		}
	}
}
