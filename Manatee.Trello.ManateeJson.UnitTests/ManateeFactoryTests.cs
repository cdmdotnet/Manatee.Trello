using System.Collections;
using Manatee.Trello.Json;
using NUnit.Framework;

namespace Manatee.Trello.ManateeJson.UnitTests
{
	[TestFixture]
	public class ManateeFactoryTests
	{
		public abstract class Test
		{
			public abstract void Run();
		}

		public class Test<T> : Test
			where T : class
		{
			public override void Run()
			{
				var factory = new ManateeFactory();

				var entity = factory.Create<T>();

				Assert.IsNotNull(entity);
			}
		}

		public static IEnumerable TestCases
		{
			get
			{
				yield return new TestCaseData(new Test<IJsonAction>());
				yield return new TestCaseData(new Test<IJsonActionData>());
				yield return new TestCaseData(new Test<IJsonActionOldData>());
				yield return new TestCaseData(new Test<IJsonAttachment>());
				yield return new TestCaseData(new Test<IJsonBadges>());
				yield return new TestCaseData(new Test<IJsonBoard>());
				yield return new TestCaseData(new Test<IJsonBoardBackground>());
				yield return new TestCaseData(new Test<IJsonBoardMembership>());
				yield return new TestCaseData(new Test<IJsonBoardPersonalPreferences>());
				yield return new TestCaseData(new Test<IJsonBoardPreferences>());
				yield return new TestCaseData(new Test<IJsonBoardVisibilityRestrict>());
				yield return new TestCaseData(new Test<IJsonCard>());
				yield return new TestCaseData(new Test<IJsonCheckItem>());
				yield return new TestCaseData(new Test<IJsonCheckList>());
				yield return new TestCaseData(new Test<IJsonImagePreview>());
				yield return new TestCaseData(new Test<IJsonLabel>());
				yield return new TestCaseData(new Test<IJsonList>());
				yield return new TestCaseData(new Test<IJsonMember>());
				yield return new TestCaseData(new Test<IJsonMemberPreferences>());
				yield return new TestCaseData(new Test<IJsonMemberSearch>());
				yield return new TestCaseData(new Test<IJsonMemberSession>());
				yield return new TestCaseData(new Test<IJsonNotification>());
				yield return new TestCaseData(new Test<IJsonNotificationData>());
				yield return new TestCaseData(new Test<IJsonNotificationOldData>());
				yield return new TestCaseData(new Test<IJsonOrganization>());
				yield return new TestCaseData(new Test<IJsonOrganizationMembership>());
				yield return new TestCaseData(new Test<IJsonOrganizationPreferences>());
				yield return new TestCaseData(new Test<IJsonParameter>());
				yield return new TestCaseData(new Test<IJsonPosition>());
				yield return new TestCaseData(new Test<IJsonPowerUp>());
				yield return new TestCaseData(new Test<IJsonPowerUpData>());
				yield return new TestCaseData(new Test<IJsonSearch>());
				yield return new TestCaseData(new Test<IJsonSticker>());
				yield return new TestCaseData(new Test<IJsonToken>());
				yield return new TestCaseData(new Test<IJsonTokenPermission>());
				yield return new TestCaseData(new Test<IJsonWebhook>());
				yield return new TestCaseData(new Test<IJsonWebhookNotification>());
			}
		}

		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void GenerateEntities(Test test)
		{
			test.Run();
		}
	}
}
