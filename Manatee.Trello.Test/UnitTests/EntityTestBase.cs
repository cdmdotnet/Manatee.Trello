using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.UnitTests
{
	public abstract class EntityTestBase<T> : UnitTestBase<T>
		where T : ExpiringObject, new()
	{
		#region Dependencies

		protected class DependencyCollection : UnitTestBase<T>.DependencyCollection
		{
			public Mock<ITrelloRest> Api { get; private set; }

			public DependencyCollection()
			{
				Api = new Mock<ITrelloRest>();

				RestClientProvider.SetupGet(p => p.RequestProvider)
					.Returns(RequestProvider);
				Api.SetupGet(a => a.RestClientProvider)
					.Returns(RestClientProvider.Object);
				Api.SetupGet(a => a.RequestProvider)
					.Returns(RequestProvider);
				Api.SetupGet(a => a.AuthKey)
					.Returns(TrelloIds.Key);
				Api.SetupGet(a => a.AuthToken)
					.Returns(TrelloIds.Token);
			}
		}

		protected class EntityUnderTest : SystemUnderTest<DependencyCollection>
		{
			public EntityUnderTest()
			{
				Sut = new T();
			}
		}

		#endregion

		#region Data

		protected EntityUnderTest _systemUnderTest;

		#endregion

		#region Given

		protected void TokenNotSupplied()
		{
			_systemUnderTest.Dependencies.Api.SetupGet(a => a.AuthToken)
				.Returns((string) null);
		}
		protected void SetupMockGet<TRequest>()
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Setup(a => a.Get(It.IsAny<IRestRequest<TRequest>>()))
				.Returns(new TRequest());
		}
		protected void SetupMockGetCollection<TRequest>()
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Setup(a => a.Get(It.IsAny<IRestCollectionRequest<TRequest>>()))
				.Returns(new List<TRequest>());
		}
		protected void SetupMockPut<TRequest>()
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Setup(a => a.Put(It.IsAny<IRestRequest<TRequest>>()))
				.Returns(new TRequest());
		}
		protected void SetupMockPost<TRequest>()
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Setup(a => a.Post(It.IsAny<IRestRequest<TRequest>>()))
				.Returns(new TRequest());
		}
		protected void SetupMockDelete<TRequest>()
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Setup(a => a.Delete(It.IsAny<IRestRequest<TRequest>>()))
				.Returns(new TRequest());
		}
		protected void EntityIsExpired()
		{
			_systemUnderTest.Sut.MarkForUpdate();
		}
		protected void EntityIsNotExpired()
		{
			_systemUnderTest.Sut.ForceNotExpired();
		}
		protected void SetupProperty(System.Action action)
		{
			_systemUnderTest.Sut.Svc = null;
			action();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
		}

		#endregion

		#region Then

		[GenericMethodFormat("API.Get<{0}> is called {1} time(s)")]
		protected void MockApiGetIsCalled<TRequest>(int times)
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Get(It.IsAny<IRestRequest<TRequest>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("API.GetCollection<{0}> is called {1} time(s)")]
		protected void MockApiGetCollectionIsCalled<TRequest>(int times)
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Get(It.IsAny<IRestCollectionRequest<TRequest>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("API.Put<{0}> is called {1} time(s)")]
		protected void MockApiPutIsCalled<TRequest>(int times)
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Put(It.IsAny<IRestRequest<TRequest>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("API.Post<{0}> is called {1} time(s)")]
		protected void MockApiPostIsCalled<TRequest>(int times)
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Post(It.IsAny<IRestRequest<TRequest>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("API.Delete<{0}> is called {1} time(s)")]
		protected void MockApiDeleteIsCalled<TRequest>(int times)
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Delete(It.IsAny<IRestRequest<TRequest>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("{0} is returned")]
		protected void ValueIsReturned<TResult>(TResult expectedValue)
		{
			Assert.IsInstanceOfType(_actualResult, typeof (TResult));
			Assert.AreEqual(expectedValue, (TResult) _actualResult);
		}
		protected void NonNullValueOfTypeIsReturned<TResult>()
		{
			Assert.IsNotNull(_actualResult);
			Assert.IsInstanceOfType(_actualResult, typeof (TResult));
		}
		[GenericMethodFormat("{0} is returned")]
		protected void PropertyIsSet<TProp>(TProp expectedValue, TProp propValue)
		{
			Assert.AreEqual(expectedValue, propValue);
		}

		#endregion
	}
}
