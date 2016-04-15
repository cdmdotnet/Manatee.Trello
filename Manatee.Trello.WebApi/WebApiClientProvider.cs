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
 
	File Name:		WebApiClientProvider.cs
	Namespace:		Manatee.Trello.WebApi
	Class Name:		WebApiClientProvider
	Purpose:		Implements IRestClientProvider for WebApi.

***************************************************************************************/

using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiClientProvider : IRestClientProvider
	{
		public IRestRequestProvider RequestProvider { get; }

		public WebApiClientProvider()
		{
			RequestProvider = new WebApiRequestProvider();
		}

		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			return new WebApiClient();
		}
	}
}