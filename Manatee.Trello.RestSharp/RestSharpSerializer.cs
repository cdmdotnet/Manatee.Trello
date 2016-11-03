using RestSharp.Serializers;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpSerializer : ISerializer
	{
		private readonly Json.ISerializer _inner;

		public string RootElement { get; set; }
		public string Namespace { get; set; }
		public string DateFormat { get; set; }
		public string ContentType { get; set; }

		public RestSharpSerializer(Json.ISerializer inner)
		{
			_inner = inner;
		}

		public string Serialize(object obj)
		{
			return _inner.Serialize(obj);
		}
	}
}
