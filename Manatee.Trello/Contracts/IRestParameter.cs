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
 
	File Name:		IRestCollectionRequest.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		IRestCollectionRequest
	Purpose:		Defines properties required to add parameters to RESTful calls.

***************************************************************************************/
namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines properties required to add parameters to RESTful calls.
	/// </summary>
	public interface IRestParameter
	{
		/// <summary>
		/// The name of the parameter.
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// The value of the parameter.
		/// </summary>
		object Value { get; set; }
	}
}