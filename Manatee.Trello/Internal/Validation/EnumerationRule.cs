/***************************************************************************************

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
		public static IValidationRule<T> Instance { get; private set; }

		private readonly Type _enumType;

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
					throw new ArgumentException(string.Format("Type {0} must be an enumeration or a nullable enumeration.", _enumType));
				_enumType = _enumType.GetGenericArguments().First();
			}
			if (!_enumType.IsEnum)
				throw new ArgumentException(string.Format("Type {0} must be an enumeration or a nullable enumeration.", _enumType));
		}

		public string Validate(T oldValue, T newValue)
		{
			var validValues = Enum.GetValues(_enumType).Cast<T>();
			return !validValues.Contains(newValue)
					   ? string.Format("{0} is not defined in type {1}.", newValue, _enumType.Name)
				       : null;
		}
	}
}