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
 
	File Name:		TrelloInteractionException.cs
	Namespace:		Manatee.Trello.Exceptions
	Class Name:		TrelloInteractionException
	Purpose:		Thrown when Trello reports an error with a request.

***************************************************************************************/
using System;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Thrown when Trello reports an error with a request.
	/// </summary>
	public class TrelloInteractionException : Exception
	{
		/// <summary>
		/// Creates a new instance of the TrelloInteractionException class.
		/// </summary>
		/// <param name="innerException">The exception which occurred during the call.</param>
		public TrelloInteractionException(Exception innerException)
			: base("Trello has reported an error with the request.", innerException) {}
	}
}
