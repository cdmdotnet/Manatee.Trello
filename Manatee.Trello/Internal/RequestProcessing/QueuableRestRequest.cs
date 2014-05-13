/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		QueuableRestRequest.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		QueuableRestRequest
	Purpose:		Implements IQueuableRestReqeust in order to process REST
					requests.

***************************************************************************************/

using System;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal abstract class QueuableRestRequest
	{
		public IRestRequest Request { get; private set; }

		public QueuableRestRequest(IRestRequest request)
		{
			Request = request;
		}

		public abstract void Execute(IRestClient client);
		public abstract void CreateNullResponse(Exception e = null);
	}

	internal class QueuableRestRequest<T> : QueuableRestRequest
		where T : class
	{
		public QueuableRestRequest(IRestRequest request)
			: base(request) {}

		public override void Execute(IRestClient client)
		{
			Request.Response = client.Execute<T>(Request);
		}
		public override void CreateNullResponse(Exception e = null)
		{
			Request.Response = new NullRestResponse<T> {Exception = e};
		}
	}
}