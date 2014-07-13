/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		LabelNamesContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		LabelNamesContext
	Purpose:		Provides a data context for the label set of a board.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class LabelNamesContext : LinkedSynchronizationContext<IJsonLabelNames>
	{
		static LabelNamesContext()
		{
			_properties = new Dictionary<string, Property<IJsonLabelNames>>
				{
					{"Green", new Property<IJsonLabelNames>(d => d.Green, (d, o) => d.Green = (string) o)},
					{"Yellow", new Property<IJsonLabelNames>(d => d.Yellow, (d, o) => d.Yellow = (string) o)},
					{"Orange", new Property<IJsonLabelNames>(d => d.Orange, (d, o) => d.Orange = (string) o)},
					{"Red", new Property<IJsonLabelNames>(d => d.Red, (d, o) => d.Red = (string) o)},
					{"Purple", new Property<IJsonLabelNames>(d => d.Purple, (d, o) => d.Purple = (string) o)},
					{"Blue", new Property<IJsonLabelNames>(d => d.Blue, (d, o) => d.Blue = (string) o)},
				};
		}
	}
}