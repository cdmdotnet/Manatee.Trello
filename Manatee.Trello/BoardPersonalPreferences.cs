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
 
	File Name:		BoardPersonalPreferences.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardPersonalPreferences
	Purpose:		Represents a member's viewing preferences for a board
					on Trello.com.

***************************************************************************************/

using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	///<summary>
	/// Represents a member's viewing preferences for a board
	///</summary>
	public class BoardPersonalPreferences : ExpiringObject
	{
		private IJsonBoardPersonalPreferences _jsonBoardPersonalPreferences;

		///<summary>
		/// Gets or sets whether the list guide (left side of the screen) is expanded.
		///</summary>
		/// <remarks>
		/// The option to show the list guide is only active when horizontal scrolling is enabled.
		/// </remarks>
		public bool? ShowListGuide
		{
			get
			{
				VerifyNotExpired();
				return _jsonBoardPersonalPreferences.ShowListGuide;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoardPersonalPreferences.ShowListGuide == value) return;
				_jsonBoardPersonalPreferences.ShowListGuide = value;
				Parameters.Add("name", "showListGuide");
				Parameters.Add("value", _jsonBoardPersonalPreferences.ShowListGuide.ToLowerString());
				Upload(EntityRequestType.BoardPersonalPreferences_Write_ShowListGuide);
			}
		}
		///<summary>
		/// Gets or sets whether the side bar (right side of the screen) is shown
		///</summary>
		public bool? ShowSidebar
		{
			get
			{
				VerifyNotExpired();
				return _jsonBoardPersonalPreferences.ShowSidebar;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoardPersonalPreferences.ShowSidebar == value) return;
				_jsonBoardPersonalPreferences.ShowSidebar = value;
				Parameters.Add("name", "showSidebar");
				Parameters.Add("value", _jsonBoardPersonalPreferences.ShowSidebar.ToLowerString());
				Upload(EntityRequestType.BoardPersonalPreferences_Write_ShowSidebar);
			}
		}
		/// <summary>
		/// Gets or sets whether the activity section of the side bar is shown.
		/// </summary>
		public bool? ShowSidebarActivity
		{
			get
			{
				VerifyNotExpired();
				return _jsonBoardPersonalPreferences.ShowSidebarActivity;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoardPersonalPreferences.ShowSidebarActivity == value) return;
				_jsonBoardPersonalPreferences.ShowSidebarActivity = value;
				Parameters.Add("name", "showSidebarActivity");
				Parameters.Add("value", _jsonBoardPersonalPreferences.ShowSidebarActivity.ToLowerString());
				Upload(EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarActivity);
			}
		}
		/// <summary>
		/// Gets or sets whether the board actions (Add List/Add Member/Filter Cards) section of the side bar is shown.
		/// </summary>
		public bool? ShowSidebarBoardActions
		{
			get
			{
				VerifyNotExpired();
				return _jsonBoardPersonalPreferences.ShowSidebarBoardActions;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoardPersonalPreferences.ShowSidebarBoardActions == value) return;
				_jsonBoardPersonalPreferences.ShowSidebarBoardActions = value;
				Parameters.Add("name", "showSidebarBoardActions");
				Parameters.Add("value", _jsonBoardPersonalPreferences.ShowSidebarBoardActions.ToLowerString());
				Upload(EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarBoardActions);
			}
		}
		///<summary>
		/// Gets or sets whether the members section of the list of the side bar is shown.
		///</summary>
		public bool? ShowSidebarMembers
		{
			get
			{
				VerifyNotExpired();
				return _jsonBoardPersonalPreferences.ShowSidebarMembers;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoardPersonalPreferences.ShowSidebarMembers == value) return;
				_jsonBoardPersonalPreferences.ShowSidebarMembers = value;
				Parameters.Add("name", "showSidebarMembers");
				Parameters.Add("value", _jsonBoardPersonalPreferences.ShowSidebarMembers.ToLowerString());
				Upload(EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarMembers);
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonBoardPersonalPreferences is InnerJsonBoardPersonalPreferences; } }

		/// <summary>
		/// Creates a new instance of the CheckList class.
		/// </summary>
		public BoardPersonalPreferences()
		{
			_jsonBoardPersonalPreferences = new InnerJsonBoardPersonalPreferences();
		}
		internal BoardPersonalPreferences(Board owner)
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
			return EntityRepository.Refresh(this, EntityRequestType.BoardPersonalPreferences_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonBoardPersonalPreferences = (IJsonBoardPersonalPreferences)obj;
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}

		private void Upload(EntityRequestType requestType)
		{
			Parameters.Add("_boardId", Owner.Id);
			EntityRepository.Upload(requestType, Parameters);
		}
	}
}
