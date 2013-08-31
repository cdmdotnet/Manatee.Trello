/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		MockValidator.cs
	Namespace:		Manatee.Trello.Test.Unit.Mocks
	Class Name:		MockValidator
	Purpose:		Mocks IValidator.

***************************************************************************************/

using System;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Moq;

namespace Manatee.Trello.Test.Unit.Mocks
{
	public class MockValidator : Mock<IValidator>
	{
		public MockValidator()
		{
			SetupValidatorGenericCalls();
			Setup(v => v.NonEmptyString(It.Is<string>(s => string.IsNullOrWhiteSpace(s))))
					 .Throws<ArgumentNullException>();
			Setup(v => v.Position(It.Is<Position>(p => p == null)))
					 .Throws<ArgumentNullException>();
			Setup(v => v.Position(It.Is<Position>(p => (p != null) && !p.IsValid)))
					 .Throws<ArgumentException>();
			Setup(v => v.MinStringLength(It.Is<string>(s => s == null), It.IsAny<int>(), It.IsAny<string>()))
					 .Throws<ArgumentNullException>();
			Setup(v => v.MinStringLength(It.Is<string>(s => s != null), It.IsAny<int>(), It.IsAny<string>()))
					 .Callback((string s, int i, string p) => { if (s.Trim().Length < i) throw new ArgumentException(); });
			Setup(v => v.StringLengthRange(It.Is<string>(s => s == null), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
					 .Throws<ArgumentNullException>();
			Setup(v => v.StringLengthRange(It.Is<string>(s => s != null), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
					 .Callback((string s, int l, int h, string p) => { if (s.Trim().Length < l || s.Trim().Length > h) throw new ArgumentException(); });
			Setup(v => v.UserName(It.Is<string>(s => s == null)))
					 .Throws<ArgumentNullException>();
			Setup(v => v.UserName(It.Is<string>(s => (s != null) && s.Length < 3)))
					 .Throws<ArgumentException>();
			Setup(v => v.OrgName(It.Is<string>(s => s == null)))
					 .Throws<ArgumentNullException>();
			Setup(v => v.OrgName(It.Is<string>(s => (s != null) && s.Length < 3)))
					 .Throws<ArgumentException>();
			Setup(v => v.Url(It.Is<string>(s => string.IsNullOrWhiteSpace(s))))
					 .Throws<ArgumentNullException>();
			Setup(v => v.Url(It.Is<string>(s => (s != null) && (s.Length > 8) && (s.Substring(0, 7) != "http://") && (s.Substring(0, 8) != "https://"))))
					 .Throws<ArgumentException>();
			Setup(v => v.ArgumentNotNull(It.Is<object>(o => o == null), It.IsAny<string>()))
					 .Throws<ArgumentNullException>();
		}
		private void SetupValidatorGenericCalls()
		{
			SetupValidatorForEntity<Action>();
			SetupValidatorForEntity<Attachment>();
			SetupValidatorForEntity<Badges>();
			SetupValidatorForEntity<Board>();
			SetupValidatorForEntity<BoardMembership>();
			SetupValidatorForEntity<BoardPersonalPreferences>();
			SetupValidatorForEntity<BoardPreferences>();
			SetupValidatorForEntity<Card>();
			SetupValidatorForEntity<CheckItem>();
			SetupValidatorForEntity<CheckList>();
			SetupValidatorForEntity<Label>();
			SetupValidatorForEntity<LabelNames>();
			SetupValidatorForEntity<List>();
			SetupValidatorForEntity<Member>();
			SetupValidatorForEntity<MemberPreferences>();
			SetupValidatorForEntity<MemberSession>();
			SetupValidatorForEntity<Notification>();
			SetupValidatorForEntity<Organization>();
			SetupValidatorForEntity<OrganizationMembership>();
			SetupValidatorForEntity<OrganizationPreferences>();
			SetupValidatorForEntity<Token>();
			SetupValidatorForNullable<double>();
			SetupValidatorForNullable<int>();
			SetupValidatorForNullable<bool>();
			SetupValidatorForNullable<DateTime>();
			SetupValidatorForNullable<MemberPreferenceSummaryPeriodType>();
			SetupValidatorForEnum<MemberPreferenceSummaryPeriodType>();
		}
		private void SetupValidatorForEntity<T>()
			where T : ExpiringObject
		{
			Setup(v => v.Entity(It.Is<T>(e => e == null), It.Is<bool>(b => !b)))
					 .Throws<ArgumentNullException>();
			Setup(v => v.Entity(It.Is<T>(e => (e != null) && string.IsNullOrWhiteSpace(e.Id)), It.IsAny<bool>()))
					 .Throws(new EntityNotOnTrelloException<T>(null));
		}
		private void SetupValidatorForNullable<T>()
			where T : struct
		{
			Setup(v => v.Nullable(It.Is<T?>(n => !n.HasValue)))
					 .Throws<ArgumentNullException>();
		}
		private void SetupValidatorForEnum<T>()
		{
			Setup(v => v.Enumeration(It.Is<T>(n => !Enum.GetValues(typeof(T)).Cast<T>().Contains(n))))
					 .Throws<ArgumentException>();
		}
		 
	}
}