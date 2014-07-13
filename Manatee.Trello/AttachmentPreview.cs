/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		AttachmentPreview.cs
	Namespace:		Manatee.Trello
	Class Name:		AttachmentPreview
	Purpose:		Represents a preview for an attachment on a card.

***************************************************************************************/

using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class AttachmentPreview
	{
		public string Id { get; private set; }
		public int? Height { get; private set; }
		public bool? IsScaled { get; set; }
		public string Url { get; private set; }
		public int? Width { get; private set; }

		internal AttachmentPreview(IJsonAttachmentPreview json)
		{
			Id = json.Id;
			Height = json.Height;
			IsScaled = json.Scaled;
			Url = json.Url;
			Width = json.Width;
		}
	}
}