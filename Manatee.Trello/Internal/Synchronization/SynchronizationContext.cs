using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.RequestProcessing;

namespace Manatee.Trello.Internal.Synchronization
{
	internal abstract class SynchronizationContext
	{
		private readonly Timer _timer;
		private readonly object _lock;
		private DateTime _lastUpdate;
		private bool _cancelUpdate;

		public abstract bool HasChanges { get; }

		protected virtual bool IsDataComplete => true;
		protected bool ManagesSubmissions { get; }

		public event Action<IEnumerable<string>> Synchronized;

		protected SynchronizationContext(bool useTimer)
		{
			ManagesSubmissions = useTimer;
			if (useTimer && TrelloConfiguration.ChangeSubmissionTime.Milliseconds != 0)
			{
				_timer = new Timer(state => TimerElapsed().Wait(), null, TimeSpan.Zero, TrelloConfiguration.ChangeSubmissionTime);
			}

			_lastUpdate = DateTime.MinValue;
			_lock = new object();
			RestRequestProcessor.LastCall += TimerElapsed;
		}
		~SynchronizationContext()
		{
			_timer?.Dispose();
		}

		public abstract T GetValue<T>(string property);
		public abstract Task SetValue<T>(string property, T value, CancellationToken ct);

		public async Task Synchronize(CancellationToken ct, bool force = false)
		{
			var data = await GetBasicData(ct);
			lock (_lock)
			{
				if (!force && IsDataComplete && DateTime.Now < _lastUpdate.Add(TrelloConfiguration.ExpiryTime)) return;
				var properties = (Merge(data)).ToList();
				if (!properties.Any()) return;
				var handler = Synchronized;
				handler?.Invoke(properties);
			}
		}
		public virtual Task Expire(CancellationToken ct)
		{
			_lastUpdate = DateTime.MinValue;
			var task = Synchronize(ct, true);
			if (TrelloConfiguration.AutoUpdate)
			{
				task.Wait(ct);
			}

			return task;
		}

		protected abstract Task<object> GetBasicData(CancellationToken ct);
		protected abstract IEnumerable<string> Merge(object newData);
		protected abstract Task Submit(CancellationToken ct);

		protected void MarkAsUpdated()
		{
			_lastUpdate = DateTime.Now;
		}
		protected void CancelUpdate()
		{
			_cancelUpdate = true;
		}
		protected async Task ResetTimer(CancellationToken ct)
		{
			if (_timer == null)
			{
				await SubmitChanges(ct);
				return;
			}
			_timer.Stop();
			_timer.Start(TrelloConfiguration.ChangeSubmissionTime);
		}

		private async Task TimerElapsed()
		{
			_timer?.Stop();
			if (!_cancelUpdate && HasChanges)
				await SubmitChanges(CancellationToken.None);
		}
		private async Task SubmitChanges(CancellationToken ct)
		{
			await Submit(ct);
			_lastUpdate = DateTime.Now;
		}
	}

	internal abstract class SynchronizationContext<TJson> : SynchronizationContext
		where TJson : class
	{
		protected static Dictionary<string, Property<TJson>> _properties;

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
			var value = (T)_properties[property].Get(Data, Auth);
			return value;
		}
		public override async Task SetValue<T>(string property, T value, CancellationToken ct)
		{
			if (!CanUpdate()) return;
			_properties[property].Set(Data, value);
			_localChanges.Add(property);
			await ResetTimer(ct);
		}

		protected virtual Task<TJson> GetData(CancellationToken ct)
		{
			return Task.FromResult(Data);
		}
		protected virtual Task SubmitData(TJson json, CancellationToken ct)
		{
			return Task.CompletedTask;
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
			AddLocalChange(propertyName);
			await ResetTimer(ct);
		}
		protected void MarkInitialized()
		{
			IsInitialized = true;
		}

		internal IEnumerable<string> Merge(TJson json)
		{
			lock (_mergeLock)
			{
				MarkAsUpdated();
				MarkInitialized();
				if (!CanUpdate()) return Enumerable.Empty<string>();
				if (Equals(json, default(TJson)) || ReferenceEquals(json, Data))
					return Enumerable.Empty<string>();
				var propertyNames = new List<string>();
				foreach (var propertyName in _properties.Keys.Except(_localChanges))
				{
					var property = _properties[propertyName];
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
				var property = _properties[propertyName];
				var newValue = property.Get(Data, Auth);
				property.Set(json, newValue);
			}
			return json;
		}

		private void AddLocalChange(string property)
		{
			if (_localChanges.Contains(property))
				return;
			_localChanges.Add(property);
		}
	}
}