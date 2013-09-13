using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	public abstract class TrelloTestBase<T>
	{
		protected class SystemUnderTest<TDepCol>
			where TDepCol : new()
		{
			public TDepCol Dependencies { get; private set; }
			public T Sut { get; set; }

			public SystemUnderTest()
			{
				Dependencies = new TDepCol();
			}
		}

		protected Exception Exception { get; private set; }
		protected object ActualResult { get; private set; }

		protected void Execute<TRequest>(Func<TRequest> func)
		{
			Exception = null;
			ActualResult = null;

			try
			{
				ActualResult = func();
			}
			catch (Exception e)
			{
				Exception = e;
			}
		}
		protected void Execute(System.Action action)
		{
			Exception = null;
			ActualResult = null;

			try
			{
				action();
			}
			catch (Exception e)
			{
				Exception = e;
			}
		}
		protected void ExceptionIsNotThrown()
		{
			Debug.WriteLine(Exception);
			Assert.IsNull(Exception);
		}
		[GenericMethodFormat("{0} is thrown")]
		protected void ExceptionIsThrown<TEx>()
			where TEx : Exception
		{
			Assert.IsNotNull(Exception);
			if (!(Exception is TEx))
				Debug.WriteLine(Exception);
			Assert.IsInstanceOfType(Exception, typeof(TEx));
		}
		protected void ResponseIsNull()
		{
			Debug.WriteLine(ActualResult);
			Assert.IsNull(ActualResult);
		}
		protected void ResponseIsNotNull()
		{
			if (ActualResult != null)
				Debug.WriteLine(ActualResult);
			Assert.IsNotNull(ActualResult);
		}
		[GenericMethodFormat("{0} is returned")]
		protected void ResponseIs<TResult>()
		{
			ResponseIsNotNull();
			Assert.IsInstanceOfType(ActualResult, typeof(TResult));
		}
	}
}