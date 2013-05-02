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
 
	File Name:		IJsonAction.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonAction
	Purpose:		Defines the JSON structure for the Action object.

***************************************************************************************/
using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Action object.
	/// </summary>
	public interface IJsonAction
	{
		/// <summary>
		/// Gets or sets a unique identifier (not necessarily a GUID).
		/// </summary>
		string Id { get; set; }
		/// <summary>
		/// Gets or sets the ID of the member who performed the action.
		/// </summary>
		string IdMemberCreator { get; set; }
		/// <summary>
		/// Gets or sets the data associated with the action.  Contents depend upon the action's type.
		/// </summary>
		IJsonActionData Data { get; set; }
		/// <summary>
		/// Gets or sets the action's type.
		/// </summary>
		string Type { get; set; }
		///<summary>
		/// Gets or sets the date on which the action was performed.
		///</summary>
		DateTime? Date { get; set; }
	}
}