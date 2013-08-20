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
 
	File Name:		IJsonOrganization.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonOrganization
	Purpose:		Defines the JSON structure for the Organization object.

***************************************************************************************/
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Organization object.
	/// </summary>
	public interface IJsonOrganization : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the name of the organization.
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the name to be displayed for the organization.
		/// </summary>
		string DisplayName { get; set; }
		/// <summary>
		/// Gets or sets the description for the organization.
		/// </summary>
		string Desc { get; set; }
		/// <summary>
		/// Gets or sets the URL to the organization's profile.
		/// </summary>
		string Url { get; set; }
		/// <summary>
		/// Gets or sets the organization's website.
		/// </summary>
		string Website { get; set; }
		/// <summary>
		/// Gets or sets the organization's logo hash.
		/// </summary>
		string LogoHash { get; set; }
		/// <summary>
		/// Enumerates the powerups obtained by the organization.
		/// </summary>
		List<int> PowerUps { get; set; }
		/// <summary>
		/// Gets or sets whether the organization is a paid account.
		/// </summary>
		bool? PaidAccount { get; set; }
		/// <summary>
		/// Gets or sets a collection of premium features available to the organization.
		/// </summary>
		List<string> PremiumFeatures { get; set; }
	}
}