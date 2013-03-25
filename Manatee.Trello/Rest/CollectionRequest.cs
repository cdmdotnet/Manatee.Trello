﻿/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		CollectionRequest.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		CollectionRequest
	Purpose:		A request object to be used to obtain a collection of items.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Implementation;

namespace Manatee.Trello.Rest
{
	internal class CollectionRequest<T> : Request<T>
		where T : ExpiringObject, new()
	{
		public CollectionRequest(IEnumerable<ExpiringObject> tokens, ExpiringObject entity = null)
			: base(tokens, entity) {}
	}
}