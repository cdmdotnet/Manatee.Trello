/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		EntityRepository.cs
	Namespace:		Manatee.Trello.Internal.DataAccess
	Class Name:		EntityRepository
	Purpose:		Implements IEntityRepository.

***************************************************************************************/

using System.Collections.Generic;
using System.Threading;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.DataAccess
{
	internal class JsonRepository : IJsonRepository
	{
		private readonly IRestRequestProcessor _requestProcessor;
		private readonly IRestRequestProvider _requestProvider;

		public JsonRepository(IRestRequestProcessor requestProcessor, IRestRequestProvider requestProvider)
		{
			_requestProcessor = requestProcessor;
			_requestProvider = requestProvider;
		}

		public T Execute<T>(Endpoint endpoint, IDictionary<string, object> parameters = null)
			where T : class
		{
			var request = _requestProvider.Create(endpoint.ToString(), parameters);
			request.Method = endpoint.Method;
			_requestProcessor.AddRequest<T>(request);
			SpinWait.SpinUntil(() => request.Response != null);
			var response = (IRestResponse<T>)request.Response;
			return response.Data;
		}
	}
}
