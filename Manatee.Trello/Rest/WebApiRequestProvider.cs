using System.Collections.Generic;

namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Implements IRestRequestProvider for WebApi.
	/// </summary>
	internal class WebApiRequestProvider : IRestRequestProvider
	{
		/// <summary>
		/// Creates a general request using a collection of objects and an additional parameter to
		/// generate the resource string and an object to supply additional parameters.
		/// </summary>
		/// <param name="endpoint">The method endpoint the request calls.</param>
		/// <param name="parameters">A list of paramaters to include in the request.</param>
		/// <returns>An IRestRequest instance which can be sent to an IRestClient.</returns>
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
						request.AddFile(parameter.Key, rf.FilePath, rf.FileName);
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