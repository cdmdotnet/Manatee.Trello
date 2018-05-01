using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests.Internal.Synchronization
{
	[TestFixture]
	public class SynchronizedContextTests
	{
		[Test]
		public void GetValueDoesNotRetrieve()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			var target = new SynchronizedObject();

			var value = target.GetValue<string>(nameof(SynchronizedData.Test));

			value.Should().Be("default");
			target.RetrievalCount.Should().Be(0);
		}

		[Test]
		public async Task RefreshThrottleHoldsCalls()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			TrelloConfiguration.RefreshThrottle = TimeSpan.FromDays(1);

			var target = new SynchronizedObject();

			await target.Synchronize(CancellationToken.None);
			await target.Synchronize(CancellationToken.None);

			target.RetrievalCount.Should().Be(1);
		}

		[Test]
		public async Task NoRefreshThrottleAllowsCalls()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			TrelloConfiguration.RefreshThrottle = TimeSpan.Zero;

			var target = new SynchronizedObject();

			await target.Synchronize(CancellationToken.None);
			await target.Synchronize(CancellationToken.None);

			target.RetrievalCount.Should().Be(2);
		}

		[Test]
		public async Task SettingDataMergesRequests()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			TrelloConfiguration.ChangeSubmissionTime = TimeSpan.FromMilliseconds(100);

			var target = new SynchronizedObject();

			await target.SetValue(nameof(SynchronizedData.Test), "one", CancellationToken.None);
			await target.SetValue(nameof(SynchronizedData.Test), "two", CancellationToken.None);

			await Task.Delay(150);

			target.SubmissionCount.Should().Be(1);
		}

		[Test]
		public async Task SettingDataWithZeroSubmisisonTimeDoesNotMergeRequests()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			TrelloConfiguration.ChangeSubmissionTime = TimeSpan.Zero;

			var target = new SynchronizedObject();

			await target.SetValue(nameof(SynchronizedData.Test), "one", CancellationToken.None);
			await target.SetValue(nameof(SynchronizedData.Test), "two", CancellationToken.None);

			await Task.Delay(100);

			target.SubmissionCount.Should().Be(2);
		}

		[Test]
		public async Task SynchronizeRaisesSynchronizedEvent()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			TrelloConfiguration.RefreshThrottle = TimeSpan.FromDays(1);

			var counter = 0;

			var target = new SynchronizedObject
				{
					NewData = new SynchronizedData {Test = "one"}
				};
			target.Synchronized += properties => counter++;

			await target.Synchronize(CancellationToken.None);

			counter.Should().Be(1);
		}

		[Test]
		public async Task CancelUpdatePreventsSubmittingPendingChanges()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			TrelloConfiguration.ChangeSubmissionTime = TimeSpan.FromMilliseconds(100);

			var target = new SynchronizedObject();

			await target.SetValue(nameof(SynchronizedData.Test), "one", CancellationToken.None);
			await target.SetValue(nameof(SynchronizedData.Test), "two", CancellationToken.None);

			target.Cancel();

			await Task.Delay(150);

			target.SubmissionCount.Should().Be(0);
		}

		[Test]
		public async Task DependantContextRequestsChangeTriggersSubmission()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			TrelloConfiguration.ChangeSubmissionTime = TimeSpan.FromMilliseconds(100);

			var target = new SynchronizedObject();

			await target.SetValue(nameof(SynchronizedData.Test), "one", CancellationToken.None);
			await target.RequestDependencyChange(nameof(SynchronizedData.Dependency), CancellationToken.None);

			await Task.Delay(150);

			target.SubmissionCount.Should().Be(1);
		}

		[Test]
		public async Task UnsupportedUpdateSkipsSubmission()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData());

			var target = new SynchronizedObject {SupportsUpdates = false};

			await target.Synchronize(CancellationToken.None);

			target.SubmissionCount.Should().Be(0);
		}

		[Test]
		public async Task NonMergeableUpdateSkipsSubmission()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData {ValidForMerge = false});

			var target = new SynchronizedObject {SupportsUpdates = false};

			await target.Synchronize(CancellationToken.None);

			target.SubmissionCount.Should().Be(0);
		}
	}
}