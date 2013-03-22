/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Member.cs
	Namespace:		Manatee.Trello
	Class Name:		Member
	Purpose:		Represents a member (user) on Trello.com.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;
using RestSharp;
using JsonObject = Manatee.Json.JsonObject;

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
	public class Member : JsonCompatibleExpiringObject, IEquatable<Member>
	{
		private static readonly OneToOneMap<MemberStatusType, string> _statusMap;

		internal static readonly Dictionary<string, Func<Member, object>> _propertyMap;

		private readonly ExpiringList<Member, Action> _actions;
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
		private readonly ExpiringList<Member, Notification> _notifications;
		private readonly ExpiringList<Member, Organization> _organizations;
		private readonly ExpiringList<Member, PinnedBoard> _pinnedBoards;
		private readonly MemberPreferences _preferences;
		private readonly ExpiringList<Member, PremiumOrganization> _premiumOrganizations;
		private MemberStatusType _status;
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
		}
		public string AvatarSource
		{
			get
			{
				VerifyNotExpired();
				return _avatarSource;
			}
			set
			{
				_avatarSource = value;
				Parameters.Add("avatarSource", _avatarSource);
				Put();
			}
		}
		public string Bio
		{
			get
			{
				VerifyNotExpired();
				return _bio;
			}
			set
			{
				_bio = value;
				Parameters.Add("bio", _bio);
				Put();
			}
		}
		public IEnumerable<Board> Boards { get { return _boards; } }
		private bool? Confirmed
		{
			get
			{
				VerifyNotExpired();
				return _confirmed;
			}
		}
		public string Email
		{
			get
			{
				VerifyNotExpired();
				return _email;
			}
		}
		public string FullName
		{
			get
			{
				VerifyNotExpired();
				return _fullName;
			}
			set
			{
				_fullName = value;
				Parameters.Add("fullName", _fullName);
				Put();
			}
		}
		public string GravatarHash
		{
			get
			{
				VerifyNotExpired();
				return _gravatarHash;
			}
		}
		public string Initials
		{
			get
			{
				VerifyNotExpired();
				return _initials;
			}
			set
			{
				_initials = value;
				Parameters.Add("initials", _initials);
				Put();
			}
		}
		public IEnumerable<Board> InvitedBoardIds { get { return _invitedBoards; } }
		public IEnumerable<Organization> InvitedOrganizations { get { return _invitedOrganizations; } }
		public List<string> LoginTypes
		{
			get
			{
				VerifyNotExpired();
				return _loginTypes;
			}
		}
		public string MemberType
		{
			get
			{
				VerifyNotExpired();
				return _memberType;
			}
		}
		public IEnumerable<Notification> Notifications { get { return _notifications; } }
		public IEnumerable<Organization> Organizations { get { return _organizations; } }
		public IEnumerable<Board> PinnedBoards { get { return _pinnedBoards; } }
		public MemberPreferences Preferences { get { return _preferences; } }
		public IEnumerable<Organization> PremiumOrganizations { get { return _premiumOrganizations; } }
		public MemberStatusType Status
		{
			get
			{
				VerifyNotExpired();
				return _status;
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
			set
			{
				_username = value;
				Parameters.Add("username", _username);
				Put();
			}
		}

		static Member()
		{
			_statusMap = new OneToOneMap<MemberStatusType, string>
			           	{
			           		{MemberStatusType.Disconnected, "disconnected"},
			           		{MemberStatusType.Idle, "idle"},
			           		{MemberStatusType.Active, "active"},
			           	};
			_propertyMap = new Dictionary<string, Func<Member, object>>
			               	{
			               		{"email", m => m._email},
			               		{"username", m => m._username},
			               		{"fullName", m => m._fullName},
			               	};
		}
		public Member()
		{
			_actions = new ExpiringList<Member, Action>(this);
			_premiumOrganizations = new ExpiringList<Member, PremiumOrganization>(this);
			_boards = new ExpiringList<Member, Board>(this);
			_invitedBoards = new ExpiringList<Member, InvitedBoard>(this);
			_invitedOrganizations = new ExpiringList<Member, InvitedOrganization>(this);
			_notifications = new ExpiringList<Member, Notification>(this);
			_organizations = new ExpiringList<Member, Organization>(this);
			_pinnedBoards = new ExpiringList<Member, PinnedBoard>(this);
			_preferences = new MemberPreferences(null, this);
		}
		internal Member(TrelloService svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<Member, Action>(svc, this);
			_premiumOrganizations = new ExpiringList<Member, PremiumOrganization>(svc, this);
			_boards = new ExpiringList<Member, Board>(svc, this);
			_invitedBoards = new ExpiringList<Member, InvitedBoard>(svc, this);
			_invitedOrganizations = new ExpiringList<Member, InvitedOrganization>(svc, this);
			_notifications = new ExpiringList<Member, Notification>(svc, this);
			_organizations = new ExpiringList<Member, Organization>(svc, this);
			_pinnedBoards = new ExpiringList<Member, PinnedBoard>(svc, this);
			_preferences = new MemberPreferences(svc, this);
		}

		public void MarkAllNotificationsAsRead()
		{
			Svc.PostAndCache(new Request<Notification>(new ExpiringObject[] {new Notification()}, urlExtension: "all/read"));
		}
		public void PinBoard(Board board)
		{
			Parameters.Add("value", board.Id);
			Svc.PostAndCache(new Request<Member>(new ExpiringObject[] {this, new PinnedBoard()}, this));
		}
		public void RescindVoteForCard(Card card)
		{
			Svc.DeleteFromCache(new Request<Card>(new ExpiringObject[] {card, new VotingMember {Id = Id}}));
		}
		public void UnpinBoard(Board board)
		{
			Svc.DeleteFromCache(new Request<Member>(new ExpiringObject[] {this, new PinnedBoard {Id = board.Id}}));
		}
		public void VoteForCard(Card card)
		{
			Parameters.Add("value", Id);
			Svc.PostAndCache(new Request<Card>(new ExpiringObject[] {card, new VotingMember()}, this));
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
		public bool Equals(Member other)
		{
			return Id == other.Id;
		}

		internal override void Refresh(ExpiringObject entity)
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
		internal void AddParameter(string property)
		{
			Parameters.Add(new Parameter{Name = property, Value = _propertyMap[property](this)});
		}

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<Member>(Id));
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_premiumOrganizations.Svc = Svc;
			_boards.Svc = Svc;
			_invitedBoards.Svc = Svc;
			_invitedOrganizations.Svc = Svc;
			_notifications.Svc = Svc;
			_organizations.Svc = Svc;
			_pinnedBoards.Svc = Svc;
			_preferences.Svc = Svc;
		}

		private void Put()
		{
			Svc.PutAndCache(new Request<Member>(this));
		}
		private void UpdateStatus()
		{
			_status = _statusMap.Any(kvp => kvp.Value == _apiStatus) ? _statusMap[_apiStatus] : MemberStatusType.Unknown;
		}
		private void UpdateApiStatus()
		{
			if (_statusMap.Any(kvp => kvp.Key == _status))
				_apiStatus = _statusMap[_status];
		}
	}
}
