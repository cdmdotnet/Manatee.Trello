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
 
	File Name:		ManateeBoardBackground.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeBoardBackground
	Purpose:		Implements IJsonBoardBackground for Manatee.Json.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeBoardBackground : IJsonBoardBackground, IJsonSerializable
	{
		public string Id { get; set; }
		public string Color { get; set; }
		public string Image { get; set; }
		public List<IJsonImagePreview> ImageScaled { get; set; }
		public bool? Tile { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("background");
					Color = obj.TryGetString("backgroundColor");
					Image = obj.TryGetString("backgroundImage");
					ImageScaled = obj.Deserialize<List<IJsonImagePreview>>(serializer, "backgroundImageScaled");
					Tile = obj.TryGetBoolean("backgroundTile");
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
