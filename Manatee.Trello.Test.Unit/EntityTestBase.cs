using System;
using System.Collections.Generic;
using System.Threading;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Manatee.Trello.Test.Unit.Factories;
using Manatee.Trello.Test.Unit.Mocks;
using Moq;

namespace Manatee.Trello.Test.Unit
{
	public abstract class EntityTestBase<T> : TrelloTestBase
	{
		#region Setup

		protected class DependencyCollection : IDependencyCollection
		{
			public Mock<ISerializer> Serializer { get; private set; }
			public Mock<IDeserializer> Deserializer { get; private set; }
			public Mock<IJsonFactory> JsonFactory { get; private set; }
			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public Mock<IRestRequestProvider> RestRequestProvider { get; private set; }
			public MockRestClient RestClient { get; private set; }
			public Mock<ICache> Cache { get; private set; }
			public Mock<ILog> Log { get; private set; }

			public DependencyCollection()
			{
				Serializer = new Mock<ISerializer>();
				Deserializer = new Mock<IDeserializer>();
				JsonFactory = new Mock<IJsonFactory>();
				RestClientProvider = new Mock<IRestClientProvider>();
				RestRequestProvider = new Mock<IRestRequestProvider>();
				RestClient = new MockRestClient();
				Cache = new Mock<ICache>();
				Log = new Mock<ILog>();
			}

			public void ConfigureDefaultBehavior()
			{
				TrelloConfiguration.Serializer = Serializer.Object;
				TrelloConfiguration.Deserializer = Deserializer.Object;
				TrelloConfiguration.JsonFactory = JsonFactory.Object;
				TrelloConfiguration.RestClientProvider = RestClientProvider.Object;
				TrelloConfiguration.Cache = Cache.Object;
				TrelloConfiguration.Log = Log.Object;
				// Real-time processing for tests means we don't have to wait for the processor.
				TrelloConfiguration.ChangeSubmissionTime = TimeSpan.FromMilliseconds(0);

				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
								  .Returns(RestClient);
				RestClientProvider.SetupGet(p => p.RequestProvider)
				                  .Returns(RestRequestProvider.Object);
				RestRequestProvider.Setup(p => p.Create(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
				                   .Returns((string s, IDictionary<string, object> d) =>
					                   {
						                   var mockResponse = new Mock<IRestRequest>();
						                   mockResponse.SetupAllProperties();
						                   return mockResponse.Object;
					                   });
				ConfigureJsonFactory();
			}

			private void ConfigureJsonFactory()
			{
				ConfigureJsonFactory<IJsonAction>();
				ConfigureJsonFactory<IJsonActionData>();
				ConfigureJsonFactory<IJsonActionOldData>();
				ConfigureJsonFactory<IJsonAttachment>();
				ConfigureJsonFactory<IJsonBadges>();
				ConfigureJsonFactory<IJsonBoard>();
				ConfigureJsonFactory<IJsonBoardMembership>();
				ConfigureJsonFactory<IJsonBoardPersonalPreferences>();
				ConfigureJsonFactory<IJsonBoardPreferences>();
				ConfigureJsonFactory<IJsonBoardVisibilityRestrict>();
				ConfigureJsonFactory<IJsonCard>();
				ConfigureJsonFactory<IJsonCheckItem>();
				ConfigureJsonFactory<IJsonCheckList>();
				ConfigureJsonFactory<IJsonComment>();
				ConfigureJsonFactory<IJsonImagePreview>();
				ConfigureJsonFactory<IJsonLabel>();
				ConfigureJsonFactory<IJsonList>();
				ConfigureJsonFactory<IJsonMember>();
				ConfigureJsonFactory<IJsonMemberPreferences>();
				ConfigureJsonFactory<IJsonMemberSearch>();
				ConfigureJsonFactory<IJsonMemberSession>();
				ConfigureJsonFactory<IJsonNotification>();
				ConfigureJsonFactory<IJsonNotificationData>();
				ConfigureJsonFactory<IJsonOrganization>();
				ConfigureJsonFactory<IJsonOrganizationMembership>();
				ConfigureJsonFactory<IJsonOrganizationPreferences>();
				ConfigureJsonFactory<IJsonParameter>();
				ConfigureJsonFactory<IJsonPosition>();
				ConfigureJsonFactory<IJsonSearch>();
				ConfigureJsonFactory<IJsonToken>();
				ConfigureJsonFactory<IJsonTokenPermission>();
				ConfigureJsonFactory<IJsonWebhook>();
				ConfigureJsonFactory<IJsonWebhookNotification>();
			}

			private void ConfigureJsonFactory<TJson>()
				where TJson : class
			{
				JsonFactory.Setup(f => f.Create<TJson>())
				           .Returns(JsonObjectFactory.Get<TJson>().Object);
			}
		}

		protected abstract class SystemUnderTest : SystemUnderTest<T, DependencyCollection>
		{
			protected SystemUnderTest()
			{
				Sut = BuildSut();
			}

			protected abstract T BuildSut();
		}

		protected SystemUnderTest _sut;

		#endregion

		#region Given

		#endregion

		#region When

		protected void NothingHappens() {}

		#endregion

		#region Then

		protected void RestClientExecuteIsInvoked()
		{
			_sut.Dependencies.RestClient.Verify(null);
		}
		[GenericMethodFormat("RestClient.Execute<{0}> is invoked")]
		protected void RestClientExecuteIsInvoked<TRequest>()
		{
			_sut.Dependencies.RestClient.Verify(typeof(TRequest));
		}
		protected void RestClientExecuteIsNotInvoked()
		{
			_sut.Dependencies.RestClient.Verify(null, 0);
		}
		[GenericMethodFormat("RestClient.Execute<{0}> is not invoked")]
		protected void RestClientExecuteIsNotInvoked<TRequest>()
		{
			_sut.Dependencies.RestClient.Verify(typeof(TRequest), 0);
		}
		protected void CacheAddIsInvoked()
		{
			_sut.Dependencies.Cache.Verify(c => c.Add(It.IsAny<object>()));
		}
		[GenericMethodFormat("Cache.Find<{0}> is invoked")]
		protected void CacheFindIsInvoked<TObj>()
		{
			_sut.Dependencies.Cache.Verify(c => c.Find(It.IsAny<Func<TObj, bool>>()));
		}
		protected void CacheRemoveIsInvoked()
		{
			_sut.Dependencies.Cache.Verify(c => c.Remove(It.IsAny<object>()));
		}

		#endregion
	}
}