﻿/***************************************************************************************

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
 
	File Name:		IRestRequest.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		IRestRequest<T>
	Purpose:		Defines properties and methods required to make RESTful requests.

***************************************************************************************/
using Manatee.Trello.Implementation;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines properties and methods required to make RESTful requests.
	/// </summary>
	/// <typeparam name="T">The type of object expected in the response.</typeparam>
	public interface IRestRequest<T>
		where T : new()
	{
		/// <summary>
		/// Gets the source object used to retrieve additional parameters.
		/// </summary>
		ExpiringObject ParameterSource { get; }
		/// <summary>
		/// Gets and sets the method to be used in the call.
		/// </summary>
		Method Method { get; set; }
		/// <summary>
		/// Adds the parameters from the ParameterSource object.
		/// </summary>
		void AddParameters();
		/// <summary>
		/// Explicitly adds a parameter to the request.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		void AddParameter(string name, object value);
		/// <summary>
		/// Adds a body to the request.
		/// </summary>
		/// <param name="body">The body.</param>
		void AddBody(object body);
	}
}