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
using System;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
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
	///<summary>
	/// Represents available preferences setting for a board
	///</summary>
	public class BoardPreferences : JsonCompatibleExpiringObject
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
		private BoardCommentType _comments = BoardCommentType.Unknown;
		private BoardInvitationType _invitations = BoardInvitationType.Unknown;
		private BoardPermissionLevelType _permissionLevel = BoardPermissionLevelType.Unknown;
		private bool? _showCardCovers;
		private BoardVotingType _voting = BoardVotingType.Unknown;

		/// <summary>
		/// Gets and sets whether any Trello member may join a board without an invitation.
		/// </summary>
		public bool? AllowsSelfJoin
		{
			get
			{
				VerifyNotExpired();
				return _allowsSelfJoin;
			}
			set
			{
				Validate.Writable(Svc);
				if (_allowsSelfJoin == value) return;
				Validate.Nullable(value);
				_allowsSelfJoin = value;
				Parameters.Add("value", _allowsSelfJoin.ToLowerString());
				Put("selfJoin");
			}
		}
		/// <summary>
		/// Gets and sets who may comment on cards.
		/// </summary>
		public BoardCommentType Comments
		{
			get
			{
				VerifyNotExpired();
				return _comments;
			}
			set
			{
				Validate.Writable(Svc);
				if (_comments == value) return;
				_comments = value;
				UpdateApiComments();
				Parameters.Add("value", _apiComments);
				Put("comments");
			}
		}
		/// <summary>
		/// Gets and sets who may extend invitations to join the board.
		/// </summary>
		public BoardInvitationType Invitations
		{
			get
			{
				VerifyNotExpired();
				return _invitations;
			}
			set
			{
				Validate.Writable(Svc);
				if (_invitations == value) return;
				_invitations = value;
				UpdateApiInvitations();
				Parameters.Add("value", _apiInvitations);
				Put("invitations");
			}
		}
		/// <summary>
		/// Gets and sets who may view the board.
		/// </summary>
		public BoardPermissionLevelType PermissionLevel
		{
			get
			{
				VerifyNotExpired();
				return _permissionLevel;
			}
			set
			{
				Validate.Writable(Svc);
				if (_permissionLevel == value) return;
				_permissionLevel = value;
				UpdateApiPermissionLevel();
				Parameters.Add("value", _apiPermissionLevel);
				Put("permissionLevel");
			}
		}
		/// <summary>
		/// Gets and sets whether card covers are shown on the board.
		/// </summary>
		public bool? ShowCardCovers
		{
			get
			{
				VerifyNotExpired();
				return _showCardCovers;
			}
			set
			{
				Validate.Writable(Svc);
				if (_showCardCovers == value) return;
				Validate.Nullable(value);
				_showCardCovers = value;
				Parameters.Add("value", _showCardCovers.ToLowerString());
				Put("cardCovers");
			}
		}
		/// <summary>
		/// Gets and sets who may vote on cards.
		/// </summary>
		public BoardVotingType Voting
		{
			get
			{
				VerifyNotExpired();
				return _voting;
			}
			set
			{
				Validate.Writable(Svc);
				if (_voting == value) return;
				_voting = value;
				UpdateApiVoting();
				Parameters.Add("value", _apiVoting);
				Put("voting");
			}
		}

		internal override string Key { get { return "prefs"; } }

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
		/// <summary>
		/// Creates a new instance of the BoardPreferences class.
		/// </summary>
		public BoardPreferences() {}
		internal BoardPreferences(ITrelloRest svc, Board owner)
			: base(svc, owner) {}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
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
			_isInitialized = true;
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			if (!_isInitialized) VerifyNotExpired();
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

		internal override void Refresh(ExpiringObject entity)
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
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Get(Svc.RequestProvider.Create<BoardPreferences>(new[] {Owner, this}));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService() {}

		private void Put(string extension)
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var request = Svc.RequestProvider.Create<BoardPreferences>(new[] { Owner, this }, this, extension);
			Svc.Put(request);
		}
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