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
	Namespace:		Manatee.Trello.Internal
	Class Name:		ITrelloRest
	Purpose:		Defines methods required to retrieve entities from Trello.

***************************************************************************************/
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	/// <summary>
	/// Internal use only.  Defines methods required to retrieve entities from Trello.
	/// </summary>
	public interface ITrelloRest
	{
		/// <summary>
		/// 
		/// </summary>
		IRestClientProvider RestClientProvider { get; set; }
		/// <summary>
		/// 
		/// </summary>
		IRestRequestProvider RequestProvider { get; }
		/// <summary>
		/// 
		/// </summary>
		string AppKey { get; }
		/// <summary>
		/// 
		/// </summary>
		string UserToken { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="request"></param>
		/// <returns></returns>
		T Get<T>(IRestRequest request)
			where T : class;
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="request"></param>
		/// <returns></returns>
		T Put<T>(IRestRequest request)
			where T : class;
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="request"></param>
		/// <returns></returns>
		T Post<T>(IRestRequest request)
			where T : class;
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="request"></param>
		/// <returns></returns>
		T Delete<T>(IRestRequest request)
			where T : class;
	}
}