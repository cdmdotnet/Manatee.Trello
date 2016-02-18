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
 
	File Name:		BoardBackgroundContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		BoardBackgroundContext
	Purpose:		Provides a data context for a board background.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardBackgroundContext : LinkedSynchronizationContext<IJsonBoardBackground>
	{
		static BoardBackgroundContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoardBackground>>
				{
					{"Id", new Property<IJsonBoardBackground, string>((d, a) => d.Id, (d, o ) => d.Id = o)},
					{
						"Color", new Property<IJsonBoardBackground, WebColor>((d, a) => d.Color.IsNullOrWhiteSpace() ? null : new WebColor(d.Color),
																			  (d, o) => d.Color = o?.ToString())
					},
					{"Image", new Property<IJsonBoardBackground, string>((d, a) => d.Image, (d, o) => d.Image = o)},
					{"IsTiled", new Property<IJsonBoardBackground, bool?>((d, a) => d.Tile, (d, o) => d.Tile = o)},
				};
		}
		public BoardBackgroundContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}