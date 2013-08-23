using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Rest;
using Moq;

namespace Manatee.Trello.Test.UnitTests
{
	public abstract class UnitTestBase<T> : TrelloTestBase<T>
	{
		protected class DependencyCollection
		{
			public class MockRestClient : IRestClient
			{
				public IRestResponse<TRequest> Execute<TRequest>(IRestRequest request)
					where TRequest : class
				{
					var mock = new Mock<IRestResponse<TRequest>>();
					mock.SetupGet(r => r.Data)
						.Returns(new Mock<TRequest>().Object);
					return mock.Object;
				}
			}

			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public Mock<IRestRequestProvider> RequestProvider { get; private set; }
			public MockRestClient RestClient { get; private set; }
			public Mock<ITrelloServiceConfiguration> Config { get; private set; }
			public Mock<ILog> Log { get; private set; }
			public Mock<ICache> Cache { get; private set; }
			public Mock<IJsonRepository> Api { get; private set; }
			public Mock<IValidator> Validator { get; set; }

			public DependencyCollection()
			{
				Config = new Mock<ITrelloServiceConfiguration>();
				Cache = new	Mock<ICache>();
				Log = new Mock<ILog>();
				RestClientProvider = new Mock<IRestClientProvider>();
				RequestProvider = new Mock<IRestRequestProvider>();
				RestClient = new MockRestClient();
				Api = new Mock<IJsonRepository>();
				Validator = new Mock<IValidator>();

				Config.SetupGet(c => c.RestClientProvider)
					  .Returns(RestClientProvider.Object);
				Config.SetupGet(c => c.Log)
					  .Returns(Log.Object);
				Config.SetupGet(c => c.Cache)
				      .Returns(Cache.Object);
				Config.SetupGet(c => c.ItemDuration)
				      .Returns(TimeSpan.FromSeconds(60));
				Config.SetupGet(c => c.AutoRefresh)
				      .Returns(true);
				Log.Setup(l => l.Error(It.IsAny<Exception>(), It.Is<bool>(b => b)))
						.Callback((Exception e, bool b) => { throw e; });
				RestClientProvider.SetupGet(p => p.RequestProvider)
				                  .Returns(RequestProvider.Object);
				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
				                  .Returns(RestClient);
				RequestProvider.Setup(p => p.Create(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
				               .Returns(new Mock<IRestRequest>().Object);
				RequestProvider.Setup(p => p.Create(It.IsAny<IRestRequest>()))
							   .Returns((IRestRequest r) => r);
				SetupValidatorGenericCalls();
				Validator.Setup(v => v.NonEmptyString(It.Is<string>(s => string.IsNullOrWhiteSpace(s))))
						 .Throws<ArgumentNullException>();
				Validator.Setup(v => v.Position(It.Is<Position>(p => p == null)))
						 .Throws<ArgumentNullException>();
				Validator.Setup(v => v.Position(It.Is<Position>(p => (p != null) && !p.IsValid)))
						 .Throws<ArgumentException>();
				Validator.Setup(v => v.MinStringLength(It.Is<string>(s => s == null), It.IsAny<int>(), It.IsAny<string>()))
				         .Throws<ArgumentNullException>();
				Validator.Setup(v => v.MinStringLength(It.Is<string>(s => s != null), It.IsAny<int>(), It.IsAny<string>()))
				         .Callback((string s, int i, string p) => { if (s.Trim().Length < i) throw new ArgumentException(); });
				Validator.Setup(v => v.StringLengthRange(It.Is<string>(s => s == null), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
						 .Throws<ArgumentNullException>();
				Validator.Setup(v => v.StringLengthRange(It.Is<string>(s => s != null), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
				         .Callback((string s, int l, int h, string p) =>{ if (s.Trim().Length < l || s.Trim().Length > h) throw new ArgumentException(); });
				Validator.Setup(v => v.UserName(It.Is<string>(s => s == null)))
						 .Throws<ArgumentNullException>();
				Validator.Setup(v => v.UserName(It.Is<string>(s => (s != null) && s.Length < 3)))
						 .Throws<ArgumentException>();
				Validator.Setup(v => v.OrgName(It.Is<string>(s => s == null)))
						 .Throws<ArgumentNullException>();
				Validator.Setup(v => v.OrgName(It.Is<string>(s => (s != null) && s.Length < 3)))
						 .Throws<ArgumentException>();
				Validator.Setup(v => v.Url(It.Is<string>(s => string.IsNullOrWhiteSpace(s))))
				         .Throws<ArgumentNullException>();
				Validator.Setup(v => v.Url(It.Is<string>(s => (s != null) && (s.Length > 8) && (s.Substring(0, 7) != "http://") && (s.Substring(0, 8) != "https://"))))
				         .Throws<ArgumentException>();
				Validator.Setup(v => v.ArgumentNotNull(It.Is<object>(o => o == null), It.IsAny<string>()))
				         .Throws<ArgumentNullException>();
			}

			private void SetupValidatorGenericCalls()
			{
				SetupValidatorForEntity<Action>();
				SetupValidatorForEntity<Attachment>();
				SetupValidatorForEntity<Badges>();
				SetupValidatorForEntity<Board>();
				SetupValidatorForEntity<BoardMembership>();
				SetupValidatorForEntity<BoardPersonalPreferences>();
				SetupValidatorForEntity<BoardPreferences>();
				SetupValidatorForEntity<Card>();
				SetupValidatorForEntity<CheckItem>();
				SetupValidatorForEntity<CheckList>();
				SetupValidatorForEntity<Label>();
				SetupValidatorForEntity<LabelNames>();
				SetupValidatorForEntity<List>();
				SetupValidatorForEntity<Member>();
				SetupValidatorForEntity<MemberPreferences>();
				SetupValidatorForEntity<MemberSession>();
				SetupValidatorForEntity<Notification>();
				SetupValidatorForEntity<Organization>();
				SetupValidatorForEntity<OrganizationMembership>();
				SetupValidatorForEntity<OrganizationPreferences>();
				SetupValidatorForEntity<Token>();
				SetupValidatorForNullable<double>();
				SetupValidatorForNullable<int>();
				SetupValidatorForNullable<bool>();
				SetupValidatorForNullable<DateTime>();
				SetupValidatorForNullable<MemberPreferenceSummaryPeriodType>();
				SetupValidatorForEnum<MemberPreferenceSummaryPeriodType>();
			}
			private void SetupValidatorForEntity<TE>()
				where TE : ExpiringObject
			{
				Validator.Setup(v => v.Entity(It.Is<TE>(e => e == null), It.Is<bool>(b => !b)))
						 .Throws<ArgumentNullException>();
				Validator.Setup(v => v.Entity(It.Is<TE>(e => (e != null) && string.IsNullOrWhiteSpace(e.Id)), It.IsAny<bool>()))
				         .Throws(new EntityNotOnTrelloException<TE>(null));
			}
			private void SetupValidatorForNullable<TN>()
				where TN : struct
			{
				Validator.Setup(v => v.Nullable(It.Is<TN?>(n => !n.HasValue)))
						 .Throws<ArgumentNullException>();
			}
			private void SetupValidatorForEnum<TE>()
			{
				Validator.Setup(v => v.Enumeration(It.Is<TE>(n => !Enum.GetValues(typeof(TE)).Cast<TE>().Contains(n))))
						 .Throws<ArgumentException>();
			}
		}
	}
}
