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
 
	File Name:		RestSharpDeserializer.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestSharpDeserializer
	Purpose:		Wraps an IDeserializer implementation in RestSharp's IDeserializer.

***************************************************************************************/

using RestSharp;
using RestSharp.Deserializers;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpDeserializer : IDeserializer
	{
		private readonly Json.IDeserializer _inner;

		public string RootElement { get; set; }
		public string Namespace { get; set; }
		public string DateFormat { get; set; }

		public RestSharpDeserializer(Json.IDeserializer inner)
		{
			_inner = inner;
		}

		public T Deserialize<T>(IRestResponse response)
		{
			var r = new RestSharpResponse<T>(response, default(T));
			return _inner.Deserialize(r);
		}
	}
}