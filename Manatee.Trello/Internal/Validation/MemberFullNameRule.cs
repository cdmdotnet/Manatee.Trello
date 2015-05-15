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
 
	File Name:		MemberFullNameRule.cs
	Namespace:		Manatee.Trello.Internal.Validation
	Class Name:		MemberFullNameRule
	Purpose:		Validates that a value is valid for a member's full name.

***************************************************************************************/
using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class MemberFullNameRule : IValidationRule<string>
	{
		private static readonly Regex _regex = new Regex(@"^(\S.{2,}\S)|\S$");

		public static MemberFullNameRule Instance { get; private set; }

		static MemberFullNameRule()
		{
			Instance = new MemberFullNameRule();
		}
		private MemberFullNameRule() { }

		public string Validate(string oldValue, string newValue)
		{
			var isValid = _regex.IsMatch(newValue);
			return !isValid
				       ? "Value must consist of at least four characters and cannot begin or end with whitespace."
				       : null;
		}
	}
}