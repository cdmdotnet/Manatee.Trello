using System;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class LabelNamesTest : EntityTestBase<LabelNames>
	{
		[TestMethod]
		public void Red()
		{
			var story = new Story("Red");

			var feature = story.InOrderTo("control the red label name")
				.AsA("developer")
				.IWant("to get and set the Red");

			feature.WithScenario("Access Red property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(RedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Red property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(RedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Red property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(RedIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Red property to null")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(RedIs, "not description")
				.When(RedIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Red property to empty")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(RedIs, "not description")
				.When(RedIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Red property to same")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(RedIs, "description")
				.When(RedIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Red property without UserToken")
				.Given(ALabelNamesObject)
				.And(TokenNotSupplied)
				.When(RedIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Orange()
		{
			var story = new Story("Orange");

			var feature = story.InOrderTo("control the red label name")
				.AsA("developer")
				.IWant("to get and set the Orange");

			feature.WithScenario("Access Orange property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(OrangeIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Orange property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(OrangeIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Orange property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(OrangeIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Orange property to null")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(OrangeIs, "not description")
				.When(OrangeIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Orange property to empty")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(OrangeIs, "not description")
				.When(OrangeIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Orange property to same")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(OrangeIs, "description")
				.When(OrangeIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Orange property without UserToken")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(TokenNotSupplied)
				.When(OrangeIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Yellow()
		{
			var story = new Story("Yellow");

			var feature = story.InOrderTo("control the red label name")
				.AsA("developer")
				.IWant("to get and set the Yellow");

			feature.WithScenario("Access Yellow property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(YellowIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Yellow property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(YellowIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Yellow property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(YellowIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Yellow property to null")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(YellowIs, "not description")
				.When(YellowIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Yellow property to empty")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(YellowIs, "not description")
				.When(YellowIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Yellow property to same")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(YellowIs, "description")
				.When(YellowIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Yellow property without UserToken")
				.Given(ALabelNamesObject)
				.And(TokenNotSupplied)
				.When(YellowIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Green()
		{
			var story = new Story("Green");

			var feature = story.InOrderTo("control the red label name")
				.AsA("developer")
				.IWant("to get and set the Green");

			feature.WithScenario("Access Green property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(GreenIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Green property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(GreenIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Green property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(GreenIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Green property to null")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(GreenIs, "not description")
				.When(GreenIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Green property to empty")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(GreenIs, "not description")
				.When(GreenIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Green property to same")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(GreenIs, "description")
				.When(GreenIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Green property without UserToken")
				.Given(ALabelNamesObject)
				.And(TokenNotSupplied)
				.When(GreenIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Blue()
		{
			var story = new Story("Blue");

			var feature = story.InOrderTo("control the red label name")
				.AsA("developer")
				.IWant("to get and set the Blue");

			feature.WithScenario("Access Blue property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(BlueIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Blue property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(BlueIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Blue property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(BlueIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Blue property to null")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(BlueIs, "not description")
				.When(BlueIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Blue property to empty")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(BlueIs, "not description")
				.When(BlueIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Blue property to same")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(BlueIs, "description")
				.When(BlueIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Blue property without UserToken")
				.Given(ALabelNamesObject)
				.And(TokenNotSupplied)
				.When(BlueIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Purple()
		{
			var story = new Story("Purple");

			var feature = story.InOrderTo("control the red label name")
				.AsA("developer")
				.IWant("to get and set the Purple");

			feature.WithScenario("Access Purple property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(PurpleIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Purple property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(PurpleIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Purple property")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.When(PurpleIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Purple property to null")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(PurpleIs, "not description")
				.When(PurpleIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Purple property to empty")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(PurpleIs, "not description")
				.When(PurpleIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Purple property to same")
				.Given(ALabelNamesObject)
				.And(EntityIsRefreshed)
				.And(PurpleIs, "description")
				.When(PurpleIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Purple property without UserToken")
				.Given(ALabelNamesObject)
				.And(TokenNotSupplied)
				.When(PurpleIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonLabelNames>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void ALabelNamesObject()
		{
			_systemUnderTest = new EntityUnderTest();
			OwnedBy<Board>();
			SetupMockGet<IJsonLabelNames>();
		}
		private void RedIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Red = value);
		}
		private void OrangeIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Orange = value);
		}
		private void YellowIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Yellow = value);
		}
		private void GreenIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Green = value);
		}
		private void BlueIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Blue = value);
		}
		private void PurpleIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Purple = value);
		}

		#endregion

		#region When

		private void RedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Red);
		}
		private void RedIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Red = value);
		}
		private void OrangeIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Orange);
		}
		private void OrangeIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Orange = value);
		}
		private void YellowIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Yellow);
		}
		private void YellowIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Yellow = value);
		}
		private void GreenIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Green);
		}
		private void GreenIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Green = value);
		}
		private void BlueIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Blue);
		}
		private void BlueIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Blue = value);
		}
		private void PurpleIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Purple);
		}
		private void PurpleIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Purple = value);
		}

		#endregion
	}
}