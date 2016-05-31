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
		public static void Execute(TrelloAuthorization auth, Endpoint endpoint, IDictionary<string, object> parameters = null)
		{
			var obj = new object();
			var request = BuildRequest(auth, endpoint, parameters);
			RestRequestProcessor.AddRequest(request, obj);
			lock (obj)
				Monitor.Wait(obj);
			ValidateResponse(request);
		}
		public static T Execute<T>(TrelloAuthorization auth, Endpoint endpoint, IDictionary<string, object> parameters = null)
			where T : class
		{
			var obj = new object();
			var request = BuildRequest(auth, endpoint, parameters);
			AddDefaultParameters<T>(request);
			RestRequestProcessor.AddRequest<T>(request, obj);
			lock (obj)
				Monitor.Wait(obj);
			ValidateResponse(request);
			var response = request.Response as IRestResponse<T>;
			return response?.Data;
		}
		public static T Execute<T>(TrelloAuthorization auth, Endpoint endpoint, T body)
			where T : class
		{
			var obj = new object();
			var request = BuildRequest(auth, endpoint);
			request.AddBody(body);
			AddDefaultParameters<T>(request);
			RestRequestProcessor.AddRequest<T>(request, obj);
			lock (obj)
				Monitor.Wait(obj);
			ValidateResponse(request);
			var response = request.Response as IRestResponse<T>;
			return response?.Data;
		}

		private static IRestRequest BuildRequest(TrelloAuthorization auth, Endpoint endpoint, IDictionary<string, object> parameters = null)
		{
			var request = TrelloConfiguration.RestClientProvider.RequestProvider.Create(endpoint.ToString(), parameters);
			request.Method = endpoint.Method;
			PrepRequest(request, auth);
			return request;
		}
		private static void PrepRequest(IRestRequest request, TrelloAuthorization auth)
		{
			request.AddParameter("key", auth.AppKey);
			if (auth.UserToken != null)
				request.AddParameter("token", auth.UserToken);
		}
		private static void ValidateResponse(IRestRequest request)
		{
			if (request.Response.Exception != null)
				TrelloConfiguration.Log.Error(request.Response.Exception, TrelloConfiguration.ThrowOnTrelloError);
		}
		private static void AddDefaultParameters<T>(IRestRequest request)
		{
			if (request.Method != RestMethod.Get) return;
			var defaultParameters = RestParameterRepository.GetParameters<T>();
			foreach (var parameter in defaultParameters)
			{
				request.AddParameter(parameter.Key, parameter.Value);
			}
		}
	}
}
