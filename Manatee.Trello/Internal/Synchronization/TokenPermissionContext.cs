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
 
	File Name:		TokenPermissionContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		TokenPermissionContext
	Purpose:		Provides a data context for a permissions granted by a token.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class TokenPermissionContext : LinkedSynchronizationContext<IJsonTokenPermission>
	{
		static TokenPermissionContext()
		{
			_properties = new Dictionary<string, Property<IJsonTokenPermission>>
				{
					{"ModelType", new Property<IJsonTokenPermission, TokenModelType>(d => d.ModelType, (d, o) => d.ModelType = o)},
					{"CanRead", new Property<IJsonTokenPermission, bool?>(d => d.Read, (d, o) => d.Read = o)},
					{"CanWrite", new Property<IJsonTokenPermission, bool?>(d => d.Write, (d, o) => d.Write = o)},
				};
		}
	}
}