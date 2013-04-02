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
 
	File Name:		MakeAdminOfOrganizationNotification.cs
	Namespace:		Manatee.Trello
	Class Name:		MakeAdminOfOrganizationNotification
	Purpose:		Provides notification that the current member was
					made an admin of an organization.

***************************************************************************************/
using Manatee.Json.Extensions;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides notification that the current member was made an admin of an organization.
	/// </summary>
	public class MakeAdminOfOrganizationNotification : Notification
	{
		private Organization _organization;
		private readonly string _organizationId;

		/// <summary>
		/// Gets the board associated with the notification.
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
		/// Creates a new instance of the MakeAdminOfOrganizationNotification class.
		/// </summary>
		/// <param name="notification">The base notification</param>
		public MakeAdminOfOrganizationNotification(Notification notification)
			: base(notification.Svc, notification.Id)
		{
			_organizationId = notification.Data.Object.TryGetObject("organization").TryGetString("id");
			Refresh(notification);
		}
	}
}