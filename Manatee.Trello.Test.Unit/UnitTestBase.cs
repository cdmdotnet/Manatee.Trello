using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Moq;

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
				Validator = new Mock<IValidator>();
				EntityRepository = new Mock<IEntityRepository>();

				Log.Setup(l => l.Error(It.IsAny<Exception>(), It.Is<bool>(b => b)))
						.Callback((Exception e, bool b) => { throw e; });
				EntityRepository.SetupGet(r => r.EntityDuration)
								.Returns(TimeSpan.FromDays(1));
			}
		}
	}
}
