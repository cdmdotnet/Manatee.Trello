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
 
	File Name:		AttachmentPreview.cs
	Namespace:		Manatee.Trello
	Class Name:		AttachmentPreview
	Purpose:		Represents a thumbnail preview of a card attachment on Trello.com.

***************************************************************************************/
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Serialization;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	public class AttachmentPreview : IJsonCompatible
	{
		public string Id { get; set; }
		public int Height { get; set; }
		public string Url { get; set; }
		public int Width { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Height = (int) obj.TryGetNumber("height");
			Url = obj.TryGetString("url");
			Width = (int) obj.TryGetNumber("width");
		}
		public JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"height", Height},
			           		{"url", Url},
			           		{"width", Width}
			           	};
			return json;
		}
	}
}