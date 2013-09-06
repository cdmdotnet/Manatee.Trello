/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		IEntityFactory.cs
	Namespace:		Manatee.Trello.Internal.Genesis
	Class Name:		IEntityFactory
	Purpose:		Defines methods required to create entities from a given
					JSON entity type.

***************************************************************************************/
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.Genesis
{
	/// <summary>
	/// Defines methods required to create entities from a given JSON entity type.
	/// </summary>
	/// <remarks>
	/// This interface is only exposed for unit testing purposes.
	/// </remarks>
	public interface IEntityFactory
	{
		/// <summary />
		T CreateEntity<T>() where T : ExpiringObject;
	}
}