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
 
	File Name:		EntityNotOnTrelloException.cs
	Namespace:		Manatee.Trello.Exceptions
	Class Name:		EntityNotOnTrelloException<T>
	Purpose:		Thrown when attempting to assign an entity without an ID.

***************************************************************************************/
using System;

namespace Manatee.Trello.Exceptions
{
	public class EntityNotOnTrelloException<T> : Exception
	{
		public T Entity { get; private set; }

		public EntityNotOnTrelloException(T entity)
			: base("Attempted to perform an operation with an entity which does not exist on Trello.")
		{
			Entity = entity;
		}
	}
}
