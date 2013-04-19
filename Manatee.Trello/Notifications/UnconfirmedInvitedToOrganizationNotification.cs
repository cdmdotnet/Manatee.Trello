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
 
	File Name:		UnconfirmedInvitedToOrganizationNotification.cs
	Namespace:		Manatee.Trello
	Class Name:		UnconfirmedInvitedToOrganizationNotification
	Purpose:		Provides notification that an unconfirmed member was invited
					to an organization.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Provides notification that an unconfirmed member was invited to an organization.
	/// </summary>
	public class UnconfirmedInvitedToOrganizationNotification : Notification 
	{
		/// <summary>
		/// Creates a new instance of the UnconfirmedInvitedToOrganizationNotification class.
		/// </summary>
		/// <param name="notification">The base notification</param>
		public UnconfirmedInvitedToOrganizationNotification(Notification notification)
			: base(notification.Svc, notification.Id)
		{
			
		}
	}
}