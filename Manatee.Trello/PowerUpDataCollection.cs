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
 
	File Name:		ReadOnlyPowerUpCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyPowerUpCollection
	Purpose:		Collection objects for power-ups.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of actions.
	/// </summary>
	public class ReadOnlyPowerUpDataCollection : ReadOnlyCollection<PowerUpData>
	{
		internal ReadOnlyPowerUpDataCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}
		internal ReadOnlyPowerUpDataCollection(ReadOnlyPowerUpDataCollection source, TrelloAuthorization auth)
			: this(() => source.OwnerId, auth) {}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_PowerUpData, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonPowerUpData>>(Auth, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jn =>
				{
					var powerUp = jn.GetFromCache<PowerUpData>(Auth);
					powerUp.Json = jn;
					return powerUp;
				}));
		}
	}
}