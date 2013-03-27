/***************************************************************************************

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
 
	File Name:		JsonCompatibleEquatableExpiringObject.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		JsonCompatibleEquatableExpiringObject
	Purpose:		Base class which extends an ExpiringObject to implement
					IJsonCompatible.

***************************************************************************************/
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Implementation
{
	/// <summary>
	/// Base class which extends an ExpiringObject to implement IJsonCompatible.
	/// </summary>
	public abstract class JsonCompatibleExpiringObject : ExpiringObject, IJsonCompatible
	{
		internal JsonCompatibleExpiringObject() {}
		internal JsonCompatibleExpiringObject(TrelloService svc)
			: base(svc) {}
		internal JsonCompatibleExpiringObject(TrelloService svc, string id)
			: base(svc, id) {}
		internal JsonCompatibleExpiringObject(TrelloService svc, ExpiringObject owner)
			: base(svc, owner) {}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public abstract void FromJson(JsonValue json);
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public abstract JsonValue ToJson();
	}
}