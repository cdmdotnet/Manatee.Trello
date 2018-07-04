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
		[SetUp]
		public void Setup()
		{
			MockHost.MockRest<SynchronizedData>();
			TrelloAuthorization.Default.AppKey = "test";
		}

		[TearDown]
		public void TearDown()
		{
			MockHost.ResetRest();
		}

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
			var currentThrottle = TrelloConfiguration.RefreshThrottle;
			try
			{
				MockHost.MockJson();
				MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
				        .Returns(new SynchronizedData());

				TrelloConfiguration.RefreshThrottle = TimeSpan.FromDays(1);

				var target = new SynchronizedObject();

				await target.Synchronize(false, CancellationToken.None);
				await target.Synchronize(false, CancellationToken.None);

				target.RetrievalCount.Should().Be(1);
			}
			finally
			{
				TrelloConfiguration.RefreshThrottle = currentThrottle;
			}
		}

		[Test]
		public async Task NoRefreshThrottleAllowsCalls()
		{
			var currentThrottle = TrelloConfiguration.RefreshThrottle;
			try
			{
				MockHost.MockJson();
				MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
				        .Returns(new SynchronizedData());

				TrelloConfiguration.RefreshThrottle = TimeSpan.Zero;

				var target = new SynchronizedObject();

				await target.Synchronize(false, CancellationToken.None);
				await target.Synchronize(false, CancellationToken.None);

				target.RetrievalCount.Should().Be(2);
			}
			finally
			{
				TrelloConfiguration.RefreshThrottle = currentThrottle;
			}
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
			var currentThrottle = TrelloConfiguration.RefreshThrottle;
			try
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

				await target.Synchronize(false, CancellationToken.None);

				counter.Should().Be(1);
			}
			finally
			{
				TrelloConfiguration.RefreshThrottle = currentThrottle;
			}
		}

		[Test]
		public async Task ForcedRefreshOverridesThrottle()
		{
			var currentThrottle = TrelloConfiguration.RefreshThrottle;
			try
			{
				MockHost.MockJson();
				MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
				        .Returns(new SynchronizedData());

				TrelloConfiguration.RefreshThrottle = TimeSpan.FromDays(1);

				var target = new SynchronizedObject();

				await target.Synchronize(true, CancellationToken.None);
				await target.Synchronize(true, CancellationToken.None);

				target.RetrievalCount.Should().Be(2);
			}
			finally
			{
				TrelloConfiguration.RefreshThrottle = currentThrottle;
			}
		}

		[Test]
		public void MergeRaisesSynchronizedEvent()
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

			target.Merge(new SynchronizedData {Test = "two"});

			counter.Should().Be(1);
			target.Data.Test.Should().Be("two");

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

			await target.Synchronize(false, CancellationToken.None);

			target.SubmissionCount.Should().Be(0);
		}

		[Test]
		public async Task NonMergeableUpdateSkipsSubmission()
		{
			MockHost.MockJson();
			MockHost.JsonFactory.Setup(f => f.Create<SynchronizedData>())
			        .Returns(new SynchronizedData {ValidForMerge = false});

			var target = new SynchronizedObject {SupportsUpdates = false};

			await target.Synchronize(false, CancellationToken.None);

			target.SubmissionCount.Should().Be(0);
		}
	}
}