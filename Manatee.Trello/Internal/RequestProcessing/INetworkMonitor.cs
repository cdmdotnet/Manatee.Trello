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
 
	File Name:		INetworkMonitor.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		INetworkMonitor
	Purpose:		Defines properties and methods used to monitor network
					connectivity.

***************************************************************************************/
using System;

namespace Manatee.Trello.Internal.RequestProcessing
{
	/// <summary>
	/// Defines properties and methods used to monitor network connectivity.
	/// </summary>
	/// <remarks>
	/// Exposed solely for unit testing purposes.
	/// </remarks>
	public interface INetworkMonitor
	{
		/// <summary>
		/// Gets whether the system is currently connected to a network.
		/// </summary>
		bool IsConnected { get; }
		/// <summary>
		/// Raised when the connection status changes.
		/// </summary>
		event EventHandler ConnectionStatusChanged;
	}
}