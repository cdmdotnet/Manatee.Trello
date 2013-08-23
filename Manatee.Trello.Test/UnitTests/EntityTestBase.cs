using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.UnitTests
{
	public abstract class EntityTestBase<T> : UnitTestBase<T>
		where T : ExpiringObject, new()
	{
		#region Dependencies

		protected new class DependencyCollection : UnitTestBase<T>.DependencyCollection
		{
			public Mock<ITrelloService> Svc { get; private set; }

			public DependencyCollection()
			{
				Svc = new Mock<ITrelloService>();

				Svc.SetupGet(s => s.UserToken)
				   .Returns(TrelloIds.UserToken);
				Svc.SetupGet(s => s.Configuration)
				   .Returns(Config.Object);
				Validator.Setup(v => v.Writable())
				         .Callback(() => { if (string.IsNullOrWhiteSpace(Svc.Object.UserToken)) throw new ReadOnlyAccessException(); });
				Cache.Setup(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()))
				     .Returns((Func<T, bool> match, Func<T> fetch) => fetch());
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
			_systemUnderTest.Dependencies.Svc.SetupGet(a => a.UserToken)
				.Returns((string)null);
		}
		protected Mock<TRequest> SetupMockGet<TRequest>()
			where TRequest : class
		{
			var obj = new Mock<TRequest>();
			obj.SetupAllProperties();
			_systemUnderTest.Dependencies.Api.Setup(a => a.Get<TRequest>(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
				.Returns(obj.Object);
			return obj;
		}
		protected Mock<TRequest> SetupMockPut<TRequest>()
			where TRequest : class
		{
			var obj = new Mock<TRequest>();
			obj.SetupAllProperties();
			_systemUnderTest.Dependencies.Api.Setup(a => a.Put<TRequest>(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
				.Returns(obj.Object);
			return obj;
		}
		protected Mock<TRequest> SetupMockPost<TRequest>()
			where TRequest : class
		{
			var obj = new Mock<TRequest>();
			obj.SetupAllProperties();
			_systemUnderTest.Dependencies.Api.Setup(a => a.Post<TRequest>(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
				.Returns(obj.Object);
			return obj;
		}
		protected Mock<TRequest> SetupMockDelete<TRequest>()
			where TRequest : class
		{
			var obj = new Mock<TRequest>();
			obj.SetupAllProperties();
			_systemUnderTest.Dependencies.Api.Setup(a => a.Delete<TRequest>(It.IsAny<string>()))
				.Returns(obj.Object);
			return obj;
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
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
		}
		protected void EntityIsRefreshed()
		{
			EntityIsExpired();
			_systemUnderTest.Sut.VerifyNotExpired();
		}
		protected void SetupMockRetrieve<TRequest>()
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Svc.Setup(s => s.Retrieve<TRequest>(It.IsAny<string>()))
				.Returns(new TRequest());
		}
		protected Mock<TEntity> OwnedBy<TEntity>()
			where TEntity : ExpiringObject, new()
		{
			var temp = _systemUnderTest.Sut.Svc;
			var entity = new Mock<TEntity>();
			_systemUnderTest.Sut.Owner = entity.Object;
			_systemUnderTest.Sut.Svc = temp;
			return entity;
		}

		#endregion

		#region Then

		[GenericMethodFormat("SVC.Retrieve<{0}> is called {1} time(s)")]
		protected void MockSvcRetrieveIsCalled<TRequest>(int times)
			where TRequest : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.Svc.Verify(a => a.Retrieve<TRequest>(It.IsAny<string>()), Times.Exactly(times));
		}
		[GenericMethodFormat("API.Get<{0}> is called {1} time(s)")]
		protected void MockApiGetIsCalled<TRequest>(int times)
			where TRequest : class
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Get<TRequest>(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("API.Put<{0}> is called {1} time(s)")]
		protected void MockApiPutIsCalled<TRequest>(int times)
			where TRequest : class
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Put<TRequest>(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("API.Post<{0}> is called {1} time(s)")]
		protected void MockApiPostIsCalled<TRequest>(int times)
			where TRequest : class
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Post<TRequest>(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("API.Delete<{0}> is called {1} time(s)")]
		protected void MockApiDeleteIsCalled<TRequest>(int times)
			where TRequest : class
		{
			_systemUnderTest.Dependencies.Api.Verify(a => a.Delete<TRequest>(It.IsAny<string>()), Times.Exactly(times));
		}
		[GenericMethodFormat("'{0}' is returned")]
		protected void ValueIsReturned<TResult>(TResult expectedValue)
		{
			Assert.IsInstanceOfType(_actualResult, typeof (TResult));
			Assert.AreEqual(expectedValue, (TResult) _actualResult);
		}
		[GenericMethodFormat("Non-null value of type '{0}' is returned")]
		protected void NonNullValueOfTypeIsReturned<TResult>()
		{
			Assert.IsNotNull(_actualResult);
			Assert.IsInstanceOfType(_actualResult, typeof (TResult));
		}
		protected void PropertyIsSet<TProp>(TProp expectedValue, TProp propValue)
		{
			Assert.AreEqual(expectedValue, propValue);
		}

		#endregion
	}
}
