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
 
	File Name:		IEntityRepository.cs
	Namespace:		Manatee.Trello.Internal.DataAccess
	Class Name:		IEntityRepository
	Purpose:		Manages creation and retrieval of Trello entities.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.DataAccess
{
	/// <summary>
	/// Manages creation and retrieval of Trello entities.
	/// </summary>
	/// <remarks>
	/// This interface is only exposed for unit testing purposes.
	/// </remarks>
	public interface IEntityRepository
	{
		/// <summary />
		TimeSpan EntityDuration { get; }
		/// <summary />
		bool Refresh<T>(T entity, EntityRequestType request)
			where T : ExpiringObject;
		/// <summary />
		bool RefreshCollection<T>(ExpiringObject list, EntityRequestType request)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>;
		/// <summary />
		T Download<T>(EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject;
		/// <summary />
		IEnumerable<T> GenerateList<T>(ExpiringObject owner, EntityRequestType request, string filter)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>;
		/// <summary />
		void Upload(EntityRequestType request, IDictionary<string, object> parameters);
		/// <summary />
		void NetworkStatusChanged(object sender, EventArgs e);
		/// <summary />
		bool AllowSelfUpdate { get; set; }
	}
}