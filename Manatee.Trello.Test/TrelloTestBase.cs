using System;
using System.Diagnostics;
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
			Debug.WriteLine(_exception);
			Assert.IsNull(_exception);
		}
		[GenericMethodFormat("{0} is thrown")]
		protected void ExceptionIsThrown<TEx>()
			where TEx : Exception
		{
			Assert.IsNotNull(_exception);
			if (!(_exception is TEx))
				Debug.WriteLine(_exception);
			Assert.IsInstanceOfType(_exception, typeof(TEx));
		}
		protected void ResponseIsNull()
		{
			Debug.WriteLine(_actualResult);
			Assert.IsNull(_actualResult);
		}
		protected void ResponseIsNotNull()
		{
			if (_actualResult != null)
				Debug.WriteLine(_actualResult);
			Assert.IsNotNull(_actualResult);
		}
		[GenericMethodFormat("{0} is returned")]
		protected void ResponseIs<TResult>()
		{
			ResponseIsNotNull();
			Assert.IsInstanceOfType(_actualResult, typeof(TResult));
		}
	}
}