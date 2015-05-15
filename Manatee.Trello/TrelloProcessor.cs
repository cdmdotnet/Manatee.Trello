/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		TrelloProcessor.cs
	Namespace:		Manatee.Trello
	Class Name:		TrelloProcessor
	Purpose:		Provides options and control for the internal request queue
					processor.

***************************************************************************************/

using Manatee.Trello.Internal.RequestProcessing;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides options and control for the internal request queue processor.
	/// </summary>
	public static class TrelloProcessor
	{
		/// <summary>
		/// Specifies whether the request processor can keep the application process alive after the main thread exits.  Default is false.
		/// </summary>
		/// <remarks>
		/// When this property is set to true, the application must call <see cref="Shutdown"/> at the end of execution.  This can be used
		/// to ensure that all requests are sent to Trello before an application ends.  This property appears to have no effect in testing
		/// environments.
		/// </remarks>
		public static bool WaitForPendingRequests { get; set; }

		/// <summary>
		/// Signals the processor that the application is shutting down.  The processor will perform a "last call" for pending requests.
		/// </summary>
		/// <remarks>
		/// Calling this method is only required when <see cref="WaitForPendingRequests"/> is set to true.  This method can also be used
		/// in testing environments to complete all expected calls.
		/// </remarks>
		public static void Shutdown()
		{
			RestRequestProcessor.ShutDown();
		}
	}
}