using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Manatee.Trello.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests
{
	[TestFixture]
	public class SynchronizedContextTests
	{
		[Test]
		public async Task RefreshThrottleHoldsCalls()
		{
			var callCount = 0;

			MockHost.Initialize();

			MockHost.Client.Setup(c => c.Execute<IJsonCard>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
			        .Callback(() => Interlocked.Increment(ref callCount))
			        .ReturnsAsync(MockHost.CardResponse.Object);

			await SafeRunner.Rest(async () =>
				{
					TrelloConfiguration.RefreshThrottle = TimeSpan.FromDays(1);

					var card = new Card(TrelloIds.CardId);

					await card.Refresh();
					await card.Refresh();
				});


			callCount.Should().Be(1);
		}

		[Test]
		public async Task NoRefreshThrottleAllowsCalls()
		{
			var callCount = 0;

			MockHost.Initialize();

			MockHost.Client.Setup(c => c.Execute<IJsonCard>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
			        .Callback(() => Interlocked.Increment(ref callCount))
			        .ReturnsAsync(MockHost.CardResponse.Object);

			await SafeRunner.Rest(async () =>
				{
					TrelloConfiguration.RefreshThrottle = TimeSpan.FromDays(0);

					var card = new Card(TrelloIds.CardId);

					await card.Refresh();
					await card.Refresh();
				});

			callCount.Should().Be(2);
		}
	}
}
