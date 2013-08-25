﻿/***************************************************************************************

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
	public interface IEntityRepository
	{
		void Refresh<T>(T entity, EntityRequestType request)
			where T : ExpiringObject;
		void RefreshCollecion<T>(ExpiringObject list, EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>;
		T Download<T>(EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject;
		void Upload(EntityRequestType request, IDictionary<string, object> parameters);
	}
}