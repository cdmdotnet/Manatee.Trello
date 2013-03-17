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
 
	File Name:		Serializer.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		Serializer
	Purpose:		Wrapper class for the Manatee.Json.Serializer for use with
					RestSharp.

***************************************************************************************/
using Manatee.Json;
using Manatee.Json.Serialization;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using JsonSerializer = Manatee.Json.Serialization.JsonSerializer;

namespace Manatee.Trello.Rest
{
	internal class Serializer : ISerializer, IDeserializer
	{
		private readonly JsonSerializer _serializer;

		public Serializer()
		{
			_serializer = new JsonSerializer();

			JsonSerializationTypeRegistry.RegisterListType<Board>();
			JsonSerializationTypeRegistry.RegisterListType<Card>();
			JsonSerializationTypeRegistry.RegisterListType<CheckList>();
			JsonSerializationTypeRegistry.RegisterListType<List>();
			JsonSerializationTypeRegistry.RegisterListType<Member>();
			JsonSerializationTypeRegistry.RegisterListType<Notification>();
			JsonSerializationTypeRegistry.RegisterListType<Organization>();
		}

		public string Serialize(object obj)
		{
			var json = _serializer.Serialize(obj);
			return json.ToString();
		}
		public T Deserialize<T>(IRestResponse response)
		{
			var json = JsonValue.Parse(response.Content);
			T obj = _serializer.Deserialize<T>(json);
			return obj;
		}
		public string RootElement { get; set; }
		public string Namespace { get; set; }
		public string DateFormat { get; set; }
		public string ContentType { get; set; }
	}
}
