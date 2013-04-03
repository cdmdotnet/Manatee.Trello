using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
				public IRestRequest<T> Create<T>() where T : ExpiringObject, new()
				{
					return new Mock<IRestRequest<T>>().Object;
				}
				public IRestRequest<T> Create<T>(string id) where T : ExpiringObject, new()
				{
					var mock = new Mock<IRestRequest<T>>();
					mock.SetupGet(r => r.Template).Returns(new T { Id = id });
					return mock.Object;
				}
				public IRestRequest<T> Create<T>(ExpiringObject obj) where T : ExpiringObject, new()
				{
					var mock = new Mock<IRestRequest<T>>();
					mock.SetupGet(r => r.Template).Returns(new T { Id = obj.Id });
					return mock.Object;
				}
				public IRestRequest<T> Create<T>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity, string urlExtension) where T : ExpiringObject, new()
				{
					return new Mock<IRestRequest<T>>().Object;
				}
				public IRestCollectionRequest<T> CreateCollectionRequest<T>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity) where T : ExpiringObject, new()
				{
					return new Mock<IRestCollectionRequest<T>>().Object;
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

		internal SystemUnderTest _serviceGroup;
		protected Exception _exception;
		protected object _actualResult;

		#endregion

		#region Given


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

		#endregion

		#region Then

		protected void ExceptionIsNotThrown()
		{
			Assert.IsNull(_exception);
		}
		protected void ExceptionIsThrown<T>()
			where T : Exception
		{
			Assert.IsNotNull(_exception);
			Assert.IsTrue(_exception is T);
		}

		#endregion
	}
}
