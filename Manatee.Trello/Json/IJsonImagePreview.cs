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
 
	File Name:		IJsonImagePreview.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonImagePreview
	Purpose:		Defines the JSON structure for the AttachmentPreview object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the AttachmentPreview object.
	/// </summary>
	public interface IJsonImagePreview : IJsonCacheable
	{
		///<summary>
		/// Gets or sets the width in pixels of the attachment preview.
		///</summary>
		[JsonDeserialize]
		int? Width { get; set; }
		///<summary>
		/// Gets or sets the height in pixels of the attachment preview.
		///</summary>
		[JsonDeserialize]
		int? Height { get; set; }
		///<summary>
		/// Gets or sets the attachment storage location.
		///</summary>
		[JsonDeserialize]
		string Url { get; set; }
		/// <summary>
		/// Gets or sets whether the attachment was scaled to produce the preview.
		/// </summary>
		[JsonDeserialize]
		bool? Scaled { get; set; }
	}
}