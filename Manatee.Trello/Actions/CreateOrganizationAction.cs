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
 
	File Name:		CreateOrganizationAction.cs
	Namespace:		Manatee.Trello
	Class Name:		CreateOrganizationAction
	Purpose:		Indicates an organization was created.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates an organization was created.
	/// </summary>
	public class CreateOrganizationAction : Action
	{
		private Organization _organization;
		private readonly string _organizationId;
		private readonly string _organizationName;
		private string _stringFormat;

		/// <summary>
		/// Gets the organization associated with the action.
		/// </summary>
		public Organization Organization
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_organization == null) || (_organization.Id != _organizationId)) && (Svc != null)
				       	? (_organization = Svc.Retrieve<Organization>(_organizationId))
				       	: _organization;
			}
		}
		/// <summary>
		/// Creates a new instance of the CreateOrganizationAction class.
		/// </summary>
		/// <param name="action"></param>
		public CreateOrganizationAction(Action action)
		{
			VerifyNotExpired();
			_organizationId = action.Data.TryGetString("organization", "id");
			_organizationName = action.Data.TryGetString("organization", "name");
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
			return _stringFormat ?? (_stringFormat = string.Format("{0} created organization '{1}' on {2}",
																   MemberCreator.FullName,
																   Organization != null ? Organization.Name : _organizationName,
																   Date));
		}
	}
}