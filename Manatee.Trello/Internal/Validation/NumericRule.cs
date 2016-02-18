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
 
	File Name:		NumericRule.cs
	Namespace:		Manatee.Trello.Internal.Validation
	Class Name:		NumericRule
	Purpose:		Validates that a value is valid for a numeric type.

***************************************************************************************/
using System;

namespace Manatee.Trello.Internal.Validation
{
	internal class NumericRule<T> : IValidationRule<T?>
		where T : struct, IComparable<T>
	{
		public T? Min { get; set; }
		public T? Max { get; set; }

		public string Validate(T? oldValue, T? newValue)
		{
			if (!newValue.HasValue) return null;
			if (Min.HasValue && newValue.Value.CompareTo(Min.Value) < 0)
			{
				if (Max.HasValue && newValue.Value.CompareTo(Max.Value) > 0)
					return $"Value must be between {Min} and {Max}.";
				return $"Value must be greater than {Min}.";
			}
			if (Max.HasValue && newValue.Value.CompareTo(Max.Value) > 0)
				return $"Value must be less than {Max}.";
			return null;
		}
	}
}