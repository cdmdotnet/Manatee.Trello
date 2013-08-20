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
 
	File Name:		Method.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		Method
	Purpose:		Enumerates the RESTful call methods required by TrelloService.

***************************************************************************************/
namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Enumerates the RESTful call methods required by TrelloService.
	/// </summary>
	public enum RestMethod
	{
		///<summary>
		/// Indicates an HTTP GET operation.
		///</summary>
		Get,
		///<summary>
		/// Indicates an HTTP PUT operation.
		///</summary>
		Put,
		///<summary>
		/// Indicates an HTTP POST operation.
		///</summary>
		Post,
		///<summary>
		/// Indicates an HTTP DELETE operation.
		///</summary>
		Delete
	}
}