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
 
	File Name:		Request.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		Request<T>
	Purpose:		A request object for use with all REST calls.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using RestSharp;
using Method = Manatee.Trello.Contracts.Method;

namespace Manatee.Trello.Rest
{
	internal class RestSharpRequest<T> : RestSharpRequestBase, IRestRequest<T>
		where T : ExpiringObject, new()
	{
		public ExpiringObject ParameterSource { get; private set; }
		public new Method Method
		{
			get { return GetMethod(); }
			set { SetMethod(value); }
		}

		public RestSharpRequest()
			: base(GetPath()) {}
		public RestSharpRequest(ExpiringObject obj)
			: this(obj.Id)
		{
			ParameterSource = obj;
		}
		public RestSharpRequest(string id)
			: base(GetPathWithId())
		{
			AddParameter("id", id, ParameterType.UrlSegment);
		}
		public RestSharpRequest(IEnumerable<ExpiringObject> tokens, ExpiringObject entity = null, string urlExtension = null)
			: base(GetPath(tokens, urlExtension))
		{
			ParameterSource = entity;
		}

		public void AddParameters()
		{
			if (ParameterSource == null) return;
			foreach (var parameter in ParameterSource.Parameters)
			{
				AddParameter(parameter.Name, parameter.Value);
			}
			ParameterSource.Parameters.Clear();
		}
		void IRestRequest<T>.AddParameter(string name, object value)
		{
			AddParameter(name, value);
		}
		void IRestRequest<T>.AddBody(object body)
		{
			AddBody(body);
		}

		private static string GetPath()
		{
			var section = SectionStrings[typeof(T)];
			return string.Format("{0}", section);
		}
		private static string GetPathWithId()
		{
			var section = SectionStrings[typeof (T)];
			return string.Format("{0}/{{id}}", section);
		}
		private static string GetPath(IEnumerable<ExpiringObject> tokens, string urlExtension)
		{
			var segments = new List<string>();
			foreach (var token in tokens)
			{
				segments.Add(SectionStrings[token.GetType()]);
				if (token.Id != null)
					segments.Add(token.Id);
			}
			if (urlExtension != null)
				segments.Add(urlExtension);
			return string.Join("/", segments);
		}
		private Method GetMethod()
		{
			switch (base.Method)
			{
				case RestSharp.Method.GET:
					return Method.Get;
				case RestSharp.Method.POST:
					return Method.Post;
				case RestSharp.Method.PUT:
					return Method.Put;
				case RestSharp.Method.DELETE:
					return Method.Delete;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		private void SetMethod(Method value)
		{
			switch (value)
			{
				case Method.Get:
					base.Method = RestSharp.Method.GET;
					break;
				case Method.Put:
					base.Method = RestSharp.Method.PUT;
					break;
				case Method.Post:
					base.Method = RestSharp.Method.POST;
					break;
				case Method.Delete:
					base.Method = RestSharp.Method.DELETE;
					break;
				default:
					throw new ArgumentOutOfRangeException("value");
			}
		}
	}
}