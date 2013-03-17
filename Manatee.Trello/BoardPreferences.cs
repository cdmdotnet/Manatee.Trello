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
 
	File Name:		BoardPreferences.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardPreferences
	Purpose:		Represents available preferences setting for a board
					on Trello.com.

***************************************************************************************/
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "prefs":{
	//      "permissionLevel":"public",
	//      "voting":"members",
	//      "comments":"members",
	//      "invitations":"members",
	//      "selfJoin":false,
	//      "cardCovers":true
	//   },
	public class BoardPreferences : OwnedEntityBase<Board>
	{
		private static readonly OneToOneMap<BoardCommentType, string> _commentMap;
		private static readonly OneToOneMap<BoardInvitationType, string> _invitationMap;
		private static readonly OneToOneMap<BoardPermissionLevelType, string> _permissionMap;
		private static readonly OneToOneMap<BoardVotingType, string> _votingMap;

		private bool? _allowsSelfJoin;
		private string _apiComments;
		private string _apiInvitations;
		private string _apiPermissionLevel;
		private string _apiVoting;
		private BoardCommentType _comments;
		private BoardInvitationType _invitations;
		private BoardPermissionLevelType _permissionLevel;
		private bool? _showCardCovers;
		private BoardVotingType _voting;

		public bool? AllowsSelfJoin
		{
			get
			{
				VerifyNotExpired();
				return _allowsSelfJoin;
			}
			set { _allowsSelfJoin = value; }
		}
		public BoardCommentType Comments
		{
			get
			{
				VerifyNotExpired();
				return _comments;
			}
			set
			{
				_comments = value;
				UpdateApiComments();
			}
		}
		public BoardInvitationType Invitations
		{
			get
			{
				VerifyNotExpired();
				return _invitations;
			}
			set
			{
				_invitations = value;
				UpdateApiInvitations();
			}
		}
		public BoardPermissionLevelType PermissionLevel
		{
			get
			{
				VerifyNotExpired();
				return _permissionLevel;
			}
			set
			{
				_permissionLevel = value;
				UpdateApiPermissionLevel();
			}
		}
		public bool? ShowCardCovers
		{
			get
			{
				VerifyNotExpired();
				return _showCardCovers;
			}
			set { _showCardCovers = value; }
		}
		public BoardVotingType Voting
		{
			get
			{
				VerifyNotExpired();
				return _voting;
			}
			set
			{
				_voting = value;
				UpdateApiVoting();
			}
		}

		static BoardPreferences()
		{
			_commentMap = new OneToOneMap<BoardCommentType, string>
			              	{
			              		{BoardCommentType.Members, "members"},
			              		{BoardCommentType.Org, "org"},
			              		{BoardCommentType.Public, "public"},
			              		{BoardCommentType.Disabled, "disabled"},
			              	};
			_invitationMap = new OneToOneMap<BoardInvitationType, string>
			                 	{
			                 		{BoardInvitationType.Members, "members"},
			                 		{BoardInvitationType.Admins, "admins"},
			                 	};
			_permissionMap = new OneToOneMap<BoardPermissionLevelType, string>
			                 	{
			                 		{BoardPermissionLevelType.Private, "private"},
			                 		{BoardPermissionLevelType.Org, "org"},
			                 		{BoardPermissionLevelType.Public, "public"},
			                 	};
			_votingMap = new OneToOneMap<BoardVotingType, string>
			             	{
			             		{BoardVotingType.Members, "members"},
			             		{BoardVotingType.Org, "org"},
			             		{BoardVotingType.Public, "public"},
			             		{BoardVotingType.Disabled, "disabled"},
			             	};
		}
		public BoardPreferences() {}
		public BoardPreferences(TrelloService svc, Board owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_allowsSelfJoin = obj.TryGetBoolean("selfJoin");
			_apiComments = obj.TryGetString("comments");
			_apiInvitations = obj.TryGetString("invitations");
			_apiPermissionLevel = obj.TryGetString("permissionLevel");
			_showCardCovers = obj.TryGetBoolean("cardCovers");
			_apiVoting = obj.TryGetString("voting");
			UpdateComments();
			UpdateInvitations();
			UpdatePermissionLevel();
			UpdateVoting();
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
							{"selfJoin", _allowsSelfJoin.HasValue ? _allowsSelfJoin.Value : JsonValue.Null},
			           		{"comments", _apiComments},
			           		{"invitations", _apiInvitations},
			           		{"permissionLevel", _apiPermissionLevel},
							{"cardCovers", _showCardCovers.HasValue ? _showCardCovers.Value : JsonValue.Null},
			           		{"voting", _apiVoting}
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			return true;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var prefs = entity as BoardPreferences;
			if (prefs == null) return;
			_allowsSelfJoin = prefs._allowsSelfJoin;
			_apiComments = prefs._apiComments;
			_apiInvitations = prefs._apiInvitations;
			_apiPermissionLevel = prefs._apiPermissionLevel;
			_showCardCovers = prefs._showCardCovers;
			_apiVoting = prefs._apiVoting;
			UpdateComments();
			UpdateInvitations();
			UpdatePermissionLevel();
			UpdateVoting();
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<Board, BoardPreferences>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}

		private void UpdateComments()
		{
			_comments = _commentMap.Any(kvp => kvp.Value == _apiComments) ? _commentMap[_apiComments] : BoardCommentType.Unknown;
		}
		private void UpdateApiComments()
		{
			if (_commentMap.Any(kvp => kvp.Key == _comments))
				_apiComments = _commentMap[_comments];
		}
		private void UpdateInvitations()
		{
			_invitations = _invitationMap.Any(kvp => kvp.Value == _apiInvitations) ? _invitationMap[_apiInvitations] : BoardInvitationType.Unknown;
		}
		private void UpdateApiInvitations()
		{
			if (_invitationMap.Any(kvp => kvp.Key == _invitations))
				_apiInvitations = _invitationMap[_invitations];
		}
		private void UpdatePermissionLevel()
		{
			_permissionLevel = _permissionMap.Any(kvp => kvp.Value == _apiPermissionLevel) ? _permissionMap[_apiPermissionLevel] : BoardPermissionLevelType.Unknown;
		}
		private void UpdateApiPermissionLevel()
		{
			if (_permissionMap.Any(kvp => kvp.Key == _permissionLevel))
				_apiPermissionLevel = _permissionMap[_permissionLevel];
		}
		private void UpdateVoting()
		{
			_voting = _votingMap.Any(kvp => kvp.Value == _apiVoting) ? _votingMap[_apiVoting] : BoardVotingType.Unknown;
		}
		private void UpdateApiVoting()
		{
			if (_votingMap.Any(kvp => kvp.Key == _voting))
				_apiVoting = _votingMap[_voting];
		}
	}
}