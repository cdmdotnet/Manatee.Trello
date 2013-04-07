using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.UnitTests
{
	public abstract class EntityTestBase<T> where T : ExpiringObject, new()
	{
		#region Dependencies

		internal class DependencyCollection
		{
			public class MockRequestProvider : IRestRequestProvider
			{
				public IRestRequest<TRequest> Create<TRequest>()
					where TRequest : ExpiringObject, new()
				{
					return new Mock<IRestRequest<TRequest>>().Object;
				}
				public IRestRequest<TRequest> Create<TRequest>(string id)
					where TRequest : ExpiringObject, new()
				{
					var mock = new Mock<IRestRequest<TRequest>>();
					mock.SetupGet(r => r.Template).Returns(new TRequest { Id = id });
					return mock.Object;
				}
				public IRestRequest<TRequest> Create<TRequest>(ExpiringObject obj)
					where TRequest : ExpiringObject, new()
				{
					var mock = new Mock<IRestRequest<TRequest>>();
					mock.SetupGet(r => r.Template).Returns(new TRequest { Id = obj.Id });
					return mock.Object;
				}
				public IRestRequest<TRequest> Create<TRequest>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity, string urlExtension)
					where TRequest : ExpiringObject, new()
				{
					return new Mock<IRestRequest<TRequest>>().Object;
				}
				public IRestCollectionRequest<TRequest> CreateCollectionRequest<TRequest>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity)
					where TRequest : ExpiringObject, new()
				{
					return new Mock<IRestCollectionRequest<TRequest>>().Object;
				}
			}

			public Mock<ITrelloRest> Api { get; private set; }
			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public MockRequestProvider RequestProvider { get; private set; }

			public DependencyCollection()
			{
				Api = new Mock<ITrelloRest>();
				RestClientProvider = new Mock<IRestClientProvider>();
				RequestProvider = new MockRequestProvider();

				RestClientProvider.SetupGet(p => p.RequestProvider)
					.Returns(RequestProvider);
				Api.SetupGet(a => a.RestClientProvider)
					.Returns(RestClientProvider.Object);
				Api.SetupGet(a => a.RequestProvider)
					.Returns(RequestProvider);
			}
		}

		internal class SystemUnderTest
		{
			public DependencyCollection Dependencies { get; private set; }
			public T Sut { get; private set; }

			public SystemUnderTest()
			{
				Dependencies = new DependencyCollection();
				Sut = new T();
			}
		}

		#endregion

		#region Data

		internal SystemUnderTest _systemUnderTest;
		protected Exception _exception;
		protected object _actualResult;

		#endregion

		#region Given

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

		#region When

		protected void Execute<TRequest>(Func<TRequest> func)
		{
			_exception = null;
			_actualResult = null;

			try
			{
				_actualResult = func();
			}
			catch (Exception e)
			{
				_exception = e;
			}
		}
		protected void Execute(System.Action action)
		{
			_exception = null;
			_actualResult = null;

			try
			{
				action();
			}
			catch (Exception e)
			{
				_exception = e;
			}
		}

		#endregion

		#region Then

		protected void ExceptionIsNotThrown()
		{
			Assert.IsNull(_exception);
		}
		[GenericMethodFormat("{0} is thrown")]
		protected void ExceptionIsThrown<TRequest>()
			where TRequest : Exception
		{
			Assert.IsNotNull(_exception);
			Assert.IsTrue(_exception is TRequest);
		}
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
		protected void PropertyIsSet<TParam>(TParam expectedValue, TParam propValue)
		{
			Assert.AreEqual(expectedValue, propValue);
		}

		#endregion
	}
}
