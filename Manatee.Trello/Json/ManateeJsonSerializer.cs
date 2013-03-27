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
	Namespace:		Manatee.Trello.Json
	Class Name:		Serializer
	Purpose:		Wrapper class for the Manatee.Json.Serializer for use with
					RestSharp.

***************************************************************************************/
using System.Reflection;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using IRestResponse = RestSharp.IRestResponse;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Wrapper class for the Manatee.Json.Serializer for use with RestSharp.
	/// </summary>
	public class ManateeJsonSerializer : ISerializer, IDeserializer, RestSharp.Serializers.ISerializer, RestSharp.Deserializers.IDeserializer
	{
		private readonly JsonSerializer _serializer;
		private readonly MethodInfo _method;

		/// <summary>
		/// Creates and initializes a new instance of the ManateeJsonSerializer class.
		/// </summary>
		public ManateeJsonSerializer()
		{
			_serializer = new JsonSerializer();

			JsonSerializationTypeRegistry.RegisterListType<Action>();
			JsonSerializationTypeRegistry.RegisterListType<Attachment>();
			JsonSerializationTypeRegistry.RegisterListType<Board>();
			JsonSerializationTypeRegistry.RegisterListType<BoardMembership>();
			JsonSerializationTypeRegistry.RegisterListType<Card>();
			JsonSerializationTypeRegistry.RegisterListType<CheckItem>();
			JsonSerializationTypeRegistry.RegisterListType<CheckItemState>();
			JsonSerializationTypeRegistry.RegisterListType<CheckList>();
			JsonSerializationTypeRegistry.RegisterListType<InvitedBoard>();
			JsonSerializationTypeRegistry.RegisterListType<InvitedOrganization>();
			JsonSerializationTypeRegistry.RegisterListType<Label>();
			JsonSerializationTypeRegistry.RegisterListType<List>();
			JsonSerializationTypeRegistry.RegisterListType<Member>();
			JsonSerializationTypeRegistry.RegisterListType<Notification>();
			JsonSerializationTypeRegistry.RegisterListType<Organization>();
			JsonSerializationTypeRegistry.RegisterListType<PinnedBoard>();
			JsonSerializationTypeRegistry.RegisterListType<PremiumOrganization>();
			JsonSerializationTypeRegistry.RegisterListType<VotingMember>();

			_method = _serializer.GetType().GetMethod("Serialize");
		}

		/// <summary>
		/// Serializes an object to JSON.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>An equivalent JSON string.</returns>
		public string Serialize(object obj)
		{
			var method = _method.MakeGenericMethod(new[] {obj.GetType()});
			var json = method.Invoke(_serializer, new[] {obj});
			var text = json.ToString();
			return text;
		}
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="response">The response object which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		public T Deserialize<T>(Contracts.IRestResponse<T> response)
			where T : new()
		{
			var json = JsonValue.Parse(response.Content);
			T obj = _serializer.Deserialize<T>(json);
			return obj;
		}
		/// <summary>
		/// Implements RestSharp.Deserializers.IDeserialize
		/// </summary>
		public T Deserialize<T>(IRestResponse response)
		{
			var json = JsonValue.Parse(response.Content);
			T obj = _serializer.Deserialize<T>(json);
			return obj;
		}
		/// <summary>
		/// Implements RestSharp.Serializers.ISerializer and RestSharp.Deserializers.IDeserialize
		/// </summary>
		public string RootElement { get; set; }
		/// <summary>
		/// Implements RestSharp.Serializers.ISerializer and RestSharp.Deserializers.IDeserialize
		/// </summary>
		public string Namespace { get; set; }
		/// <summary>
		/// Implements RestSharp.Serializers.ISerializer and RestSharp.Deserializers.IDeserialize
		/// </summary>
		public string DateFormat { get; set; }
		/// <summary>
		/// Implements RestSharp.Serializers.ISerializer
		/// </summary>
		public string ContentType { get; set; }
	}
}
