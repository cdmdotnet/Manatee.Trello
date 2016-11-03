using System.Collections.Generic;
using Manatee.Trello.Rest;
using RestSharp.Serializers;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpRequestProvider : IRestRequestProvider
	{
		private readonly ISerializer _serializer;

		public RestSharpRequestProvider(ISerializer serializer)
		{
			_serializer = serializer;
		}

		public IRestRequest Create(string endpoint, IDictionary<string, object> parameters = null)
		{
			var request = new RestSharpRequest(_serializer, endpoint);
			if (parameters != null)
				foreach (var parameter in parameters)
				{
					if (parameter.Key == RestFile.ParameterKey)
					{
						var rf = (RestFile)parameter.Value;
						request.AddFile(parameter.Key, rf.ContentBytes, rf.FileName);
					}
					else
					{
						request.AddParameter(parameter.Key, parameter.Value);
					}
				}
			return request;
		}
	}
}