using System.Collections.Generic;

namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines methods to generate IRequest objects used to make RESTful calls.
	/// </summary>
	public interface IRestRequestProvider
	{
		/// <summary>
		/// Creates a general request using a collection of objects and an additional parameter to generate the resource string and an object to supply additional parameters.
		/// </summary>
		/// <param name="endpoint">The method endpoint the request calls.</param>
		/// <param name="parameters">A list of paramaters to include in the request.</param>
		/// <returns>An IRestRequest instance which can be sent to an IRestClient.</returns>
		IRestRequest Create(string endpoint, IDictionary<string, object> parameters = null);
	}
}