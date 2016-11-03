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