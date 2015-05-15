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
 
	File Name:		IdRule.cs
	Namespace:		Manatee.Trello.Internal.Validation
	Class Name:		IdRule
	Purpose:		Validates that a value is valid for an ICacheable ID.

***************************************************************************************/
using System.Text.RegularExpressions;

namespace Manatee.Trello.Internal.Validation
{
	internal class IdRule : IValidationRule<string>
	{
		private static readonly Regex _regex = new Regex("^[a-z0-9]{24}$", RegexOptions.IgnoreCase);

		public static IdRule Instance { get; private set; }

		static IdRule()
		{
			Instance = new IdRule();
		}
		private IdRule() {}

		public string Validate(string oldValue, string newValue)
		{
			return _regex.IsMatch(oldValue) ? null : string.Empty;
		}
	}
}
