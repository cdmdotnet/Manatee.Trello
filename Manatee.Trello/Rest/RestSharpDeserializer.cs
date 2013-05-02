using RestSharp.Deserializers;

namespace Manatee.Trello.Rest
{
	internal class RestSharpDeserializer : IDeserializer
	{
		private readonly Json.IDeserializer _inner;

		public Json.IDeserializer Inner { get { return _inner; } }
		public string RootElement { get; set; }
		public string Namespace { get; set; }
		public string DateFormat { get; set; }

		public RestSharpDeserializer(Json.IDeserializer inner)
		{
			_inner = inner;
		}

		public T Deserialize<T>(RestSharp.IRestResponse response)
		{
			var r = new RestSharpResponse<T>(response, default(T));
			return _inner.Deserialize(r);
		}
	}
}