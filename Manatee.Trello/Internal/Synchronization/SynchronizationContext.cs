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

		internal DateTime _lastUpdate;

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
				Merge();
				_lastUpdate = DateTime.Now;
			}
		}
		public void ResetTimer()
		{
			if (_timer == null) return;
			if (_timer.Enabled)
				_timer.Stop();
			_timer.Start();
		}

		protected abstract void Merge();
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

		protected sealed override void Merge()
		{
			var newData = GetData();
			Merge(newData);
		}
		protected sealed override void Submit()
		{
			SubmitData();
			_localChanges.Clear();
		}

		internal void Merge(TJson json)
		{
			if (ReferenceEquals(json, Data)) return;
			foreach (var propertyName in _properties.Keys.Except(_localChanges))
			{
				var property = _properties[propertyName];
				var newValue = property.Get(json);
				property.Set(Data, newValue);
			}
			_lastUpdate = DateTime.Now;
		}
	}
}