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
 
	File Name:		CardTest.cs
	Namespace:		Manatee.Trello.Test.Unit
	Class Name:		CardTest
	Purpose:		Tests for the Card object.

***************************************************************************************/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit
{
	public class CardTest : EntityTestBase<Card>
	{
		#region Setup

		private class DependencyCollection : EntityTestBase<Card>.DependencyCollection
		{
			public DependencyCollection()
			{
				
			}

			public void ConfigureDefaultBehavior() {}
		}

		private class SystemUnderTest : SystemUnderTest<Card, DependencyCollection>
		{
			public SystemUnderTest()
			{
				Sut = new Card(TrelloIds.Test);
			}
		}

		private SystemUnderTest _sut;

		#endregion

		#region Test methods

		[TestMethod]
		public void TestMethod1()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Given

		private void ACard()
		{
			_sut = new SystemUnderTest();
		}

		#endregion

		#region When


		#endregion

		#region Then


		#endregion

	}
}