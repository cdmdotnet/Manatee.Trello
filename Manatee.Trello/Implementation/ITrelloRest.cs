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
 
	File Name:		ITrelloRest.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		ITrelloRest
	Purpose:		Defines methods required to retrieve entities from Trello.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Implementation
{
	public interface ITrelloRest
	{
		IRestClientProvider RestClientProvider { get; set; }
		IRestRequestProvider RequestProvider { get; }

		T Get<T>(IRestRequest<T> request)
			where T : ExpiringObject, new();
		IEnumerable<T> Get<T>(IRestCollectionRequest<T> request)
			where T : ExpiringObject, new();
		T Put<T>(IRestRequest<T> request)
			where T : ExpiringObject, new();
		T Post<T>(IRestRequest<T> request)
			where T : ExpiringObject, new();
		T Delete<T>(IRestRequest<T> request)
			where T : ExpiringObject, new();
	}
}