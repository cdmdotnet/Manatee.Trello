using System;
using System.Linq;
using Manatee.Trello.Rest;
using RestSharp;
using RestSharp.Serializers;
using IRestRequest = Manatee.Trello.Rest.IRestRequest;
using IRestResponse = Manatee.Trello.Rest.IRestResponse;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpRequest : RestRequest, IRestRequest
	{
		private bool _hasBody;

		RestMethod IRestRequest.Method
		{
			get { return GetMethod(); }
			set { SetMethod(value); }
		}
		public IRestResponse Response { get; set; }

		public RestSharpRequest(ISerializer serializer, string endpoint)
			: base(endpoint)
		{
			RequestFormat = DataFormat.Json;
			DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
			JsonSerializer = serializer;
		}

		void IRestRequest.AddParameter(string name, object value)
		{
			if (_hasBody)
			{
				if (!Resource.Contains("?"))
					Resource += "?";
				else
					Resource += "&";
				Resource += $"{name}={value}";
				return;
			}
			AddParameter(name, value, ParameterType.GetOrPost);
		}
		void IRestRequest.AddBody(object body)
		{
			if (Parameters.Any())
			{
				var parameterList = Parameters.Where(p => p.Type == ParameterType.GetOrPost).ToList();
#if NET35 || NET35C
				var parameterCallout = string.Join("&", parameterList.Select(p => $"{p.Name}={p.Value}").ToArray());
#elif NET4 || NET4C || NET45
				var parameterCallout = string.Join("&", parameterList.Select(p => $"{p.Name}={p.Value}"));
#endif
				Resource += $"?{parameterCallout}";
				foreach (var parameter in parameterList)
				{
					Parameters.Remove(parameter);
				}
			}
			// See http://boredwookie.net/index.php/blog/how-get-restsharp-send-content-type-header-applicationjson/.
			// Due to the way that R# tries to determine the Content-Type header automatically, we can't use AddBody() here.
			// Instead we serialze the content manually and add the parameter explicitly as the body.
			var content = JsonSerializer.Serialize(body);
			AddParameter("application/json", content, ParameterType.RequestBody);
			_hasBody = true;
		}

		private RestMethod GetMethod()
		{
			switch (Method)
			{
				case Method.GET:
					return RestMethod.Get;
				case Method.POST:
					return RestMethod.Post;
				case Method.PUT:
					return RestMethod.Put;
				case Method.DELETE:
					return RestMethod.Delete;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		private void SetMethod(RestMethod value)
		{
			switch (value)
			{
				case RestMethod.Get:
					Method = Method.GET;
					break;
				case RestMethod.Put:
					Method = Method.PUT;
					break;
				case RestMethod.Post:
					Method = Method.POST;
					break;
				case RestMethod.Delete:
					Method = Method.DELETE;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(value));
			}
		}
	}
}