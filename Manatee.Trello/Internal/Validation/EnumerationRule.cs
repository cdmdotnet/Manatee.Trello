﻿/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		EnumerationRule.cs
	Namespace:		Manatee.Trello.Internal.Validation
	Class Name:		EnumerationRule<T>
	Purpose:		Validates that a value is valid for an enumeration type.

***************************************************************************************/

using System;
using System.Linq;

namespace Manatee.Trello.Internal.Validation
{
	internal class EnumerationRule<T> : IValidationRule<T>
	{
		public static EnumerationRule<T> Instance { get; private set; }

		static EnumerationRule()
		{
			Instance = new EnumerationRule<T>();
		}
		private EnumerationRule()
		{
			if (!typeof (Enum).IsAssignableFrom(typeof (T)))
				throw new ArgumentException(string.Format("Type {0} must be an enumeration.", typeof (T)));
		}

		public string Validate(T value)
		{
			var validValues = Enum.GetValues(typeof(T)).Cast<T>();
			return !validValues.Contains(value)
				       ? string.Format("{0} is not defined in type {1}", value, typeof (T).Name)
				       : null;
		}
	}
}