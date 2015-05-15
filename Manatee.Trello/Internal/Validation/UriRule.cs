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
 
	File Name:		UriRule.cs
	Namespace:		Manatee.Trello.Internal.Validation
	Class Name:		UriRule
	Purpose:		Validates that a string contains an absolute URI.

***************************************************************************************/

using System;

namespace Manatee.Trello.Internal.Validation
{
	internal class UriRule : IValidationRule<string>
	{
		public static UriRule Instance { get; private set; }

		static UriRule()
		{
			Instance = new UriRule();
		}
		private UriRule() { }

		public string Validate(string oldValue, string newValue)
		{
			return !(newValue.BeginsWith("http://") || newValue.BeginsWith("https://")) || !Uri.IsWellFormedUriString(newValue, UriKind.Absolute)
					   ? "Value must begin with \"http://\" or \"https://\" and be a valid URI."
					   : null;
		}
	}
}