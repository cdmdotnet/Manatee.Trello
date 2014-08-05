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
 
	File Name:		IJsonMemberPreferences.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonMemberPreferences
	Purpose:		Defines the JSON structure for the MemberPreferences object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the MemberPreferences object.
	/// </summary>
	public interface IJsonMemberPreferences
	{
		/// <summary>
		/// Gets or sets the number of minutes between summary emails.
		/// </summary>
		[JsonDeserialize]
		int? MinutesBetweenSummaries { get; set; }
		/// <summary>
		/// Enables/disables color-blind mode.
		/// </summary>
		[JsonDeserialize]
		bool? ColorBlind { get; set; }
	}
}