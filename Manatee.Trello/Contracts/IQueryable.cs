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
 
	File Name:		IQueryable.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		IQueryable
	Purpose:		Defines properties which are required to perform a search
					within an object.

***************************************************************************************/
namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines properties which are required to perform a search within an object.
	/// </summary>
	public interface IQueryable : ICacheable {}
}