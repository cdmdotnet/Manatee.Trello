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
using System;
using System.Reflection;
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
		private readonly MethodInfo _method;

		public Serializer()
		{
			_serializer = new JsonSerializer();

			JsonSerializationTypeRegistry.RegisterListType<Attachment>();
			JsonSerializationTypeRegistry.RegisterListType<Board>();
			JsonSerializationTypeRegistry.RegisterListType<BoardMembership>();
			JsonSerializationTypeRegistry.RegisterListType<Card>();
			JsonSerializationTypeRegistry.RegisterListType<CheckItem>();
			JsonSerializationTypeRegistry.RegisterListType<CheckItemState>();
			JsonSerializationTypeRegistry.RegisterListType<CheckList>();
			JsonSerializationTypeRegistry.RegisterListType<Label>();
			JsonSerializationTypeRegistry.RegisterListType<List>();
			JsonSerializationTypeRegistry.RegisterListType<Member>();
			JsonSerializationTypeRegistry.RegisterListType<Notification>();
			JsonSerializationTypeRegistry.RegisterListType<Organization>();

			_method = _serializer.GetType().GetMethod("Serialize");
		}

		public string Serialize(object obj)
		{
			var method = _method.MakeGenericMethod(new[] {obj.GetType()});
			var json = method.Invoke(_serializer, new[] {obj});
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
