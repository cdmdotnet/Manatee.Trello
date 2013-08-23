/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		NetworkMonitor.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		NetworkMonitor
	Purpose:		Monitors the network to determine whether an active connection
					exists.

***************************************************************************************/

using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace Manatee.Trello.Internal.RequestProcessing
{
	public interface INetworkMonitor
	{
		bool IsConnected { get; }
		event EventHandler ConnectionStatusChanged;
	}

	internal class NetworkMonitor : INetworkMonitor
	{
		private const string _trelloUrl = "trello.com";
		private static readonly Timer _timer;
		private static readonly NetworkMonitor _default;
		private static bool _isConnected;

		public static NetworkMonitor Default { get { return _default; } }
		public bool IsConnected { get { return _isConnected; } }

		public event EventHandler ConnectionStatusChanged;

		static NetworkMonitor()
		{
			_default = new NetworkMonitor();
			_timer = new Timer(ExecutePing, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromSeconds(30));
		}
		private NetworkMonitor()
		{
			ExecutePing(null);
		}
		~NetworkMonitor()
		{
			_timer.Dispose();
		}

		private static void ExecutePing(object obj)
		{
			var ping = new Ping();
			var reply = ping.Send(_trelloUrl);
			var status = reply.Status == IPStatus.Success;
			if (!_isConnected != status) return;
			_isConnected = status;
			if ((_default != null) && (_default.ConnectionStatusChanged != null))
				_default.ConnectionStatusChanged(null, new EventArgs());
		}
	}
}