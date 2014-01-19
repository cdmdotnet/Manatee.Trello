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

			public void SetupListGeneration<TList>()
				where TList : ExpiringObject, IEquatable<TList>, IComparable<TList>
			{
				EntityRepository.Setup(r => r.GenerateList<TList>(It.IsAny<ExpiringObject>(), It.IsAny<EntityRequestType>(), It.IsAny<string>(), It.IsAny<string>()))
								.Returns((ExpiringObject o, EntityRequestType r, string s1, string s2) => new ExpiringList<TList>(o, r));
			}
		}
	}
}
