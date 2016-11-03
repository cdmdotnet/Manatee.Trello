using RestSharp;
using RestSharp.Deserializers;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpDeserializer : IDeserializer
	{
		private readonly Json.IDeserializer _inner;

		public string RootElement { get; set; }
		public string Namespace { get; set; }
		public string DateFormat { get; set; }

		public RestSharpDeserializer(Json.IDeserializer inner)
		{
			_inner = inner;
		}

		public T Deserialize<T>(IRestResponse response)
		{
			var r = new RestSharpResponse<T>(response, default(T));
			return _inner.Deserialize(r);
		}
	}
}