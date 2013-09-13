using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class LabelNamesTest : EntityTestBase<LabelNames, IJsonLabelNames>
	{
		[TestMethod]
		public void Red()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Red property")
				.Given(ALabelNamesObject)
				.When(RedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<LabelNames>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Red property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(RedIsAccessed)
				.Then(RepositoryRefreshIsCalled<LabelNames>, EntityRequestType.LabelNames_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Red property")
				.Given(ALabelNamesObject)
				.When(RedIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.LabelNames_Write_Red)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Red property to same")
				.Given(ALabelNamesObject)
				.And(RedIs, "description")
				.When(RedIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Orange()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Orange property")
				.Given(ALabelNamesObject)
				.When(OrangeIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<LabelNames>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Orange property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(OrangeIsAccessed)
				.Then(RepositoryRefreshIsCalled<LabelNames>, EntityRequestType.LabelNames_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Orange property")
				.Given(ALabelNamesObject)
				.When(OrangeIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.LabelNames_Write_Orange)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Orange property to same")
				.Given(ALabelNamesObject)
				.And(OrangeIs, "description")
				.When(OrangeIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Yellow()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Yellow property")
				.Given(ALabelNamesObject)
				.When(YellowIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<LabelNames>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Yellow property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(YellowIsAccessed)
				.Then(RepositoryRefreshIsCalled<LabelNames>, EntityRequestType.LabelNames_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Yellow property")
				.Given(ALabelNamesObject)
				.When(YellowIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.LabelNames_Write_Yellow)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Yello property to same")
				.Given(ALabelNamesObject)
				.And(YellowIs, "description")
				.When(YellowIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Green()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Green property")
				.Given(ALabelNamesObject)
				.When(GreenIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<LabelNames>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Green property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(GreenIsAccessed)
				.Then(RepositoryRefreshIsCalled<LabelNames>, EntityRequestType.LabelNames_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Green property")
				.Given(ALabelNamesObject)
				.When(GreenIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.LabelNames_Write_Green)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Green property to same")
				.Given(ALabelNamesObject)
				.And(GreenIs, "description")
				.When(GreenIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Blue()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Blue property")
				.Given(ALabelNamesObject)
				.When(BlueIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<LabelNames>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Blue property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(BlueIsAccessed)
				.Then(RepositoryRefreshIsCalled<LabelNames>, EntityRequestType.LabelNames_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Blue property")
				.Given(ALabelNamesObject)
				.When(BlueIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.LabelNames_Write_Blue)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Blue property to same")
				.Given(ALabelNamesObject)
				.And(BlueIs, "description")
				.When(BlueIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Purple()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Purple property")
				.Given(ALabelNamesObject)
				.When(PurpleIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<LabelNames>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Purple property when expired")
				.Given(ALabelNamesObject)
				.And(EntityIsExpired)
				.When(PurpleIsAccessed)
				.Then(RepositoryRefreshIsCalled<LabelNames>, EntityRequestType.LabelNames_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Purple property")
				.Given(ALabelNamesObject)
				.When(PurpleIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.LabelNames_Write_Purple)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Purple property to same")
				.Given(ALabelNamesObject)
				.And(PurpleIs, "description")
				.When(PurpleIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ALabelNamesObject()
		{
			_test = new EntityUnderTest();
			OwnedBy<Board>();
		}
		private void RedIs(string value)
		{
			_test.Json.SetupGet(j => j.Red)
				 .Returns(value);
		}
		private void OrangeIs(string value)
		{
			_test.Json.SetupGet(j => j.Orange)
				 .Returns(value);
		}
		private void YellowIs(string value)
		{
			_test.Json.SetupGet(j => j.Yellow)
				 .Returns(value);
		}
		private void GreenIs(string value)
		{
			_test.Json.SetupGet(j => j.Green)
				 .Returns(value);
		}
		private void BlueIs(string value)
		{
			_test.Json.SetupGet(j => j.Blue)
				 .Returns(value);
		}
		private void PurpleIs(string value)
		{
			_test.Json.SetupGet(j => j.Purple)
				 .Returns(value);
		}

		#endregion

		#region When

		private void RedIsAccessed()
		{
			Execute(() => _test.Sut.Red);
		}
		private void RedIsSet(string value)
		{
			Execute(() => _test.Sut.Red = value);
		}
		private void OrangeIsAccessed()
		{
			Execute(() => _test.Sut.Orange);
		}
		private void OrangeIsSet(string value)
		{
			Execute(() => _test.Sut.Orange = value);
		}
		private void YellowIsAccessed()
		{
			Execute(() => _test.Sut.Yellow);
		}
		private void YellowIsSet(string value)
		{
			Execute(() => _test.Sut.Yellow = value);
		}
		private void GreenIsAccessed()
		{
			Execute(() => _test.Sut.Green);
		}
		private void GreenIsSet(string value)
		{
			Execute(() => _test.Sut.Green = value);
		}
		private void BlueIsAccessed()
		{
			Execute(() => _test.Sut.Blue);
		}
		private void BlueIsSet(string value)
		{
			Execute(() => _test.Sut.Blue = value);
		}
		private void PurpleIsAccessed()
		{
			Execute(() => _test.Sut.Purple);
		}
		private void PurpleIsSet(string value)
		{
			Execute(() => _test.Sut.Purple = value);
		}

		#endregion
	}
}