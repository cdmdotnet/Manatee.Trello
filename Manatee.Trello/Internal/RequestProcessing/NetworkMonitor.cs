/***************************************************************************************

	Copyright 2013 Greg Dennis

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

using System.Net.NetworkInformation;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal static class NetworkMonitor
	{
		public static bool IsConnected { get; private set; }

#if IOS
		private static System.Action _connectionStatusChangedInvoker;

		public static event System.Action ConnectionStatusChanged
		{
			add { _connectionStatusChangedInvoker += value; }
			remove { _connectionStatusChangedInvoker -= value; }
		}
#else
		public static event System.Action ConnectionStatusChanged;
#endif
		static NetworkMonitor()
		{
			IsConnected = NetworkInterface.GetIsNetworkAvailable();
			NetworkChange.NetworkAvailabilityChanged += HandleNetworkAvailabilityChange;
		}

		private static void HandleNetworkAvailabilityChange(object sender, NetworkAvailabilityEventArgs e)
		{
			if (IsConnected == e.IsAvailable) return;
			IsConnected = e.IsAvailable;
#if IOS
			var handler = _connectionStatusChangedInvoker;
#else
			var handler = ConnectionStatusChanged;
#endif
			handler?.Invoke();
		}
	}
}