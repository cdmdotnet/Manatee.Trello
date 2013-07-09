using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	public abstract class TrelloTestBase<T>
	{
		protected abstract class SystemUnderTest<TDepCol>
			where TDepCol : new()
		{
			public TDepCol Dependencies { get; private set; }
			public T Sut { get; set; }

			public SystemUnderTest()
			{
				Dependencies = new TDepCol();
			}
		}

		protected Exception _exception;
		protected object _actualResult;

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
		protected void ExceptionIsNotThrown()
		{
			Assert.IsNull(_exception);
		}
		[GenericMethodFormat("{0} is thrown")]
		protected void ExceptionIsThrown<TEx>()
			where TEx : Exception
		{
			Assert.IsNotNull(_exception);
			Assert.IsInstanceOfType(_exception, typeof(TEx));
		}
		protected void ResponseIsNull()
		{
			Assert.IsNull(_actualResult);
		}
		protected void ResponseIsNotNull()
		{
			Assert.IsNotNull(_actualResult);
		}
		[GenericMethodFormat("{0} is returned")]
		protected void ResponseIs<TResult>()
		{
			Assert.IsInstanceOfType(_actualResult, typeof(TResult));
		}
	}
}