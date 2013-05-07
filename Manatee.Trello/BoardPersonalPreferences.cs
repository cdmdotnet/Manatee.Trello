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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	//{
	//   "showSidebar":true,
	//   "showSidebarMembers":true,
	//   "showSidebarBoardActions":true,
	//   "showSidebarActivity":true,
	//   "showListGuide":false
	//}
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
				return (_jsonBoardPersonalPreferences == null) ? null : _jsonBoardPersonalPreferences.ShowListGuide;
			}
			set
			{
				Validate.Writable(Svc);
				Validate.Nullable(value);
				if (_jsonBoardPersonalPreferences == null) return;
				if (_jsonBoardPersonalPreferences.ShowListGuide == value) return;
				_jsonBoardPersonalPreferences.ShowListGuide = value;
				Parameters.Add("showListGuide", _jsonBoardPersonalPreferences.ShowListGuide.ToLowerString());
				Post();
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
				return (_jsonBoardPersonalPreferences == null) ? null : _jsonBoardPersonalPreferences.ShowSidebar;
			}
			set
			{
				Validate.Writable(Svc);
				Validate.Nullable(value);
				if (_jsonBoardPersonalPreferences == null) return;
				if (_jsonBoardPersonalPreferences.ShowSidebar == value) return;
				_jsonBoardPersonalPreferences.ShowSidebar = value;
				Parameters.Add("showSidebar", _jsonBoardPersonalPreferences.ShowSidebar.ToLowerString());
				Post();
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
				return (_jsonBoardPersonalPreferences == null) ? null : _jsonBoardPersonalPreferences.ShowSidebarActivity;
			}
			set
			{
				Validate.Writable(Svc);
				Validate.Nullable(value);
				if (_jsonBoardPersonalPreferences == null) return;
				if (_jsonBoardPersonalPreferences.ShowSidebarActivity == value) return;
				_jsonBoardPersonalPreferences.ShowSidebarActivity = value;
				Parameters.Add("showSidebarActivity", _jsonBoardPersonalPreferences.ShowSidebarActivity.ToLowerString());
				Post();
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
				return (_jsonBoardPersonalPreferences == null) ? null : _jsonBoardPersonalPreferences.ShowSidebarBoardActions;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonBoardPersonalPreferences.ShowSidebarBoardActions == value) return;
				Validate.Nullable(value);
				_jsonBoardPersonalPreferences.ShowSidebarBoardActions = value;
				Parameters.Add("showSidebarBoardActions", _jsonBoardPersonalPreferences.ShowSidebarBoardActions.ToLowerString());
				Post();
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
				return (_jsonBoardPersonalPreferences == null) ? null : _jsonBoardPersonalPreferences.ShowSidebarMembers;
			}
			set
			{
				Validate.Writable(Svc);
				Validate.Nullable(value);
				if (_jsonBoardPersonalPreferences == null) return;
				if (_jsonBoardPersonalPreferences.ShowSidebarMembers == value) return;
				_jsonBoardPersonalPreferences.ShowSidebarMembers = value;
				Parameters.Add("showSidebarMembers", _jsonBoardPersonalPreferences.ShowSidebarMembers.ToLowerString());
				Post();
			}
		}

		internal static string TypeKey { get { return "myPrefs"; } }
		internal override string Key { get { return TypeKey; } }

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
		protected override void Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			ApplyJson(Api.Get<IJsonBoardPersonalPreferences>(request));
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService() {}

		internal override void ApplyJson(object obj)
		{
			_jsonBoardPersonalPreferences = (IJsonBoardPersonalPreferences) obj;
		}

		private void Post()
		{
			if (Svc == null) return;
			var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Post<IJsonBoardPersonalPreferences>(request);
		}
	}
}
