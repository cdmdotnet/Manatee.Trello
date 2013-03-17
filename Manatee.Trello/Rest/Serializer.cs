using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
