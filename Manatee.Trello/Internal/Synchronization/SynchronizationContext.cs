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

namespace Manatee.Trello.Internal.Synchronization
{
	internal abstract class SynchronizationContext
	{
		private readonly Timer _timer;
		private readonly object _lock;
		private DateTime _lastUpdate;

		public event Action<IEnumerable<string>>  Synchronized;

		protected SynchronizationContext(bool useTimer)
		{
			if (useTimer)
			{
				// default interval is 100ms
				_timer = new Timer { AutoReset = false };
				_timer.Elapsed += TimerElapsed;
			}

			_lastUpdate = DateTime.MinValue;
			_lock = new object();
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
				if (!force && DateTime.Now < _lastUpdate.Add(TrelloConfiguration.ExpiryTime)) return;
				var properties = Merge().ToList();
				if (!properties.Any()) return;
				var handler = Synchronized;
				if (handler != null)
					handler(properties);
			}
		}
		public void ResetTimer()
		{
			if (_timer == null) return;
			if (_timer.Enabled)
				_timer.Stop();
			_timer.Start();
		}

		protected void MarkAsUpdated()
		{
			_lastUpdate = DateTime.Now;
		}
		protected abstract IEnumerable<string> Merge();
		protected abstract void Submit();

		private void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			_timer.Stop();
			Submit();
			_lastUpdate = DateTime.Now;
		}
	}

	internal abstract class SynchronizationContext<TJson> : SynchronizationContext
	{
		protected static Dictionary<string, Property<TJson>> _properties;

		private readonly List<string> _localChanges;

		internal TJson Data { get; private set; }

		protected SynchronizationContext(bool useTimer = true)
			: base(useTimer)
		{
			Data = TrelloConfiguration.JsonFactory.Create<TJson>();
			_localChanges = new List<string>();
		}

		public sealed override T GetValue<T>(string property)
		{
			var value = (T)_properties[property].Get(Data);
			return value;
		}
		public override void SetValue<T>(string property, T value)
		{
			_properties[property].Set(Data, value);
			_localChanges.Add(property);
			ResetTimer();
		}

		protected virtual TJson GetData()
		{
			return Data;
		}
		protected virtual void SubmitData() {}

		protected sealed override IEnumerable<string> Merge()
		{
			var newData = GetData();
			return Merge(newData);
		}
		protected sealed override void Submit()
		{
			SubmitData();
			_localChanges.Clear();
		}
		protected virtual IEnumerable<string> MergeDependencies(TJson json)
		{
			return Enumerable.Empty<string>();
		}

		internal IEnumerable<string> Merge(TJson json)
		{
			MarkAsUpdated();
			if (Equals(json, default(TJson)) || ReferenceEquals(json, Data))
				return Enumerable.Empty<string>();
			var propertyNames = new List<string>();
			foreach (var propertyName in _properties.Keys.Except(_localChanges))
			{
				var property = _properties[propertyName];
				var newValue = property.Get(json);
				var oldValue = property.Get(Data);
				if (!Equals(newValue, oldValue))
					propertyNames.Add(propertyName);
				property.Set(Data, newValue);
			}
			return propertyNames.Concat(MergeDependencies(json));
		}
	}
}