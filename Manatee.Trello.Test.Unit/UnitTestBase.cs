using System;
using System.Diagnostics;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.Unit
{
	public abstract class UnitTestBase<T> : TrelloTestBase<T>
	{
		protected class DependencyCollection
		{
			public Mock<ILog> Log { get; private set; }
			public Mock<IValidator> Validator { get; private set; }
			public Mock<IEntityRepository> EntityRepository { get; private set; }

			public DependencyCollection()
			{
				Log = new Mock<ILog>();
				Log.Setup(l => l.Error(It.IsAny<Exception>(), It.Is<bool>(b => b)))
						.Callback((Exception e, bool b) => { throw e; });
				Validator = new Mock<IValidator>();
				EntityRepository = new Mock<IEntityRepository>();

				EntityRepository.SetupGet(r => r.EntityDuration)
								.Returns(TimeSpan.FromDays(1));
			}
		}

		protected Feature CreateFeature()
		{
			var frame = new StackFrame(1);
			var method = frame.GetMethod();
			var type = typeof (T);
			var name = method.Name;

			var story = new Story(string.Format("{0}.{1}", type.Name, name));
			return story.InOrderTo(string.Empty)
						.AsA(string.Empty)
						.IWant(string.Empty);
		}
	}
}
