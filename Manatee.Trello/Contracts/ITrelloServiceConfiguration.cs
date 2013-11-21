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
 
	File Name:		ITrelloServiceConfiguration.cs
	Namespace:		Manatee.Trello
	Class Name:		ITrelloServiceConfiguration
	Purpose:		Defines a set of run-time options for Manatee.Trello.

***************************************************************************************/

using System;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines a set of run-time options for Manatee.Trello.
	/// </summary>
	public interface ITrelloServiceConfiguration
	{
		/// <summary>
		/// Gets and sets the global duration setting for all auto-refreshing objects.
		/// </summary>
		TimeSpan ItemDuration { get; set; }
		/// <summary>
		/// Specifies the serializer which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		ISerializer Serializer { get; set; }
		/// <summary>
		/// Specifies the deserializer which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		IDeserializer Deserializer { get; set; }
		/// <summary>
		/// Specifies the REST client provider which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		IRestClientProvider RestClientProvider { get; set; }
		/// <summary>
		/// Provides a single cache for all TrelloService instances.  This can be overridden per instance.
		/// </summary>
		ICache Cache { get; set; }
		/// <summary>
		/// Provides logging for all of Manatee.Trello.  The default log only writes to the Debug window.
		/// </summary>
		ILog Log { get; set; }
	}
}