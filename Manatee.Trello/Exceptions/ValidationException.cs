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
 
	File Name:		ValidationException.cs
	Namespace:		Manatee.Trello.Exceptions
	Class Name:		ValidationException
	Purpose:		Thrown when validation fails on a Trello object property.

***************************************************************************************/

using System;
using System.Collections.Generic;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Thrown when validation fails on a Trello object property.
	/// </summary>
	public class ValidationException<T> : Exception
	{
		/// <summary>
		/// Gets a collection of errors that occurred while validating the value.
		/// </summary>
		public IEnumerable<string> Errors { get; private set; }

		/// <summary>
		/// Creates a new instance of the <see cref="ValidationException{T}"/> object.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="errors"></param>
		public ValidationException(T value, IEnumerable<string> errors)
			: base($"'{value}' is not a valid value.")
		{
			Errors = errors;
		}
	}
}