/***************************************************************************************

	Copyright 2014 Greg Dennis

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
	Purpose:		Represents the user-specific preferences for a board.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the user-specific preferences for a board.
	/// </summary>
	public class BoardPersonalPreferences
	{
		private readonly Field<List> _emailList;
		private readonly Field<Position> _emailPosition;
		private readonly Field<bool?> _showListGuide;
		private readonly Field<bool?> _showSidebar;
		private readonly Field<bool?> _showSidebarActivity;
		private readonly Field<bool?> _showSidebarBoardActions;
		private readonly Field<bool?> _showSidebarMembers;
		private readonly BoardPersonalPreferencesContext _context;

		public List EmailList
		{
			get { return _emailList.Value; }
			set { _emailList.Value = value; }
		}
		public Position EmailPosition
		{
			get { return _emailPosition.Value; }
			set { _emailPosition.Value = value; }
		}
		public bool? ShowListGuide
		{
			get { return _showListGuide.Value; }
			set { _showListGuide.Value = value; }
		}
		public bool? ShowSidebar
		{
			get { return _showSidebar.Value; }
			set { _showSidebar.Value = value; }
		}
		public bool? ShowSidebarActivity
		{
			get { return _showSidebarActivity.Value; }
			set { _showSidebarActivity.Value = value; }
		}
		public bool? ShowSidebarBoardActions
		{
			get { return _showSidebarBoardActions.Value; }
			set { _showSidebarBoardActions.Value = value; }
		}
		public bool? ShowSidebarMembers
		{
			get { return _showSidebarMembers.Value; }
			set { _showSidebarMembers.Value = value; }
		}

		internal BoardPersonalPreferences(string ownerId)
		{
			_context = new BoardPersonalPreferencesContext(ownerId);

			_emailList = new Field<List>(_context, () => EmailList);
			_emailList.AddRule(NotNullRule<List>.Instance);
			_emailPosition = new Field<Position>(_context, () => EmailPosition);
			_emailPosition.AddRule(NotNullRule<Position>.Instance);
			_showListGuide = new Field<bool?>(_context, () => ShowListGuide);
			_showListGuide.AddRule(NullableHasValueRule<bool>.Instance);
			_showSidebar = new Field<bool?>(_context, () => ShowSidebar);
			_showSidebar.AddRule(NullableHasValueRule<bool>.Instance);
			_showSidebarActivity = new Field<bool?>(_context, () => ShowSidebarActivity);
			_showSidebarActivity.AddRule(NullableHasValueRule<bool>.Instance);
			_showSidebarBoardActions = new Field<bool?>(_context, () => ShowSidebarBoardActions);
			_showSidebarBoardActions.AddRule(NullableHasValueRule<bool>.Instance);
			_showSidebarMembers = new Field<bool?>(_context, () => ShowSidebarMembers);
			_showSidebarMembers.AddRule(NullableHasValueRule<bool>.Instance);
		}
	}
}