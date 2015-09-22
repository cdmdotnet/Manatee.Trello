using System.Collections.Generic;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiRequestProvider : IRestRequestProvider
	{
		public IRestRequest Create(string endpoint, IDictionary<string, object> parameters = null)
		{
			var request = new WebApiRestRequest {Resource = endpoint};
			if (parameters != null)
			{
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
			}

			return request;
		}
	}
}