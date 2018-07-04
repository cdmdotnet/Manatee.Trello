using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Rest;

namespace Manatee.Trello.UnitTests.Internal.Synchronization
{
	internal class SynchronizedObject : SynchronizationContext<SynchronizedData>
	{
		public int RetrievalCount { get; private set; }
		public int SubmissionCount { get; private set; }

		public SynchronizedData NewData { get; set; }
		public bool SupportsUpdates { get; set; } = true;

		static SynchronizedObject()
		{
			Properties = new Dictionary<string, Property<SynchronizedData>>
				{
					{
						nameof(SynchronizedData.Test),
						new Property<SynchronizedData, string>(
							(d, a) => d.Test,
							(d, o) => d.Test = o)
					},
					{
						nameof(SynchronizedData.Dependency),
						new Property<SynchronizedData, string>(
							(d, a) => d.Dependency,
							(d, o) => d.Dependency = o)
					}
				};
		}

		public SynchronizedObject(TrelloAuthorization auth = null, bool useTimer = true) 
			: base(auth ?? TrelloAuthorization.Default, useTimer)
		{
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return new Endpoint(RestMethod.Get);
		}

		protected override async Task<SynchronizedData> GetData(CancellationToken ct)
		{
			RetrievalCount++;
			return NewData ?? await base.GetData(ct);
		}

		protected override async Task SubmitData(SynchronizedData json, CancellationToken ct)
		{
			SubmissionCount++;
			await base.SubmitData(json, ct);
		}

		public void Cancel()
		{
			CancelUpdate();
		}

		public Task RequestDependencyChange(string propertyName, CancellationToken ct)
		{
			return HandleSubmitRequested(propertyName, ct);
		}

		protected override bool CanUpdate()
		{
			return SupportsUpdates;
		}
	}
}
