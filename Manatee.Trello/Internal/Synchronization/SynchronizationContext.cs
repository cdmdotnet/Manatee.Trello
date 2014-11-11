/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		SynchronizationContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization.Contexts
	Class Name:		SynchronizationContext
	Purpose:		Provides an internal context which can be synchronized.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Manatee.Trello.Internal.RequestProcessing;

namespace Manatee.Trello.Internal.Synchronization
{
	internal abstract class SynchronizationContext
	{
		private readonly Timer _timer;
		private readonly object _lock;
		private DateTime _lastUpdate;
		private bool _cancelUpdate;

		protected abstract bool HasChanges { get; }

		protected virtual bool IsDataComplete { get { return true; } }
		protected bool ManagesSubmissions { get; private set; }

#if IOS
		private Action<IEnumerable<string>> _synchronizedInvoker;

		public event Action<IEnumerable<string>> Synchronized
		{
			add { _synchronizedInvoker += value; }
			remove { _synchronizedInvoker -= value; }
		}
#else
		public event Action<IEnumerable<string>> Synchronized;
#endif

		protected SynchronizationContext(bool useTimer)
		{
			ManagesSubmissions = useTimer;
			if (useTimer && TrelloConfiguration.ChangeSubmissionTime.Milliseconds != 0)
			{
				_timer = new Timer
					{
						AutoReset = false,
						Interval = TrelloConfiguration.ChangeSubmissionTime.Milliseconds
					};
				_timer.Elapsed += TimerElapsed;
			}

			_lastUpdate = DateTime.MinValue;
			_lock = new object();
			RestRequestProcessor.LastCall += () => TimerElapsed(null, null);
		}
		~SynchronizationContext()
		{
			if (_timer != null)
				_timer.Dispose();
		}

		public abstract T GetValue<T>(string property);
		public abstract void SetValue<T>(string property, T value);

		public void Synchronize(bool force = false)
		{
			lock (_lock)
			{
				if (!force && IsDataComplete && DateTime.Now < _lastUpdate.Add(TrelloConfiguration.ExpiryTime)) return;
				var properties = Merge().ToList();
				if (!properties.Any()) return;
#if IOS
				var handler = _synchronizedInvoker;
#else
				var handler = Synchronized;
#endif
				if (handler != null)
					handler(properties);
			}
		}
		public void ResetTimer()
		{
			if (_timer == null)
			{
				SubmitChanges();
				return;
			}
			if (_timer.Enabled)
				_timer.Stop();
			_timer.Start();
		}
		public void Expire()
		{
			_lastUpdate = DateTime.MinValue;
		}

		protected abstract IEnumerable<string> Merge();
		protected abstract void Submit();

		protected void MarkAsUpdated()
		{
			_lastUpdate = DateTime.Now;
		}
		protected void CancelUpdate()
		{
			_cancelUpdate = true;
		}

		private void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			if (_timer != null)
				_timer.Stop();
			if (!_cancelUpdate && HasChanges)
				SubmitChanges();
			//_cancelUpdate = false;
		}
		private void SubmitChanges()
		{
			Submit();
			_lastUpdate = DateTime.Now;
		}
	}

	internal abstract class SynchronizationContext<TJson> : SynchronizationContext
	{
		protected static Dictionary<string, Property<TJson>> _properties;

		private readonly List<string> _localChanges;
		private readonly object _mergeLock;

		protected override bool HasChanges { get { return _localChanges.Any(); } }

		internal TJson Data { get; private set; }

		protected SynchronizationContext(bool useTimer = true)
			: base(useTimer)
		{
			Data = TrelloConfiguration.JsonFactory.Create<TJson>();
			_localChanges = new List<string>();
			_mergeLock = new object();
		}

		public sealed override T GetValue<T>(string property)
		{
			var value = (T)_properties[property].Get(Data);
			return value;
		}
		public override void SetValue<T>(string property, T value)
		{
			if (!CanUpdate()) return;
			_properties[property].Set(Data, value);
			_localChanges.Add(property);
			ResetTimer();
		}

		protected virtual TJson GetData()
		{
			return Data;
		}
		protected virtual void SubmitData(TJson json) {}
		protected virtual void ApplyDependentChanges(TJson json) {}

		protected sealed override IEnumerable<string> Merge()
		{
			var newData = GetData();
			return Merge(newData);
		}
		protected sealed override void Submit()
		{
			var json = GetChanges();
			if (!ManagesSubmissions) return;
			ApplyDependentChanges(json);
			SubmitData(json);
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
		protected void HandleSubmitRequested(string propertyName)
		{
			AddLocalChange(propertyName);
			ResetTimer();
		}

		internal IEnumerable<string> Merge(TJson json)
		{
			lock (_mergeLock)
			{
				MarkAsUpdated();
				if (!CanUpdate()) return Enumerable.Empty<string>();
				if (Equals(json, default(TJson)) || ReferenceEquals(json, Data))
					return Enumerable.Empty<string>();
				var propertyNames = new List<string>();
				foreach (var propertyName in _properties.Keys.Except(_localChanges))
				{
					var property = _properties[propertyName];
					var newValue = property.Get(json);
					property.Set(Data, newValue);
					propertyNames.Add(propertyName);
				}
				return propertyNames.Concat(MergeDependencies(json));
			}
		}
		internal void AddLocalChange(string property)
		{
			if (_localChanges.Contains(property)) return;
			_localChanges.Add(property);
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
				var newValue = property.Get(Data);
				property.Set(json, newValue);
			}
			return json;
		}
	}
}