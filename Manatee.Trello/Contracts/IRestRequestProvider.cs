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
 
	File Name:		IRestRequestProvider.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		IRestRequestProvider
	Purpose:		Defines methods to generate IRequest&lt;T&gt; objects used
					to make RESTful calls.

***************************************************************************************/
using System.Collections.Generic;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines methods to generate IRequest&lt;T&gt; objects used to make RESTful calls.
	/// </summary>
	public interface IRestRequestProvider
	{
		/// <summary>
		/// Creates a general request using the type to generate the resource string.
		/// </summary>
		/// <typeparam name="T">The type of object requested.</typeparam>
		/// <returns>An IRequest&lt;T&gt; instance which can be sent to an IRestClient.</returns>
		IRestRequest<T> Create<T>()
			where T : ExpiringObject, new();
		/// <summary>
		/// Creates a general request using the type and supplied ID to generate the resource string.
		/// </summary>
		/// <typeparam name="T">The type of object requested.</typeparam>
		/// <param name="id">The ID of the object requested.</param>
		/// <returns>An IRequest&lt;T&gt; instance which can be sent to an IRestClient.</returns>
		IRestRequest<T> Create<T>(string id)
			where T : ExpiringObject, new();
		/// <summary>
		/// Creates a general request using the type and supplied object to generate the resource string
		/// where the object will be supplying additional parameters.
		/// </summary>
		/// <typeparam name="T">The type of object requested.</typeparam>
		/// <param name="obj">An object which contains additional parameters.</param>
		/// <returns>An IRequest&lt;T&gt; instance which can be sent to an IRestClient.</returns>
		IRestRequest<T> Create<T>(ExpiringObject obj)
			where T : ExpiringObject, new();
		/// <summary>
		/// Creates a general request using a collection of objects and an additional parameter to
		/// generate the resource string and an object to supply additional parameters.
		/// </summary>
		/// <typeparam name="T">The type of object requested.</typeparam>
		/// <param name="tokens">A list of objects whose types and IDs will be used to create a resource string</param>
		/// <param name="entity">An object which contains additional parameters.</param>
		/// <param name="urlExtension">An extension to the resource string.</param>
		/// <returns>An IRequest&lt;T&gt; instance which can be sent to an IRestClient.</returns>
		IRestRequest<T> Create<T>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity = null, string urlExtension = null)
			where T : ExpiringObject, new();
		/// <summary>
		/// Creates a general request to receive a collection of objects using a collection of objects
		/// and an additional parameter to generate the resource string and an object to supply
		/// additional parameters.
		/// </summary>
		/// <typeparam name="T">The type of object requested.</typeparam>
		/// <param name="tokens">A list of objects whose types and IDs will be used to create a resource string</param>
		/// <param name="entity">An object which contains additional parameters.</param>
		/// <returns>An IRequest&lt;T&gt; instance which can be sent to an IRestClient.</returns>
		IRestCollectionRequest<T> CreateCollectionRequest<T>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity = null)
			where T : ExpiringObject, new();
	}
}