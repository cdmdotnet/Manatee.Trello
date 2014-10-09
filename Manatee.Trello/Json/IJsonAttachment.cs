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
 
	File Name:		IJsonAttachment.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonAttachment
	Purpose:		Defines the JSON structure for the Attachment object.

***************************************************************************************/
using System;
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Attachment object.
	/// </summary>
	public interface IJsonAttachment : IJsonCacheable
	{
		///<summary>
		/// Gets or sets the size of the attachment.
		///</summary>
		[JsonDeserialize]
		int? Bytes { get; set; }
		/// <summary>
		/// Gets or sets the date on which the attachment was created.
		/// </summary>
		[JsonDeserialize]
		DateTime? Date { get; set; }
		///<summary>
		/// Gets or sets the ID of the member who created the attachment.
		///</summary>
		[JsonDeserialize]
		IJsonMember Member { get; set; }
		///<summary>
		/// ?
		///</summary>
		[JsonDeserialize]
		bool? IsUpload { get; set; }
		///<summary>
		/// Gets or sets the type of attachment.
		///</summary>
		[JsonDeserialize]
		string MimeType { get; set; }
		///<summary>
		/// Gets or sets the name of the attachment.
		///</summary>
		[JsonDeserialize]
		string Name { get; set; }
		///<summary>
		/// Gets or sets a collection of previews for the attachment.
		///</summary>
		[JsonDeserialize]
		List<IJsonImagePreview> Previews { get; set; }
		///<summary>
		/// Gets or sets the attachment storage location.
		///</summary>
		[JsonDeserialize]
		string Url { get; set; }
	}
}