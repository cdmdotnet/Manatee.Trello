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
 
	File Name:		PowerUpContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		PowerUpContext
	Purpose:		Provides a data context for power-ups.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class PowerUpContext : SynchronizationContext<IJsonPowerUp>
	{
		static PowerUpContext()
		{
			_properties = new Dictionary<string, Property<IJsonPowerUp>>
				{
					{"Id", new Property<IJsonPowerUp, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonPowerUp, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Public", new Property<IJsonPowerUp, bool?>((d, a) => d.Public, (d, o) => d.Public = o)},
					{"AdditionalInfo", new Property<IJsonPowerUp, string>((d, a) => d.Url, (d, o) => d.Url = o)},
				};
		}
		public PowerUpContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}
	}
}