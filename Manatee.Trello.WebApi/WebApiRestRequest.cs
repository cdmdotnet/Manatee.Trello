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
 
	File Name:		WebApiRestRequest.cs
	Namespace:		Manatee.Trello.WebApi
	Class Name:		WebApiRestRequest
	Purpose:		Implements IRestRequest for WebApi.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	internal class WebApiRestRequest : IRestRequest
	{
		public RestMethod Method { get; set; }
		public string Resource { get; set; }
		public IRestResponse Response { get; set; }

		internal Dictionary<string, object> Parameters { get; }
		internal object Body { get; private set; }
		internal byte[] File { get; private set; }
		internal string FileName { get; private set; }

		public WebApiRestRequest()
		{
			Parameters = new Dictionary<string, object>();
		}

		public void AddParameter(string name, object value)
		{
			Parameters[name] = value;
		}
		public void AddBody(object body)
		{
			Body = body;
		}

		public void AddFile(string key, byte[] contentBytes, string fileName)
		{
			File = contentBytes;
			FileName = fileName;
		}
	}
}