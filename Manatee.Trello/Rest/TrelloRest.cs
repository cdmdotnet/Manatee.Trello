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
 
	File Name:		TrelloRest.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		TrelloRest
	Purpose:		Provides a Trello-specific wrapper for RestSharp.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Trello.Implementation;
using RestSharp;

namespace Manatee.Trello.Rest
{
	internal class TrelloRest
	{
		private const string ApiBaseUrl = "https://api.trello.com/1";
		private readonly Serializer _serializer;
		private readonly string _authKey;
		private readonly string _authToken;

		public TrelloRest(string authKey, string authToken)
		{
			if (string.IsNullOrEmpty(authKey))
				throw new ArgumentException("Authentication key required. Auth keys can be generated from https://trello.com/1/appKey/generate", "authKey");
			_authKey = authKey;
			_authToken = authToken;
			_serializer = new Serializer();
		}

		public T Get<T>(Request<T> request)
			where T : EntityBase, new()
		{
			return Execute(request, Method.GET);
		}
		public TEntity Get<TOwner, TEntity>(Request<TOwner, TEntity> request)
			where TOwner : EntityBase
			where TEntity : OwnedEntityBase<TOwner>, new()
		{
			return Execute(request, Method.GET);
		}
		public IEnumerable<TEntity> Get<TOwner, TEntity>(CollectionRequest<TOwner, TEntity> request)
			where TOwner : EntityBase
			where TEntity : ExpiringObject, new()
		{
			return Execute(request, Method.GET);
		}
		public T Put<T>(Request<T> request)
			where T : EntityBase, new()
		{
			return Execute(request, Method.PUT);
		}
		public TEntity Put<TOwner, TEntity>(Request<TOwner, TEntity> request)
			where TOwner : EntityBase
			where TEntity : OwnedEntityBase<TOwner>, new()
		{
			return Execute(request, Method.PUT);
		}
		public T Post<T>(Request<T> request)
			where T : EntityBase, new()
		{
			return Execute(request, Method.POST);
		}

		private void PrepRequest<T>(Request<T> request, Method method)
			where T : EntityBase, new()
		{
			if (_authToken != null)
				request.AddParameter("token", _authToken);
			request.Method = method;
			request.JsonSerializer = _serializer;
			if (method.In(Method.PUT, Method.POST))
			{
				request.AddParameters();
				request.AddBody(request.Entity);
			}
		}
		private void PrepRequest<TOwner, TEntity>(Request<TOwner, TEntity> request, Method method)
			where TOwner : EntityBase
			where TEntity : OwnedEntityBase<TOwner>, new()
		{
			if (_authToken != null)
				request.AddParameter("token", _authToken);
			request.Method = method;
			request.JsonSerializer = _serializer;
			if (method.In(Method.PUT, Method.POST))
			{
				request.AddParameters();
				request.AddBody(request.Entity);
			}
		}
		private void PrepRequest<TOwner, TEntity>(CollectionRequest<TOwner, TEntity> request, Method method)
			where TOwner : EntityBase
			where TEntity : ExpiringObject, new()
		{
			if (_authToken != null)
				request.AddParameter("token", _authToken);
			request.Method = method;
			request.JsonSerializer = _serializer;
			if (method.In(Method.PUT, Method.POST))
			{
				request.AddParameters();
				request.AddBody(request.Entity);
			}
		}
		private T Execute<T>(Request<T> request, Method method)
			where T : EntityBase, new()
		{
			var client = GenerateRestClient();
			PrepRequest(request, method);
			var response = client.Execute<T>(request);
			return response.Data;
		}
		private TEntity Execute<TOwner, TEntity>(Request<TOwner, TEntity> request, Method method)
			where TOwner : EntityBase
			where TEntity : OwnedEntityBase<TOwner>, new()
		{
			var client = GenerateRestClient();
			PrepRequest(request, method);
			var response = client.Execute<TEntity>(request);
			return response.Data;
		}
		private IEnumerable<TEntity> Execute<TOwner, TEntity>(CollectionRequest<TOwner, TEntity> request, Method method)
			where TOwner : EntityBase
			where TEntity : ExpiringObject, new()
		{
			var client = GenerateRestClient();
			PrepRequest(request, method);
			var response = client.Execute<List<TEntity>>(request);
			return response.Data;
		}
		private RestClient GenerateRestClient()
		{
			var client = new RestClient(ApiBaseUrl);
			client.AddDefaultParameter("key", _authKey);
			client.AddHandler("application/json", _serializer);
			client.AddHandler("text/json", _serializer);
			return client;
		}
	}
}
