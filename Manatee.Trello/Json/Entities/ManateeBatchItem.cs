using System.Linq;
using System.Net;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeBatchItem : IJsonBatchItem, IJsonSerializable
	{
		public HttpStatusCode StatusCode { get; set; }
		public string EntityId { get; set; }
		public string Content { get; set; }
		public string Error { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var response = json.Object.Single();
					StatusCode = (HttpStatusCode) int.Parse(response.Key);
					EntityId = response.Value.Object["id"].String;
					// Don't really like putting it back in string form...
					Content = response.Value.ToString();
					break;
				case JsonValueType.String:
					Error = json.String;
					break;
			}
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new System.NotImplementedException();
		}
	}
}