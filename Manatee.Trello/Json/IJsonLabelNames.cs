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
 
	File Name:		IJsonLabelNames.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonLabelNames
	Purpose:		Defines the JSON structure for the LabelNames object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a LabelNames object.
	/// </summary>
	public interface IJsonLabelNames
	{
		/// <summary>
		/// Gets and sets the name for the red label.
		/// </summary>
		string Red { get; set; }
		/// <summary>
		/// Gets and sets the name for the orange label.
		/// </summary>
		string Orange { get; set; }
		/// <summary>
		/// Gets and sets the name for the yellow label.
		/// </summary>
		string Yellow { get; set; }
		/// <summary>
		/// Gets and sets the name for the green label.
		/// </summary>
		string Green { get; set; }
		/// <summary>
		/// Gets and sets the name for the blue label.
		/// </summary>
		string Blue { get; set; }
		/// <summary>
		/// Gets and sets the name for the purple label.
		/// </summary>
		string Purple { get; set; }
	}
}