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
 
	File Name:		NullableHasValueRule.cs
	Namespace:		Manatee.Trello.Internal.Validation
	Class Name:		NullableHasValueRule
	Purpose:		Validates that a nullable has a value.

***************************************************************************************/

namespace Manatee.Trello.Internal.Validation
{
	internal class NullableHasValueRule<T> : IValidationRule<T?>
		where T : struct
	{
		public static NullableHasValueRule<T> Instance { get; private set; }

		static NullableHasValueRule()
		{
			Instance = new NullableHasValueRule<T>();
		}
		private NullableHasValueRule() { }

		public string Validate(T? oldValue, T? newValue)
		{
			return !newValue.HasValue
				       ? "Value cannot be null"
				       : null;
		}
	}
}