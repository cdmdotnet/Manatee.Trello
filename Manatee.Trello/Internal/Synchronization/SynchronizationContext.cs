using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Eventing;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal abstract class SynchronizationContext
	{
		private readonly Timer _timer;
		private readonly SemaphoreSlim _semaphore;
		private readonly object _updateLock, _expireLock;
		private bool _cancelUpdate;
		private DateTime _expires;

		public abstract bool HasChanges { get; }
		public TrelloAuthorization Auth { get; }

		protected bool ManagesSubmissions { get; }

		public event Action<IEnumerable<string>> Synchronized;

		protected SynchronizationContext(TrelloAuthorization auth, bool useTimer)
		{
			ManagesSubmissions = useTimer;
			if (useTimer && TrelloConfiguration.ChangeSubmissionTime.Milliseconds != 0)
			{
				_timer = new Timer(async state => await _TimerElapsed(), null,
				                   TrelloConfiguration.ChangeSubmissionTime,
				                   TrelloConfiguration.ChangeSubmissionTime);
			}

			_updateLock = new object();
			_expireLock = new object();
			_semaphore = new SemaphoreSlim(1, 1);
			_expires = DateTime.MinValue;
			RestRequestProcessor.LastCall += _TimerElapsed;
			Auth = auth ?? TrelloAuthorization.Default;
		}
		~SynchronizationContext()
		{
			_timer?.Dispose();
		}

		public abstract T GetValue<T>(string property);
		public abstract Task SetValue<T>(string property, T value, CancellationToken ct);

		public async Task Synchronize(bool force, CancellationToken ct)
		{
			if (Auth == TrelloAuthorization.Null) return;

			var now = DateTime.Now;
			if (!force && _expires > now) return;

			lock (_expireLock)
			{
				if (!force && _expires > now) return;

				_expires = now + TrelloConfiguration.RefreshThrottle;
			}

			var data = await GetBasicData(ct);
			lock (_updateLock)
			{
				Merge(data);
			}
		}

		protected abstract Task<object> GetBasicData(CancellationToken ct);
		protected abstract void Merge(object newData);
		protected abstract Task Submit(CancellationToken ct);


		protected void OnMerged(IEnumerable<string> properties)
		{
			var handler = Synchronized;
			handler?.Invoke(properties);
		}
		protected void CancelUpdate()
		{
			_cancelUpdate = true;
		}
		protected async Task ResetTimer(CancellationToken ct)
		{
			if (_timer == null)
			{
				await _SubmitChanges(ct);
				return;
			}

			_timer.Stop();
			_timer.Start(TrelloConfiguration.ChangeSubmissionTime);
		}

		protected async Task DoLocked(Func<CancellationToken, Task> action, CancellationToken ct)
		{
			await _semaphore.WaitAsync(ct);
			try
			{
				await action(ct);
			}
			finally
			{
				_semaphore.Release();
			}
		}

		private async Task _TimerElapsed()
		{
			_timer?.Stop();
			if (!_cancelUpdate)
			{
				await DoLocked(async t => await _SubmitChanges(t), CancellationToken.None);
			}
		}
		private async Task _SubmitChanges(CancellationToken ct)
		{
			if (Auth == TrelloAuthorization.Null) return;
			if (!HasChanges) return;

			await Submit(ct);
		}
	}

	internal abstract class SynchronizationContext<TJson> : SynchronizationContext
		where TJson : class
	{
		protected static Dictionary<string, Property<TJson>> Properties;

		private readonly List<string> _localChanges;
		private readonly object _mergeLock;

		public override bool HasChanges => _localChanges?.Any() ?? false;

		protected bool IsInitialized { get; private set; }

		internal TJson Data { get; }

		protected SynchronizationContext(TrelloAuthorization auth, bool useTimer = true)
			: base(auth, useTimer)
		{
			Data = TrelloConfiguration.JsonFactory.Create<TJson>();
			_localChanges = new List<string>();
			_mergeLock = new object();
		}

		public sealed override T GetValue<T>(string property)
		{
			var value = (T)Properties[property].Get(Data, Auth);
			return value;
		}
		public override Task SetValue<T>(string property, T value, CancellationToken ct)
		{
			return DoLocked(async t =>
				{
					if (!CanUpdate()) return;

					Properties[property].Set(Data, value);
					RaiseUpdated(new[] {property});
					_localChanges.Add(property);
					await ResetTimer(t);
				}, ct);
		}

		public virtual Endpoint GetRefreshEndpoint()
		{
			throw new NotImplementedException();
		}

		protected virtual async Task<TJson> GetData(CancellationToken ct)
		{
			var endpoint = GetRefreshEndpoint();
			var parameters = GetParameters();
			var newData = await JsonRepository.Execute<TJson>(Auth, endpoint, ct, parameters);

			MarkInitialized();
			return newData;
		}
		protected virtual Task SubmitData(TJson json, CancellationToken ct)
		{
#if NET45
			return Task.Run(() => { }, ct);
#else
			return Task.CompletedTask;
#endif
		}
		protected virtual void ApplyDependentChanges(TJson json) {}

		protected virtual Dictionary<string, object> GetParameters()
		{
			return new Dictionary<string, object>();
		}

		protected sealed override async Task<object> GetBasicData(CancellationToken ct)
		{
			return await GetData(ct);
		}
		protected sealed override void Merge(object newData)
		{
			Merge((TJson) newData);
		}

		protected sealed override async Task Submit(CancellationToken ct)
		{
			var json = GetChanges();
			if (!ManagesSubmissions) return;

			ApplyDependentChanges(json);
			await SubmitData(json, ct);
			ClearChanges();
		}
		protected virtual IEnumerable<string> MergeDependencies(TJson json, bool overwrite)
		{
			return Enumerable.Empty<string>();
		}
		protected virtual bool CanUpdate()
		{
			return true;
		}
		protected Task HandleSubmitRequested(string propertyName, CancellationToken ct)
		{
			_AddLocalChange(propertyName);
			return ResetTimer(ct);
		}
		protected void MarkInitialized()
		{
			IsInitialized = true;
		}

		internal IEnumerable<string> Merge(TJson json, bool overwrite = true)
		{
			if (json is IAcceptId mergable && !mergable.ValidForMerge) return Enumerable.Empty<string>();

			IEnumerable<string> allProperties;

			lock (_mergeLock)
			{
				MarkInitialized();
				if (!CanUpdate()) return Enumerable.Empty<string>();
				if (Equals(json, default(TJson)) || ReferenceEquals(json, Data))
					return Enumerable.Empty<string>();
				var propertyNames = new List<string>();
				foreach (var propertyName in Properties.Keys.Except(_localChanges))
				{
					var property = Properties[propertyName];
					var oldValue = property.Get(Data, Auth);
					var newValue = property.Get(json, Auth);
					if (newValue == null && !overwrite) continue;
					if (Equals(newValue, oldValue)) continue;

					property.Set(Data, newValue);
					if (!property.IsHidden)
						propertyNames.Add(propertyName);
				}

				allProperties = propertyNames.Concat(MergeDependencies(json, overwrite));
			}

			allProperties = allProperties.ToList();

			// ReSharper disable PossibleMultipleEnumeration
			if (allProperties.Any())
			{
				OnMerged(allProperties);
				RaiseUpdated(allProperties);
			}

			return allProperties;
			// ReSharper restore PossibleMultipleEnumeration
		}
		internal void ClearChanges()
		{
			_localChanges.Clear();
		}
		internal TJson GetChanges()
		{
			var json = TrelloConfiguration.JsonFactory.Create<TJson>();
			foreach (var propertyName in _localChanges)
			{
				var property = Properties[propertyName];
				var newValue = property.Get(Data, Auth);
				property.Set(json, newValue);
			}
			return json;
		}

		internal void RaiseUpdated(IEnumerable<string> properties)
		{
			if (TrelloConfiguration.EnableConsistencyProcessing && Data is IJsonCacheable cacheable)
				EventAggregator.Publish(EntityUpdatedEvent.Create(typeof(TJson), cacheable, properties));
		}

		internal void RaiseDeleted()
		{
			if (TrelloConfiguration.EnableConsistencyProcessing && Data is IJsonCacheable cacheable)
				EventAggregator.Publish(EntityDeletedEvent.Create(typeof(TJson), cacheable));
		}

		private void _AddLocalChange(string property)
		{
			if (_localChanges.Contains(property)) return;

			_localChanges.Add(property);
		}
	}
}