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
 
	File Name:		UsernameRule.cs
	Namespace:		Manatee.Trello.Internal.Validation
	Class Name:		UsernameRule
	Purpose:		Validates that a string can be used as a username.

***************************************************************************************/

using System.Linq;
using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class UsernameRule : IValidationRule<string>
	{
		private static readonly Regex _regex = new Regex("^[a-z0-9_]{3,}$");
		
		public static UsernameRule Instance { get; private set; }

		static UsernameRule()
		{
			Instance = new UsernameRule();
		}
		private UsernameRule() { }

		public string Validate(string oldValue, string newValue)
		{
			var isValid = _regex.IsMatch(newValue);
			if (isValid)
			{
				var search = new MemberSearch(newValue);
				isValid &= search.Results == null || search.Results.All(o => o.Member.UserName != newValue);
			}
			return !isValid
					   ? "Value must consist of at least three lowercase letters, number, or underscores and must be unique on Trello."
					   : null;
		}
	}
}