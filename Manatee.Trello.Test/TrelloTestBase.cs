using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test
{
	public abstract class TrelloTestBase
	{
		protected interface IDependencyCollection
		{
			void ConfigureDefaultBehavior();
		}

		protected abstract class SystemUnderTest<T, TDepCol>
			where TDepCol : IDependencyCollection, new()
		{
			public TDepCol Dependencies { get; }
			public T Sut { get; set; }

			protected SystemUnderTest()
			{
				Dependencies = new TDepCol();
				Dependencies.ConfigureDefaultBehavior();
			}
		}

		protected Exception Exception { get; private set; }
		protected object ActualResult { get; private set; }

		protected Feature CreateFeature()
		{
			var frame = new StackFrame(1);
			var method = frame.GetMethod();
			var type = GetType();

			var story = new Story($"{type.Name}.{method.Name}");
			return story.InOrderTo(string.Empty)
						.AsA(string.Empty)
						.IWant(string.Empty);
		}

		protected Scenario CreateScenario()
		{
			var frame = new StackFrame(1);
			var method = frame.GetMethod();
			var type = GetType();

			var story = new Story($"{type.Name}.{method.Name}");
			return story.InOrderTo(string.Empty)
						.AsA(string.Empty)
						.IWant(string.Empty)
						.WithScenario(string.Empty);
		}

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