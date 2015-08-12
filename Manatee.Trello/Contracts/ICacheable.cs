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
 
	File Name:		ICacheable.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		ICacheable
	Purpose:		Defines properties which are required to cache an object.

***************************************************************************************/

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines properties which are required to cache an object.
	/// </summary>
	public interface ICacheable
	{
		/// <summary>
		/// Gets an ID on which matching can be performed.
		/// </summary>
		string Id { get; }
	}
}