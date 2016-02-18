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
 
	File Name:		TrelloAuthorization.cs
	Namespace:		Manatee.Trello
	Class Name:		TrelloAuthorization
	Purpose:		Contains authorization tokens needed to connect to trello.com.

***************************************************************************************/

using System;
using Manatee.Trello.Internal;

namespace Manatee.Trello
{
	/// <summary>
	/// Contains authorization tokens needed to connect to trello.com.
	/// </summary>
	public class TrelloAuthorization
	{
		private string _appKey;
		/// <summary>
		/// Gets the default authorization.
		/// </summary>
		public static TrelloAuthorization Default { get; }

		/// <summary>
		/// The token which identifies the application attempting to connect.
		/// </summary>
		public string AppKey
		{
			get { return _appKey; }
			set
			{
				if (value.IsNullOrWhiteSpace())
					throw new ArgumentNullException(nameof(value));
				_appKey = value;
			}
		}
		/// <summary>
		/// The token which identifies special permissions as granted by a specific user.
		/// </summary>
		public string UserToken { get; set; }

		static TrelloAuthorization()
		{
			Default = new TrelloAuthorization();
		}
	}
}