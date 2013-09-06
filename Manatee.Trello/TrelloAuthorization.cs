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
 
	File Name:		TrelloAuthorization.cs
	Namespace:		Manatee.Trello
	Class Name:		TrelloAuthorization
	Purpose:		Contains authorization tokens needed to connect to trello.com.

***************************************************************************************/

namespace Manatee.Trello
{
	/// <summary>
	/// Contains authorization tokens needed to connect to trello.com.
	/// </summary>
	public class TrelloAuthorization
	{
		/// <summary>
		/// The token which identifies the application attempting to connect.
		/// </summary>
		public string AppKey { get; private set; }
		/// <summary>
		/// The token which identifies special permissions as granted by a specific user.
		/// </summary>
		public string UserToken { get; set; }

		/// <summary>
		/// Creates a new instance of the TrelloAuthorization class.
		/// </summary>
		/// <param name="appKey">The application key.</param>
		/// <param name="userToken">The user token.</param>
		public TrelloAuthorization(string appKey, string userToken = null)
		{
			AppKey = appKey;
			UserToken = userToken;
		}
	}
}