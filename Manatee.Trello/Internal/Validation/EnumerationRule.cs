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
		public static IValidationRule<T> Instance { get; private set; }

		private readonly Type _enumType;
		private readonly bool _isNullable;

		static EnumerationRule()
		{
			Instance = new EnumerationRule<T>();
		}
		private EnumerationRule()
		{
			_enumType = typeof (T);
			if (_enumType.IsGenericType)
			{
				if (_enumType.GetGenericTypeDefinition() != typeof(Nullable<>))
					throw new ArgumentException($"Type {_enumType} must be an enumeration or a nullable enumeration.");
				_enumType = _enumType.GetGenericArguments().First();
				_isNullable = true;
			}
			if (!_enumType.IsEnum)
				throw new ArgumentException($"Type {_enumType} must be an enumeration or a nullable enumeration.");
		}

		public string Validate(T oldValue, T newValue)
		{
			if (_isNullable && Equals(newValue, default(T))) return null;
			var validValues = Enum.GetValues(_enumType).Cast<T>();
			return !validValues.Contains(newValue)
					   ? $"{newValue} is not defined in type {_enumType.Name}."
				       : null;
		}
	}
}