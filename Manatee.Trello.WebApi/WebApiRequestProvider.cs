/***************************************************************************************

	Copyright 2016 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		WebApiRequestProvider.cs
	Namespace:		Manatee.Trello.WebApi
	Class Name:		WebApiRequestProvider
	Purpose:		Implements IRestRequestProvider for WebApi.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	/// <summary>
	/// Implements IRestRequestProvider for WebApi.
	/// </summary>
	public class WebApiRequestProvider : IRestRequestProvider
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