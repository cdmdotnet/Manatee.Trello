/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateeSticker.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeSticker
	Purpose:		Implements IJsonSticker for Manatee.Json

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeSticker : IJsonSticker, IJsonSerializable
	{
		public string Id { get; set; }
		public double? Left { get; set; }
		public string Name { get; set; }
		public List<IJsonImagePreview> Previews { get; set; }
		public int? Rotation { get; set; }
		public double? Top { get; set; }
		public string Url { get; set; }
		public int? ZIndex { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Left = obj.TryGetNumber("left");
					Name = obj.TryGetString("image");
					Previews = obj.Deserialize<List<IJsonImagePreview>>(serializer, "imageScaled");
					Rotation = (int?) obj.TryGetNumber("rotate");
					Top = obj.TryGetNumber("top");
					Url = obj.TryGetString("imageUrl");
					ZIndex = (int?) obj.TryGetNumber("zIndex");
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();

			Id.Serialize(json, serializer, "id");
			Left.Serialize(json, serializer, "left");
			Name.Serialize(json, serializer, "image");
			Rotation.Serialize(json, serializer, "rotate");
			Top.Serialize(json, serializer, "top");
			ZIndex.Serialize(json, serializer, "zIndex");

			return json;
		}
	}
}