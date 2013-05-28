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
 
	File Name:		IJsonNotificationData.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonNotificationData
	Purpose:		Defines the JSON structure for the ActionData object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the ActionData object.
	/// </summary>
	public interface IJsonActionData
	{
		/// <summary>
		/// Contains the raw JSON data.  The type of this object will be determined
		/// by the JSON serializer implementation.
		/// </summary>
		object RawData { get; set; }

		/// <summary>
		/// Attempts to retrieve a string from an object.
		/// </summary>
		/// <param name="path">
		/// The set of successive keys to use while drilling down
		/// through successive JSON object structures.
		/// </param>
		/// <returns>
		/// The string value of the item at the specified path,
		/// or null if the path does not exist.
		/// </returns>
		string TryGetString(params string[] path);
		/// <summary>
		/// Attempts to retrieve a number from an object.
		/// </summary>
		/// <param name="path">
		/// The set of successive keys to use while drilling down
		/// through successive JSON object structures.
		/// </param>
		/// <returns>
		/// The number value of the item at the specified path,
		/// or null if the path does not exist.
		/// </returns>
		double? TryGetNumber(params string[] path);
		/// <summary>
		/// Attempts to retrieve a boolean from an object.
		/// </summary>
		/// <param name="path">
		/// The set of successive keys to use while drilling down
		/// through successive JSON object structures.
		/// </param>
		/// <returns>
		/// The boolean value of the item at the specified path,
		/// or null if the path does not exist.
		/// </returns>
		bool? TryGetBoolean(params string[] path);
		/// <summary>
		/// Attempts to retrieve the JSON data to represent an attachment from an object.
		/// </summary>
		/// <param name="path">
		/// The set of successive keys to use while drilling down
		/// through successive JSON object structures.
		/// </param>
		/// <returns>
		/// The IJsonAttachment value of the item at the specified path,
		/// or null if the path does not exist.
		/// </returns>
		IJsonAttachment TryGetAttachment(params string[] path);
	}
}