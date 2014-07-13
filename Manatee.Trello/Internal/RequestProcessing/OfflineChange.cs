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
 
	File Name:		OfflineChange.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		OfflineChange
	Purpose:		Defines a single offline change.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.RequestProcessing
{
	/// <summary>
	/// Defines a single offline change.
	/// </summary>
	/// <remarks>
	/// This class is only exposed for unit testing purposes.
	/// </remarks>
	public class OfflineChange
	{
		/// <summary />
		public IDictionary<string, object> Parameters { get; private set; }
		/// <summary />
		public object Entity { get; private set; }
		/// <summary />
		public Endpoint Endpoint { get; private set; }

		/// <summary />
		public OfflineChange(object entity, Endpoint endpoint, IDictionary<string, object> parameters)
		{
			Entity = entity;
			Endpoint = endpoint;
			Parameters = parameters;
		}
	}
}