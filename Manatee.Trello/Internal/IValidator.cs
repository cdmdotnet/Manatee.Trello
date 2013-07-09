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
 
	File Name:		IValidator.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		IValidator
	Purpose:		Defines validation methods.

***************************************************************************************/

using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal
{
	public interface IValidator
	{
		void Writable(ITrelloService service);
		void Entity<T>(T entity, bool allowNulls = false)
			where T : ExpiringObject;
		void Nullable<T>(T? value)
			where T : struct;
		void NonEmptyString(string str);
		void Position(Position pos);
		string MinStringLength(string str, int minLength, string parameter);
		string StringLengthRange(string str, int low, int high, string parameter);
		string UserName(ITrelloService svc, string value);
		string OrgName(ITrelloService svc, string value);
		void Enumeration<T>(T value);
		void Url(string url);
		void ArgumentNotNull(object value, string name = "value");
	}
}