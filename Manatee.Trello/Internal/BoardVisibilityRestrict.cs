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
 
	File Name:		BoardVisibilityRestrict.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		BoardVisibilityRestrict
	Purpose:		Represents the visibility of boards contained within an
					organization.

***************************************************************************************/
using System.Linq;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal
{
	internal class BoardVisibilityRestrict
	{
		private static readonly OneToOneMap<BoardPermissionLevelType, string> _permissionMap;

		private readonly IJsonBoardVisibilityRestrict _jsonBoardVisbilityRestrict;
		private BoardPermissionLevelType _private = BoardPermissionLevelType.Unknown;
		private BoardPermissionLevelType _org = BoardPermissionLevelType.Unknown;
		private BoardPermissionLevelType _public = BoardPermissionLevelType.Unknown;

		public BoardPermissionLevelType Private
		{
			get { return _private; }
			set
			{
				_private = value;
				UpdateApiPrivate();
			}
		}
		public BoardPermissionLevelType Org
		{
			get { return _org; }
			set
			{
				_org = value;
				UpdateApiOrg();
			}
		}
		public BoardPermissionLevelType Public
		{
			get { return _public; }
			set
			{
				_public = value;
				UpdateApiPublic();
			}
		}

		static BoardVisibilityRestrict()
		{
			_permissionMap = new OneToOneMap<BoardPermissionLevelType, string>
			                 	{
			                 		{BoardPermissionLevelType.Private, "private"},
			                 		{BoardPermissionLevelType.Org, "org"},
			                 		{BoardPermissionLevelType.Public, "public"},
			                 	};
		}
		internal BoardVisibilityRestrict(IJsonBoardVisibilityRestrict jsonBoardVisibilityRestrict)
		{
			_jsonBoardVisbilityRestrict = jsonBoardVisibilityRestrict ?? new InnerJsonBoardVisibilityRestrict();
			UpdatePrivate();
			UpdateOrg();
			UpdatePublic();
		}

		private void UpdatePrivate()
		{
			_private = _permissionMap.Any(kvp => kvp.Value == _jsonBoardVisbilityRestrict.Private)
			           	? _permissionMap[_jsonBoardVisbilityRestrict.Private]
			           	: BoardPermissionLevelType.Unknown;
		}
		private void UpdateApiPrivate()
		{
			if (_permissionMap.Any(kvp => kvp.Key == _private))
				_jsonBoardVisbilityRestrict.Private = _permissionMap[_private];
		}
		private void UpdateOrg()
		{
			_org = _permissionMap.Any(kvp => kvp.Value == _jsonBoardVisbilityRestrict.Org)
			       	? _permissionMap[_jsonBoardVisbilityRestrict.Org]
			       	: BoardPermissionLevelType.Unknown;
		}
		private void UpdateApiOrg()
		{
			if (_permissionMap.Any(kvp => kvp.Key == _org))
				_jsonBoardVisbilityRestrict.Org = _permissionMap[_org];
		}
		private void UpdatePublic()
		{
			_public = _permissionMap.Any(kvp => kvp.Value == _jsonBoardVisbilityRestrict.Public)
			          	? _permissionMap[_jsonBoardVisbilityRestrict.Public]
			          	: BoardPermissionLevelType.Unknown;
		}
		private void UpdateApiPublic()
		{
			if (_permissionMap.Any(kvp => kvp.Key == _public))
				_jsonBoardVisbilityRestrict.Public = _permissionMap[_public];
		}
	}
}
