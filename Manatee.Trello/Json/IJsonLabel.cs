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
	public interface IJsonLabel : IJsonCacheable
	{
		/// <summary>
		/// Gets and sets the board on which the label is defined.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets and sets the color of the label.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		LabelColor? Color { get; set; }
		/// <summary>
		/// Determines if the color property should be submitted even if it is null.
		/// </summary>
		/// <remarks>
		/// This property is not part of the JSON structure.
		/// </remarks>
		bool ForceNullColor { set; }
		/// <summary>
		/// Gets and sets the name of the label.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets and sets how many cards use this label.
		/// </summary>
		[JsonDeserialize]
		int? Uses { get; set; }
	}
}