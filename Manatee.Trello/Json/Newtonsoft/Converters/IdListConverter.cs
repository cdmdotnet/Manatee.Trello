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
 
	File Name:		IdListConverter.cs
	Namespace:		Manatee.Trello.Json.Newtonsoft.Converters
	Class Name:		IdListConverter
	Purpose:		Converts a list of entities to a list of their IDs.

***************************************************************************************/
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Manatee.Trello.Json.Newtonsoft.Converters
{
	internal class IdListConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var objectArray = JArray.Load(reader);
			var ids = objectArray.Select(j => ((JObject) j).GetValue("id").Value<string>())
								 .ToList();
			return ids;
		}
		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}