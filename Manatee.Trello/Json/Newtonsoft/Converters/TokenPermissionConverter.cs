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
 
	File Name:		TokenPermissionConveter.cs
	Namespace:		Manatee.Trello.Json.Newtonsoft.Converters
	Class Name:		TokenPermissionConveter
	Purpose:		Provides a concrete implementation of IJsonToken for
					Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using Manatee.Trello.Json.Newtonsoft.Entities;
using Newtonsoft.Json.Linq;

namespace Manatee.Trello.Json.Newtonsoft.Converters
{
	internal class TokenPermissionConverter : JsonCreationConverter<IJsonTokenPermission>
	{
		protected override IJsonTokenPermission Create(Type objectType, JObject jObject)
		{
			return new NewtonsoftTokenPermission();
		}
	}
}