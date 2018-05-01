using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

		protected bool ManagesSubmissions { get; }

		public event Action<IEnumerable<string>> Synchronized;

		protected SynchronizationContext(bool useTimer)
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
		}
		~SynchronizationContext()
		{
			_timer?.Dispose();
		}

		public abstract T GetValue<T>(string property);
		public abstract Task SetValue<T>(string property, T value, CancellationToken ct);

		public async Task Synchronize(CancellationToken ct)
		{
			var now = DateTime.Now;
			if (_expires > now) return;

			lock (_expireLock)
			{
				if (_expires > now) return;

				_expires = now + TrelloConfiguration.RefreshThrottle;
			}

			var data = await GetBasicData(ct);
			lock (_updateLock)
			{
				var properties = Merge(data).ToList();
				if (!properties.Any()) return;

				var handler = Synchronized;
				handler?.Invoke(properties);
			}
		}

		protected abstract Task<object> GetBasicData(CancellationToken ct);
		protected abstract IEnumerable<string> Merge(object newData);
		protected abstract Task Submit(CancellationToken ct);

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

		protected TrelloAuthorization Auth { get; }
		protected bool IsInitialized { get; private set; }

		internal TJson Data { get; }

		protected SynchronizationContext(TrelloAuthorization auth, bool useTimer = true)
			: base(useTimer)
		{
			Auth = auth ?? TrelloAuthorization.Default;
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
					_localChanges.Add(property);
					await ResetTimer(t);
				}, ct);
		}

		protected virtual Task<TJson> GetData(CancellationToken ct)
		{
			return Task.FromResult(Data);
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

		protected sealed override async Task<object> GetBasicData(CancellationToken ct)
		{
			return await GetData(ct);
		}
		protected sealed override IEnumerable<string> Merge(object newData)
		{
			return Merge((TJson) newData);
		}
		protected sealed override async Task Submit(CancellationToken ct)
		{
			var json = GetChanges();
			if (!ManagesSubmissions) return;

			ApplyDependentChanges(json);
			await SubmitData(json, ct);
			ClearChanges();
		}
		protected virtual IEnumerable<string> MergeDependencies(TJson json)
		{
			return Enumerable.Empty<string>();
		}
		protected virtual bool CanUpdate()
		{
			return true;
		}
		protected async Task HandleSubmitRequested(string propertyName, CancellationToken ct)
		{
			_AddLocalChange(propertyName);
			await ResetTimer(ct);
		}
		protected void MarkInitialized()
		{
			IsInitialized = true;
		}

		internal IEnumerable<string> Merge(TJson json, bool overwrite = true)
		{
			if (json is IAcceptId mergable && !mergable.ValidForMerge) return Enumerable.Empty<string>();

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
					if (!overwrite && oldValue != null) continue;

					var newValue = property.Get(json, Auth);
					property.Set(Data, newValue);
					propertyNames.Add(propertyName);
				}
				return propertyNames.Concat(MergeDependencies(json));
			}
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

		private void _AddLocalChange(string property)
		{
			if (_localChanges.Contains(property)) return;

			_localChanges.Add(property);
		}
	}
}