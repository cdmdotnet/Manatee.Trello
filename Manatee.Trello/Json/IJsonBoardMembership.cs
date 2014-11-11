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
 
	File Name:		IJsonBoardMembership.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonBoardMembership
	Purpose:		Defines the JSON structure for the BoardMembership object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the BoardMembership object.
	/// </summary>
	public interface IJsonBoardMembership : IJsonCacheable
	{
		///<summary>
		/// Gets or sets the ID of the member.
		///</summary>
		[JsonDeserialize]
		IJsonMember Member { get; set; }
		///<summary>
		/// Gets or sets the membership type.
		///</summary>
		[JsonDeserialize]
		[JsonSerialize(IsRequired = true)]
		BoardMembershipType? MemberType { get; set; }
		///<summary>
		/// Gets or sets whether the membership is deactivated.
		///</summary>
		[JsonDeserialize]
		bool? Deactivated { get; set; }
	}
}