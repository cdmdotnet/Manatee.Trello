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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	///<summary>
	/// Represents available preferences setting for a board
	///</summary>
	public class BoardPreferences : ExpiringObject
	{
		private static readonly OneToOneMap<BoardCommentType, string> _commentMap;
		private static readonly OneToOneMap<BoardInvitationType, string> _invitationMap;
		private static readonly OneToOneMap<BoardPermissionLevelType, string> _permissionMap;
		private static readonly OneToOneMap<BoardVotingType, string> _votingMap;

		private IJsonBoardPreferences _jsonBoardPreferences;
		private BoardCommentType _comments = BoardCommentType.Unknown;
		private BoardInvitationType _invitations = BoardInvitationType.Unknown;
		private BoardPermissionLevelType _permissionLevel = BoardPermissionLevelType.Unknown;
		private BoardVotingType _voting = BoardVotingType.Unknown;

		/// <summary>
		/// Gets or sets whether any Trello member may join a board without an invitation.
		/// </summary>
		public bool? AllowsSelfJoin
		{
			get
			{
				VerifyNotExpired();
				return (_jsonBoardPreferences == null) ? null : _jsonBoardPreferences.SelfJoin;
			}
			set
			{

				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoardPreferences == null) return;
				if (_jsonBoardPreferences.SelfJoin == value) return;
				_jsonBoardPreferences.SelfJoin = value;
				Parameters.Add("value", _jsonBoardPreferences.SelfJoin.ToLowerString());
				Put(EntityRequestType.BoardPreferences_Write_AllowsSelfJoin);
			}
		}
		/// <summary>
		/// Gets or sets who may comment on cards.
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

				Validator.Writable();
				if (_jsonBoardPreferences == null) return;
				if (_comments == value) return;
				_comments = value;
				UpdateApiComments();
				Parameters.Add("value", _jsonBoardPreferences.Comments);
				Put(EntityRequestType.BoardPreferences_Write_Comments);
			}
		}
		/// <summary>
		/// Gets or sets who may extend invitations to join the board.
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

				Validator.Writable();
				if (_jsonBoardPreferences == null) return;
				if (_invitations == value) return;
				_invitations = value;
				UpdateApiInvitations();
				Parameters.Add("value", _jsonBoardPreferences.Invitations);
				Put(EntityRequestType.BoardPreferences_Write_Invitations);
			}
		}
		/// <summary>
		/// Gets or sets who may view the board.
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

				Validator.Writable();
				if (_jsonBoardPreferences == null) return;
				if (_permissionLevel == value) return;
				_permissionLevel = value;
				UpdateApiPermissionLevel();
				Parameters.Add("value", _jsonBoardPreferences.PermissionLevel);
				Put(EntityRequestType.BoardPreferences_Write_PermissionLevel);
			}
		}
		/// <summary>
		/// Gets or sets whether card covers are shown on the board.
		/// </summary>
		public bool? ShowCardCovers
		{
			get
			{
				VerifyNotExpired();
				return (_jsonBoardPreferences == null) ? null : _jsonBoardPreferences.CardCovers;
			}
			set
			{

				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoardPreferences == null) return;
				if (_jsonBoardPreferences.CardCovers == value) return;
				_jsonBoardPreferences.CardCovers = value;
				Parameters.Add("value", _jsonBoardPreferences.CardCovers.ToLowerString());
				Put(EntityRequestType.BoardPreferences_Write_ShowCardCovers);
			}
		}
		/// <summary>
		/// Gets or sets who may vote on cards.
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

				Validator.Writable();
				if (_jsonBoardPreferences == null) return;
				if (_voting == value) return;
				_voting = value;
				UpdateApiVoting();
				Parameters.Add("value", _jsonBoardPreferences.Voting);
				Put(EntityRequestType.BoardPreferences_Write_Voting);
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonBoardPreferences is InnerJsonBoardPreferences; } }

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
		public BoardPreferences()
		{
			_jsonBoardPreferences = new InnerJsonBoardPreferences();
		}
		internal BoardPreferences(Board owner)
			: this()
		{
			Owner = owner;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			Parameters.Add("_boardId", Owner.Id);
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.BoardPreferences_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonBoardPreferences = (IJsonBoardPreferences)obj;
			UpdateComments();
			UpdateInvitations();
			UpdatePermissionLevel();
			UpdateVoting();
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}

		private void Put(EntityRequestType requestType)
		{
			Parameters.Add("_boardId", Owner.Id);
			EntityRepository.Upload(requestType, Parameters);
			Parameters.Clear();
		}
		private void UpdateComments()
		{
			_comments = _commentMap.Any(kvp => kvp.Value == _jsonBoardPreferences.Comments)
			            	? _commentMap[_jsonBoardPreferences.Comments]
			            	: BoardCommentType.Unknown;
		}
		private void UpdateApiComments()
		{
			if (_commentMap.Any(kvp => kvp.Key == _comments))
				_jsonBoardPreferences.Comments = _commentMap[_comments];
		}
		private void UpdateInvitations()
		{
			_invitations = _invitationMap.Any(kvp => kvp.Value == _jsonBoardPreferences.Invitations)
			               	? _invitationMap[_jsonBoardPreferences.Invitations]
			               	: BoardInvitationType.Unknown;
		}
		private void UpdateApiInvitations()
		{
			if (_invitationMap.Any(kvp => kvp.Key == _invitations))
				_jsonBoardPreferences.Invitations = _invitationMap[_invitations];
		}
		private void UpdatePermissionLevel()
		{
			_permissionLevel = _permissionMap.Any(kvp => kvp.Value == _jsonBoardPreferences.PermissionLevel)
			                   	? _permissionMap[_jsonBoardPreferences.PermissionLevel]
			                   	: BoardPermissionLevelType.Unknown;
		}
		private void UpdateApiPermissionLevel()
		{
			if (_permissionMap.Any(kvp => kvp.Key == _permissionLevel))
				_jsonBoardPreferences.PermissionLevel = _permissionMap[_permissionLevel];
		}
		private void UpdateVoting()
		{
			_voting = _votingMap.Any(kvp => kvp.Value == _jsonBoardPreferences.Voting)
			          	? _votingMap[_jsonBoardPreferences.Voting]
			          	: BoardVotingType.Unknown;
		}
		private void UpdateApiVoting()
		{
			if (_votingMap.Any(kvp => kvp.Key == _voting))
				_jsonBoardPreferences.Voting = _votingMap[_voting];
		}
	}
}