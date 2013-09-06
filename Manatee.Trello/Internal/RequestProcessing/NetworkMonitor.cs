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
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		NetworkMonitor
	Purpose:		Monitors the network to determine whether an active connection
					exists.

***************************************************************************************/
using System;
using System.Net.NetworkInformation;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class NetworkMonitor : INetworkMonitor
	{
		private bool _isConnected;

		public bool IsConnected { get { return _isConnected; } }

		public event EventHandler ConnectionStatusChanged;

		public NetworkMonitor()
		{
			_isConnected = NetworkInterface.GetIsNetworkAvailable();
			NetworkChange.NetworkAvailabilityChanged += HandleNetworkAvailabilityChange;
		}

		private void HandleNetworkAvailabilityChange(object sender, NetworkAvailabilityEventArgs e)
		{
			if (_isConnected == e.IsAvailable) return;
			_isConnected = e.IsAvailable;
			if (ConnectionStatusChanged != null)
				ConnectionStatusChanged(this, new EventArgs());
		}
	}
}