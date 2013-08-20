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
 
	File Name:		ISerializer.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		ISerializer
	Purpose:		Defines methods required by the IRestClient to serialize an
					object to JSON.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines methods required by the IRestClient to serialize an object to JSON.
	/// </summary>
	public interface ISerializer
	{
		/// <summary>
		/// Serializes an object to JSON.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>An equivalent JSON string.</returns>
		string Serialize(object obj);
	}
}
