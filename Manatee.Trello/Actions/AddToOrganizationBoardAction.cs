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
 
	File Name:		AddToOrganizationBoardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		AddToOrganizationBoardAction
	Purpose:		Indicates a board was added to an organization.

***************************************************************************************/
using Manatee.Json.Extensions;

namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a board was added to an organization.
	/// </summary>
	public class AddToOrganizationBoardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Organization _organization;
		private readonly string _organizationId;

		/// <summary>
		/// Gets the board associated with the action.
		/// </summary>
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}
		/// <summary>
		/// Gets the member associated with the action.
		/// </summary>
		public Organization Organization
		{
			get
			{
				VerifyNotExpired();
				return ((_organization == null) || (_organization.Id != _organizationId)) && (Svc != null) ? (_organization = Svc.Retrieve<Organization>(_organizationId)) : _organization;
			}
		}

		/// <summary>
		/// Creates a new instance of the AddToOrganizationBoardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public AddToOrganizationBoardAction(Action action)
			: base(action.Svc, action.Id)
		{
			_boardId = action.Data.Object.TryGetObject("board").TryGetString("id");
			_organizationId = action.Data.Object.TryGetObject("organization").TryGetString("id");
		}
	}
}