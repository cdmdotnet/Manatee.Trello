/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		IJsonFactory.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonFactory
	Purpose:		Creates instances of JSON interfaces.

***************************************************************************************/

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Creates instances of JSON interfaces.
	/// </summary>
	public interface IJsonFactory
	{
		/// <summary>
		/// Creates an instance of the requested JSON interface.
		/// </summary>
		/// <typeparam name="T">The type to create.</typeparam>
		/// <returns>An instance of the requested type.</returns>
		T Create<T>();
	}
}