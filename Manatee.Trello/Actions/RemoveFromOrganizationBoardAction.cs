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
 
	File Name:		RemoveFromOrganizationBoardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		RemoveFromOrganizationBoardAction
	Purpose:		Indicates a board was removed from an organization.

***************************************************************************************/
using Manatee.Json.Extensions;

namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a board was removed from an organization.
	/// </summary>
	public class RemoveFromOrganizationBoardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Organization _organization;
		private readonly string _organizationId;
		private readonly string _boardName;
		private readonly string _organizationName;

		/// <summary>
		/// Gets the board associated with the action.
		/// </summary>
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Get(Svc.RequestProvider.Create<Board>(_boardId))) : _board;
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
				return ((_organization == null) || (_organization.Id != _organizationId)) && (Svc != null) ? (_organization = Svc.Get(Svc.RequestProvider.Create<Organization>(_organizationId))) : _organization;
			}
		}

		/// <summary>
		/// Creates a new instance of the RemoveFromOrganizationBoardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public RemoveFromOrganizationBoardAction(Action action)
			: base(action.Svc, action.Id)
		{
			Refresh(action);
			_boardId = action.Data.Object.TryGetObject("board").TryGetString("id");
			_boardName = action.Data.Object.TryGetObject("board").TryGetString("name");
			_organizationId = action.Data.Object.TryGetObject("organization").TryGetString("id");
			_organizationName = action.Data.Object.TryGetObject("organization").TryGetString("name");
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("{0} removed board '{1}' from organization '{2}' on {3}",
								 MemberCreator.FullName,
								 Board != null ? Board.Name : _boardName,
								 Organization != null ? Organization.Name : _organizationName,
								 Date);
		}
	}
}