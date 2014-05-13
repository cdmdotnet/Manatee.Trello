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
	internal static class JsonRepository
	{
		public static T Execute<T>(TrelloAuthorization auth, Endpoint endpoint, IDictionary<string, object> parameters)
			where T : class
		{
			var request = TrelloServiceConfiguration.RestClientProvider.RequestProvider.Create(endpoint.ToString(), parameters);
			request.Method = endpoint.Method;
			PrepRequest(request, auth);
			RestRequestProcessor.AddRequest<T>(request);
			SpinWait.SpinUntil(() => request.Response != null);
			if (request.Response.Exception != null && TrelloServiceConfiguration.ThrowOnTrelloError)
				throw request.Response.Exception;
			var response = (IRestResponse<T>)request.Response;
			return response.Data;
		}
		private static void PrepRequest(IRestRequest request, TrelloAuthorization auth)
		{
			request.AddParameter("key", auth.AppKey);
			if (auth.UserToken != null)
				request.AddParameter("token", auth.UserToken);
		}
	}
}
