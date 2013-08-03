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
	Purpose:		Defines validation methods that throw exceptions when
					validation fails.

***************************************************************************************/

using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal
{
	/// <summary>
	/// Internal use only.  Defines validation methods that throw exceptions when validation fails.
	/// </summary>
	public interface IValidator
	{
		/// <summary>
		/// 
		/// </summary>
		void Writable();
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <param name="allowNulls"></param>
		void Entity<T>(T entity, bool allowNulls = false)
			where T : ExpiringObject;
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		void Nullable<T>(T? value)
			where T : struct;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		void NonEmptyString(string str);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pos"></param>
		void Position(Position pos);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <param name="minLength"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		string MinStringLength(string str, int minLength, string parameter);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <param name="low"></param>
		/// <param name="high"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		string StringLengthRange(string str, int low, int high, string parameter);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		string UserName(string value);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		string OrgName(string value);
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		void Enumeration<T>(T value);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		void Url(string url);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="name"></param>
		void ArgumentNotNull(object value, string name = "value");
	}
}