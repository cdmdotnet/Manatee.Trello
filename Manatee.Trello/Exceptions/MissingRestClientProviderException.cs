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
 
	File Name:		MissingRestClientProviderException.cs
	Namespace:		Manatee.Trello.Exceptions
	Class Name:		MissingRestClientProviderException
	Purpose:		Thrown on TrelloService creation when the IRestClientProvider
					implementation is null.

***************************************************************************************/

using System;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Thrown on TrelloService creation when the IRestClientProvider implementation is null.
	/// </summary>
	public class MissingRestClientProviderException : Exception
	{
		/// <summary>
		/// Creates a new instance of the MissingRestClientProviderException class.
		/// </summary>
		public MissingRestClientProviderException()
			: base("An implementation of IRestClientProvider must be supplied in the configuration. " +
				   "You may create your own or download Manatee.Trello.RestSharp from Nuget.") {}
	}
}