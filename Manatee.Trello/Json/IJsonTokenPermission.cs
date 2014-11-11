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
 
	File Name:		TokenPermission.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		TokenPermission
	Purpose:		Defines the JSON structure for the TokenPermission object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the TokenPermission object.
	/// </summary>
	public interface IJsonTokenPermission
	{
		/// <summary>
		/// Gets or sets the ID of the model to which a token grants permissions.
		/// </summary>
		[JsonDeserialize]
		string IdModel { get; set; }
		/// <summary>
		/// Gets or sets the type of the model.
		/// </summary>
		[JsonDeserialize]
		TokenModelType? ModelType { get; set; }
		/// <summary>
		/// Gets or sets whether a token grants read permissions to the model.
		/// </summary>
		[JsonDeserialize]
		bool? Read { get; set; }
		/// <summary>
		/// Gets or sets whether a token grants write permissions to the model.
		/// </summary>
		[JsonDeserialize]
		bool? Write { get; set; }
	}
}