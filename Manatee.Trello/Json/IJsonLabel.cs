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
 
	File Name:		IJsonLabel.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonLabel
	Purpose:		Defines the JSON structure for the Label object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Label object.
	/// </summary>
	public interface IJsonLabel
	{
		/// <summary>
		/// Gets and sets the color of the label.
		/// </summary>
		LabelColor Color { get; set; }
		/// <summary>
		/// Gets and sets the name of the label.
		/// </summary>
		string Name { get; set; }
	}
}