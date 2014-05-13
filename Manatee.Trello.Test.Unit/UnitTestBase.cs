using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Moq;

namespace Manatee.Trello.Test.Unit
{
	public abstract class UnitTestBase : TrelloTestBase
	{
		public class DependencyCollection
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

			public void SetupListGeneration<T>()
				where T : ExpiringObject, IEquatable<T>, IComparable<T>
			{
				EntityRepository.Setup(r => r.GenerateList<T>(It.IsAny<ExpiringObject>(), It.IsAny<EntityRequestType>(), It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
								.Returns((ExpiringObject o, EntityRequestType r, string s, IDictionary<string, object> p) => new ExpiringCollection<T>(o, r));
			}
		}
	}
}
