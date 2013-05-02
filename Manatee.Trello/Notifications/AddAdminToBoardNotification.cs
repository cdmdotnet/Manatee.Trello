﻿/***************************************************************************************

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
 
	File Name:		AddAdminToBoardNotification.cs
	Namespace:		Manatee.Trello
	Class Name:		AddAdminToBoardNotification
	Purpose:		Provides notification that a member was added to a board
					as an admin.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Provides notification that a member added to a board as an admin.
	/// </summary>
	public class AddAdminToBoardNotification : Notification
	{
		/// <summary>
		/// Creates a new instance of the AddAdminToBoardNotification class.
		/// </summary>
		/// <param name="notification">The base notification</param>
		public AddAdminToBoardNotification(Notification notification)
			: base(notification.Svc, notification.Id)
		{
			VerifyNotExpired();
		}
	}
}