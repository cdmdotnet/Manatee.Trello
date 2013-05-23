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
 
	File Name:		AddAdminToOrganizationNotification.cs
	Namespace:		Manatee.Trello
	Class Name:		AddAdminToOrganizationNotification
	Purpose:		Provides notification that a member was added to an organization
					as an admin.

***************************************************************************************/
using System.Diagnostics;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides notification that a member added to an organization as an admin.
	/// </summary>
	public class AddAdminToOrganizationNotification : Notification
	{
		/// <summary>
		/// Creates a new instance of the AddAdminToOrganizationNotification class.
		/// </summary>
		/// <param name="notification">The base notification</param>
		public AddAdminToOrganizationNotification(Notification notification)
			: base(notification.Svc, notification.Id)
		{
			Debug.Assert(false, string.Format(
				"{0} is not yet configured.  Please post the JSON returned by http://api.trello.com/notifications/{1} to https://trello.com/c/NX8xPcP6",
				GetType().Name, notification.Id));
			VerifyNotExpired();
		}
	}
}